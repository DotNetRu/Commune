import { NgModule } from "@angular/core";
import { MatMenuModule } from "@angular/material";
import { RouterModule } from "@angular/router";

import { NavMenuComponent } from "./navmenu.component";

@NgModule({
    declarations: [
        NavMenuComponent,
    ],
    exports: [
        NavMenuComponent,
    ],
    imports: [
        RouterModule,
        MatMenuModule,
    ],
})
export class NavMenuModule {
}
