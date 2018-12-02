import { NgModule } from "@angular/core";
import {
    MatButtonModule,
    MatIconModule,
    MatProgressBarModule,
    MatSnackBarModule,
    MatToolbarModule,
} from "@angular/material";
import { CoreModule } from "@dotnetru/core";

import { ToolbarComponent } from "./toolbar.component";

@NgModule({
    declarations: [
        ToolbarComponent,
    ],
    exports: [
        ToolbarComponent,
    ],
    imports: [
        CoreModule,

        MatButtonModule,
        MatIconModule,
        MatProgressBarModule,
        MatSnackBarModule,
        MatToolbarModule,
    ],
})
export class ToolbarModule { }
