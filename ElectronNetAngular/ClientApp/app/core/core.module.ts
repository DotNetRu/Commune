import { NgModule } from "@angular/core";

import { HttpService } from "./http.service";
import { LayoutService } from "./layout.service";
import { PatternErrorMessagePipe } from "./pattern-error-message.pipe";
import { RequiredErrorMessagePipe } from "./required-error-message.pipe";

@NgModule({
    declarations: [
        PatternErrorMessagePipe,
        RequiredErrorMessagePipe,
    ],
    exports: [
        PatternErrorMessagePipe,
        RequiredErrorMessagePipe,
    ],
    providers: [
        LayoutService,
        HttpService,
    ],
})
export class CoreModule { }
