import { ChangeDetectionStrategy, Component } from "@angular/core";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-home",
    templateUrl: "./home.component.html",
})
export class HomeComponent {
}
