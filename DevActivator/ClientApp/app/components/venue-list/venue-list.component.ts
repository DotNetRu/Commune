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

import { VenueListService } from "./venue-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-venue-list",
    templateUrl: "./venue-list.component.html",
})
export class VenueListComponent implements OnInit, OnDestroy {
    @Input() public title: string = "Поиск площадки";
    @Input() public iconName: string = "";
    @Input() public iconText: string = "";

    @Input() public set venueLink(value: { venueId?: string }) {
        if (value && value.venueId) {
            this._venueId$.next(value.venueId);
        }
    }

    @ViewChild("autocomplete") public autocomplete!: AutocompleteComponent;

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public venues: IAutocompleteRow[] = [];

    private _venueId$: BehaviorSubject<string> = new BehaviorSubject("");
    private _subs: Subscription[] = [];

    constructor(
        private _venueListService: VenueListService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._venueListService.venues$
                .subscribe(
                    (venues: IAutocompleteRow[]) => {
                        this.venues = venues;
                        this._changeDetectorRef.detectChanges();
                    },
                ),
            this._venueListService.venues$.pipe(switchMap((_) => this._venueId$.pipe()))
                .pipe(debounceTime(100))
                .subscribe((venueId: string) => {
                    const venue = this.venues.find((x) => x.id === venueId);
                    if (venue) {
                        this.autocomplete.queryControl.patchValue(venue.name);
                    } else {
                        console.warn("venue not found", venueId);
                    }
                }),
        ];

        this._venueListService.fetchVenues();
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
