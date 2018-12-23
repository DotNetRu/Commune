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

import { MeetupListService } from "./meetup-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-meetup-list",
    templateUrl: "./meetup-list.component.html",
})
export class MeetupListComponent implements OnInit, OnDestroy {
    @Input() public title: string = "Поиск встречи";
    @Input() public iconName: string = "add";
    @Input() public iconText: string = "Добавить";

    @Input() public set meetupLink(value: { meetupId?: string }) {
        if (value && value.meetupId) {
            this._meetupId$.next(value.meetupId);
        }
    }

    @ViewChild("autocomplete") public autocomplete!: AutocompleteComponent;

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public meetups: IAutocompleteRow[] = [];

    private _meetupId$: BehaviorSubject<string> = new BehaviorSubject("");
    private _subs: Subscription[] = [];

    constructor(
        private _meetupListService: MeetupListService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._meetupListService.meetups$
                .subscribe(
                    (meetups: IAutocompleteRow[]) => {
                        this.meetups = meetups;
                        this._changeDetectorRef.detectChanges();
                    },
                ),
            this._meetupListService.meetups$.pipe(switchMap((_) => this._meetupId$.pipe()))
                .pipe(debounceTime(100))
                .subscribe((meetupId: string) => {
                    const meetup = this.meetups.find((x) => x.id === meetupId);
                    if (meetup) {
                        this.autocomplete.queryControl.patchValue(meetup.name);
                    } else {
                        console.warn("meetup not found", meetupId);
                    }
                }),
        ];

        this._meetupListService.fetchMeetups();
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
