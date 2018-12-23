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
import { AutocompleteModule } from "@dotnetru/shared/autocomplete";

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
        CommonModule,
        ReactiveFormsModule,

        AutocompleteModule,

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
