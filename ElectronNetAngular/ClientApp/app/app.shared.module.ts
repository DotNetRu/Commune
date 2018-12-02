import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { MatSidenavModule } from "@angular/material";
import { RouterModule } from "@angular/router";
import { CoreModule } from "@dotnetru/core";
import { SpeakerEditorModule } from "@dotnetru/speaker-editor";
import { SpeakerListModule } from "@dotnetru/speaker-list";

import { AppComponent } from "./app.component";
import { HomeComponent } from "./components/home/home.component";
import { NavMenuModule } from "./components/navmenu/navmenu.module";
import { ToolbarModule } from "./components/toolbar/toolbar.module";

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
    ],
    imports: [
        CommonModule,
        HttpClientModule,

        MatSidenavModule,
        ToolbarModule,
        NavMenuModule,

        CoreModule,
        SpeakerEditorModule,
        SpeakerListModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "**", redirectTo: "home" },
        ]),
    ],
})
export class AppModuleShared {
}
