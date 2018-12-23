import jetbrains.buildServer.configs.kotlin.v2018_2.*
import jetbrains.buildServer.configs.kotlin.v2018_2.buildSteps.dotnetBuild
import jetbrains.buildServer.configs.kotlin.v2018_2.buildSteps.dotnetRestore
import jetbrains.buildServer.configs.kotlin.v2018_2.buildSteps.powerShell
import jetbrains.buildServer.configs.kotlin.v2018_2.triggers.finishBuildTrigger
import jetbrains.buildServer.configs.kotlin.v2018_2.triggers.vcs

/*
The settings script is an entry point for defining a TeamCity
project hierarchy. The script should contain a single call to the
project() function with a Project instance or an init function as
an argument.

VcsRoots, BuildTypes, Templates, and subprojects can be
registered inside the project using the vcsRoot(), buildType(),
template(), and subProject() methods respectively.

To debug settings scripts in command-line, run the

    mvnDebug org.jetbrains.teamcity:teamcity-configs-maven-plugin:generate

command and attach your debugger to the port 8000.

To debug in IntelliJ Idea, open the 'Maven Projects' tool window (View
-> Tool Windows -> Maven Projects), find the generate task node
(Plugins -> teamcity-configs -> teamcity-configs:generate), the
'Debug' option is available in the context menu for the task.
*/

version = "2018.2"

project {

    buildType(CreateGithubRelease)
    buildType(Build)
}

object Build : BuildType({
    name = "Build"

    allowExternalStatus = true
    artifactRules = """+:DevActivator\obj\desktop\win\DevActivator\bin\desktop\app.%build.number%.zip"""
    buildNumberPattern = "1.0.%build.counter%-%teamcity.build.branch%"

    params {
        param("PathToRepo", "%teamcity.build.checkoutDir%")
    }

    vcs {
        root(DslContext.settingsRoot)
    }

    steps {
        dotnetRestore {
            name = "nuget restore"
            projects = "ElectronNetAngular.sln"
            param("dotNetCoverage.dotCover.home.path", "%teamcity.tool.JetBrains.dotCover.CommandLineTools.DEFAULT%")
        }
        step {
            name = "npm ci"
            type = "jonnyzzz.npm"
            param("teamcity.build.workingDir", "DevActivator")
            param("npm_commands", "ci")
        }
        dotnetBuild {
            name = "dotnet build"
            projects = "ElectronNetAngular.sln"
            param("dotNetCoverage.dotCover.home.path", "%teamcity.tool.JetBrains.dotCover.CommandLineTools.DEFAULT%")
        }
        powerShell {
            name = "electronize"
            workingDir = "DevActivator"
            scriptMode = script {
                content = "electronize build /target win"
            }
        }
        powerShell {
            name = "electron-packager"
            workingDir = "DevActivator/obj/desktop/win"
            scriptMode = script {
                content = """
                    npm install
                    electron-packager . --platform=win32 --arch=x64  --out="%system.teamcity.build.workingDir%\DevActivator\bin\desktop" --overwrite --electron-version=4.0.0
                """.trimIndent()
            }
        }
        powerShell {
            scriptMode = script {
                content = """Compress-Archive -Path %system.teamcity.build.workingDir%\DevActivator\obj\desktop\win\DevActivator\bin\desktop\* -CompressionLevel Fastest -DestinationPath %system.teamcity.build.workingDir%\DevActivator\obj\desktop\win\DevActivator\bin\desktop\app.%build.number%.zip"""
            }
        }
    }

    triggers {
        vcs {
        }
    }
})

object CreateGithubRelease : BuildType({
    name = "Create github-release"

    enablePersonalBuilds = false
    type = BuildTypeSettings.Type.DEPLOYMENT
    maxRunningBuilds = 1

    params {
        param("PathToRepo", """"${Build.depParamRefs["PathToRepo"]}"""")
    }

    steps {
        powerShell {
            name = "create release"
            enabled = false
            scriptMode = script {
                content = """
                    %PathToRepo% git tag Release-v0.%build.number% 
                    %PathToRepo% git push 
                    %PathToRepo% git push --tags
                """.trimIndent()
            }
        }
    }

    triggers {
        finishBuildTrigger {
            buildType = "${Build.id}"
            successfulOnly = true
        }
    }

    dependencies {
        dependency(Build) {
            snapshot {
                onDependencyFailure = FailureAction.CANCEL
                onDependencyCancel = FailureAction.CANCEL
            }

            artifacts {
                artifactRules = "**/app.*.zip"
            }
        }
    }
})
