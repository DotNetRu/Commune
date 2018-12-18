import { ChangeDetectionStrategy, Component } from "@angular/core";
import { Router } from "@angular/router";
import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-search",
    templateUrl: "./search.component.html",
})
export class SearchPageComponent {
    constructor(
        private _router: Router,
    ) { }

    public onSpeakerSelected(speaker: IAutocompleteRow): void {
        this._router.navigateByUrl(`speaker-editor/${speaker.id}`);
    }

    public addSpeaker(): void {
        this._router.navigateByUrl(`speaker-creator`);
    }

    public onTalkSelected(talk: IAutocompleteRow): void {
        this._router.navigateByUrl(`talk-editor/${talk.id}`);
    }

    public addTalk(): void {
        this._router.navigateByUrl(`talk-creator`);
    }
}
