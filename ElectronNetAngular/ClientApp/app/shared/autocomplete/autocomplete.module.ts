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

import { AutocompleteComponent } from "./autocomplete.component";

@NgModule({
    declarations: [
        AutocompleteComponent,
    ],
    exports: [
        AutocompleteComponent,
    ],
    imports: [
        CommonModule,
        ReactiveFormsModule,

        MatAutocompleteModule,
        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
    ],
})
export class AutocompleteModule { }
