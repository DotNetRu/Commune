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

import { FriendListComponent } from "./friend-list.component";
import { FriendListService } from "./friend-list.service";

@NgModule({
    declarations: [
        FriendListComponent,
    ],
    entryComponents: [
        FriendListComponent,
    ],
    exports: [
        FriendListComponent,
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
        FriendListService,
    ],
})
export class FriendListModule { }
