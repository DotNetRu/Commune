import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule, MatCardModule, MatFormFieldModule, MatIconModule, MatInputModule } from "@angular/material";
import { RouterModule } from "@angular/router";
import { CoreModule } from "@dotnetru/core";
import { SpeakerListModule } from "@dotnetru/speaker-list";

import { TalkEditorComponent } from "./talk-editor.component";

@NgModule({
    declarations: [
        TalkEditorComponent,
    ],
    entryComponents: [
        TalkEditorComponent,
    ],
    exports: [
        TalkEditorComponent,
    ],
    imports: [
        RouterModule.forChild([
            { path: "talk-creator", component: TalkEditorComponent },
            { path: "talk-editor/:talkId", component: TalkEditorComponent },
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
        SpeakerListModule,
    ],
})
export class TalkEditorModule { }
