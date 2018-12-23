import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule, MatFormFieldModule, MatIconModule, MatInputModule } from "@angular/material";
import { RouterModule } from "@angular/router";
import { CoreModule } from "@dotnetru/core";
import { FileDialogModule } from "@dotnetru/shared/file-dialog";

import { FriendEditorComponent } from "./friend-editor.component";
import { FriendImageUrlPipe } from "./pipes";

@NgModule({
    declarations: [
        FriendEditorComponent,
        FriendImageUrlPipe,
    ],
    entryComponents: [
        FriendEditorComponent,
    ],
    exports: [
        FriendEditorComponent,
    ],
    imports: [
        RouterModule.forChild([
            { path: "friend-creator", component: FriendEditorComponent },
            { path: "friend-editor/:friendId", component: FriendEditorComponent },
        ]),

        CommonModule,
        FormsModule,
        ReactiveFormsModule,

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,

        CoreModule,
        FileDialogModule,
    ],
})
export class FriendEditorModule {
}
