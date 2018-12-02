import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormControl } from "@angular/forms";
import { Router } from "@angular/router";
import { Observable, Subscription } from "rxjs";
import { debounceTime, filter, map } from "rxjs/operators";

import { ISpeakerRow } from "./interfaces";
import { SpeakerListService } from "./speaker-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-speaker-list",
    styleUrls: ["./speaker-list.component.css"],
    templateUrl: "./speaker-list.component.html",
})
export class SpeakerListComponent implements OnInit, OnDestroy {
    public speakers: ISpeakerRow[] = [];
    public speakers$: Observable<ISpeakerRow[]>;
    public speakersControl: FormControl;

    private _subs: Subscription[] = [];

    constructor(
        private _speakerListService: SpeakerListService,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
        fb: FormBuilder,
    ) {
        this.speakersControl = fb.control(undefined);
        this.speakers$ = this.speakersControl.valueChanges
            .pipe(
                filter((val) => typeof val === "string"),
                debounceTime(300),
                map((query: string) => {
                    const searchPhrases: string[] = (query || "")
                        .toLowerCase()
                        .replace(/\s+/, " ")
                        .split(" ")
                        .map((x) => x.replace(/[^а-яёa-z\d]/gi, ""));
                    return this.speakers.filter((x) => {
                        const lowerName: string = x.name.toLowerCase();
                        let ix: number = 0;
                        for (const searchPhrase of searchPhrases) {
                            // important order
                            ix = lowerName/* .substring(ix) */.indexOf(searchPhrase);
                            if (ix < 0) {
                                return false;
                            }
                        }

                        return true;
                    });
                }),
            );
    }

    public ngOnInit(): void {
        this._subs = [
            this._speakerListService.speakers$
                .subscribe(
                    (speakers: ISpeakerRow[]) => {
                        this.speakers = speakers;
                        this.speakersControl.setValue("");
                        this._changeDetectorRef.detectChanges();
                    },
                    (error) => console.warn(error),
                ),
        ];

        this._speakerListService.fetchSpeakers();
    }

    public ngOnDestroy(): void {
        this._subs.forEach((x) => x.unsubscribe());
    }

    public onSelected(speaker: ISpeakerRow): void {
        this._router.navigateByUrl(`speaker-editor/${speaker.id}`);
    }
}
