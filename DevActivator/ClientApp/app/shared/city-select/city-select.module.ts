import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatFormFieldModule, MatSelectModule } from "@angular/material";

import { CityNamePipe } from "./city-name.pipe";
import { CitySelectComponent } from "./city-select.component";

@NgModule({
    declarations: [
        CityNamePipe,
        CitySelectComponent,
    ],
    exports: [
        CityNamePipe,
        CitySelectComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,

        MatFormFieldModule,
        MatSelectModule,
    ],
})
export class CitySelectModule { }
