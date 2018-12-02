import { NgModule } from "@angular/core";
import { MatButtonModule, MatIconModule } from "@angular/material";

import { FileDialogComponent } from "./file-dialog.component";

@NgModule({
    declarations: [
        FileDialogComponent,
    ],
    exports: [
        FileDialogComponent,
    ],
    imports: [
        MatIconModule,
        MatButtonModule,
    ],
})
export class FileDialogModule {
}
