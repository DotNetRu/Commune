import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import {
    MatAutocompleteModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
} from "@angular/material";
import { RouterModule } from "@angular/router";

import { SpeakerListComponent } from "./speaker-list.component";
import { SpeakerListService } from "./speaker-list.service";

@NgModule({
    declarations: [
        SpeakerListComponent,
    ],
    entryComponents: [
        SpeakerListComponent,
    ],
    exports: [
        SpeakerListComponent,
    ],
    imports: [
        RouterModule.forChild([
            { path: "speaker-list", component: SpeakerListComponent },
        ]),
        CommonModule,
        ReactiveFormsModule,

        MatAutocompleteModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
    ],
    providers: [
        SpeakerListService,
    ],
})
export class SpeakerListModule { }
