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

import { MeetupListComponent } from "./meetup-list.component";
import { MeetupListService } from "./meetup-list.service";

@NgModule({
    declarations: [
        MeetupListComponent,
    ],
    entryComponents: [
        MeetupListComponent,
    ],
    exports: [
        MeetupListComponent,
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
        MeetupListService,
    ],
})
export class MeetupListModule { }
