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

import { TalkListService } from "./talk-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-talk-list",
    templateUrl: "./talk-list.component.html",
})
export class TalkListComponent implements OnInit, OnDestroy {
    @Input() public title: string = "Поиск доклада";
    @Input() public iconName: string = "add";
    @Input() public iconText: string = "Добавить";

    @Input() public set talkLink(value: { talkId?: string }) {
        if (value && value.talkId) {
            this._talkId$.next(value.talkId);
        }
    }

    @ViewChild("autocomplete") public autocomplete!: AutocompleteComponent;

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public talks: IAutocompleteRow[] = [];

    private _talkId$: BehaviorSubject<string> = new BehaviorSubject("");
    private _subs: Subscription[] = [];

    constructor(
        private _talkListService: TalkListService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._talkListService.talks$
                .subscribe(
                    (talks: IAutocompleteRow[]) => {
                        this.talks = talks;
                        this._changeDetectorRef.detectChanges();
                    },
                ),
            this._talkListService.talks$.pipe(switchMap((_) => this._talkId$.pipe()))
                .pipe(debounceTime(100))
                .subscribe((talkId: string) => {
                    const talk = this.talks.find((x) => x.id === talkId);
                    if (talk) {
                        this.autocomplete.queryControl.patchValue(talk.name);
                    } else {
                        console.warn("talk not found", talkId);
                    }
                }),
        ];

        this._talkListService.fetchTalks();
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
