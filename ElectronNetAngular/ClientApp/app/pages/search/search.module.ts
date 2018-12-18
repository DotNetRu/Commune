import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SpeakerListModule } from "@dotnetru/speaker-list";
import { TalkListModule } from "@dotnetru/talk-list";

import { SearchPageComponent } from "./search.component";

@NgModule({
    declarations: [
        SearchPageComponent,
    ],
    exports: [
        SearchPageComponent,
    ],
    imports: [
        RouterModule.forChild([
            { path: "search", component: SearchPageComponent },
        ]),

        SpeakerListModule,
        TalkListModule,
    ],
})
export class SearchPageModule { }
