import {
    ChangeDetectionStrategy,
    ChangeDetectorRef,
    Component,
    EventEmitter,
    Input,
    OnDestroy,
    OnInit,
    Output,
    ViewChild,
} from "@angular/core";
import { AutocompleteComponent, IAutocompleteRow } from "@dotnetru/shared/autocomplete";
import { BehaviorSubject, Subscription } from "rxjs";
import { debounceTime, switchMap } from "rxjs/operators";

import { SpeakerListService } from "./speaker-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-speaker-list",
    templateUrl: "./speaker-list.component.html",
})
export class SpeakerListComponent implements OnInit, OnDestroy {
    @Input() public title: string = "Поиск спикера";
    @Input() public iconName: string = "add";
    @Input() public iconText: string = "Добавить";

    @Input() public set speakerLink(value: { speakerId?: string }) {
        if (value && value.speakerId) {
            this._speakerId$.next(value.speakerId);
        }
    }

    @ViewChild("autocomplete") public autocomplete!: AutocompleteComponent;

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public speakers: IAutocompleteRow[] = [];

    private _speakerId$: BehaviorSubject<string> = new BehaviorSubject("");
    private _subs: Subscription[] = [];

    constructor(
        private _speakerListService: SpeakerListService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._speakerListService.speakers$
                .subscribe(
                    (speakers: IAutocompleteRow[]) => {
                        this.speakers = speakers;
                        this._changeDetectorRef.detectChanges();
                    },
                ),
            this._speakerListService.speakers$.pipe(switchMap((_) => this._speakerId$.pipe()))
                .pipe(debounceTime(100))
                .subscribe((speakerId: string) => {
                    const speaker = this.speakers.find((x) => x.id === speakerId);
                    if (speaker) {
                        this.autocomplete.queryControl.patchValue(speaker.name);
                    } else {
                        console.warn("speaker not found", speakerId);
                    }
                }),
        ];

        this._speakerListService.fetchSpeakers();
    }

    public ngOnDestroy(): void {
        this._subs.forEach((x) => x.unsubscribe());
    }

    public onSelected(row: IAutocompleteRow): void {
        this.selected.emit(row);
    }

    public onIconClicked(): void {
        this.iconClicked.emit();
    }
}
