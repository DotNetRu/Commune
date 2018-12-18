import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { MatSidenavModule } from "@angular/material";
import { RouterModule } from "@angular/router";
import { CoreModule } from "@dotnetru/core";
import { SearchPageModule } from "@dotnetru/pages/search";
import { SpeakerEditorModule } from "@dotnetru/pages/speaker-editor";
import { TalkEditorModule } from "@dotnetru/pages/talk-editor";
import { AutocompleteModule } from "@dotnetru/shared/autocomplete";
import { SpeakerListModule } from "@dotnetru/speaker-list";

import { AppComponent } from "./app.component";
import { NavMenuModule } from "./components/navmenu/navmenu.module";
import { ToolbarModule } from "./components/toolbar/toolbar.module";
import { SearchPageComponent } from "./pages/search/search.component";

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        CommonModule,
        HttpClientModule,

        MatSidenavModule,
        ToolbarModule,
        NavMenuModule,

        CoreModule,

        AutocompleteModule,

        SpeakerEditorModule,
        SpeakerListModule,

        TalkEditorModule,

        SearchPageModule,

        RouterModule.forRoot([
            { path: "", redirectTo: "search", pathMatch: "full" },
            { path: "search", component: SearchPageComponent },
            { path: "**", redirectTo: "search" },
        ]),
    ],
})
export class AppModuleShared { }
