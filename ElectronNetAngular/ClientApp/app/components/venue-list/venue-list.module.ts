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

import { VenueListComponent } from "./venue-list.component";
import { VenueListService } from "./venue-list.service";

@NgModule({
    declarations: [
        VenueListComponent,
    ],
    entryComponents: [
        VenueListComponent,
    ],
    exports: [
        VenueListComponent,
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
        VenueListService,
    ],
})
export class VenueListModule { }
