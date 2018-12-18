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

import { TalkListComponent } from "./talk-list.component";
import { TalkListService } from "./talk-list.service";

@NgModule({
    declarations: [
        TalkListComponent,
    ],
    entryComponents: [
        TalkListComponent,
    ],
    exports: [
        TalkListComponent,
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
        TalkListService,
    ],
})
export class TalkListModule { }
