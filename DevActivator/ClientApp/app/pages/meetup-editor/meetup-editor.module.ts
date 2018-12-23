import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule, MatCardModule, MatFormFieldModule, MatIconModule, MatInputModule } from "@angular/material";
import { RouterModule } from "@angular/router";
import { CoreModule } from "@dotnetru/core";
import { FriendListModule } from "@dotnetru/friend-list";
import { TalkListModule } from "@dotnetru/talk-list";
import { VenueListModule } from "@dotnetru/venue-list";

import { MeetupEditorComponent } from "./meetup-editor.component";

@NgModule({
    declarations: [
        MeetupEditorComponent,
    ],
    entryComponents: [
        MeetupEditorComponent,
    ],
    exports: [
        MeetupEditorComponent,
    ],
    imports: [
        RouterModule.forChild([
            { path: "meetup-creator", component: MeetupEditorComponent },
            { path: "meetup-editor/:meetupId", component: MeetupEditorComponent },
        ]),

        CommonModule,
        FormsModule,
        ReactiveFormsModule,

        MatButtonModule,
        MatCardModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,

        CoreModule,
        FriendListModule,
        VenueListModule,
        TalkListModule,
    ],
})
export class MeetupEditorModule { }
