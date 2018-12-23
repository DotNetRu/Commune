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

import { FriendListService } from "./friend-list.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: "mtp-friend-list",
    templateUrl: "./friend-list.component.html",
})
export class FriendListComponent implements OnInit, OnDestroy {
    @Input() public title: string = "Поиск друга";
    @Input() public iconName: string = "add";
    @Input() public iconText: string = "Добавить";

    @Input() public set friendLink(value: { friendId?: string }) {
        if (value && value.friendId) {
            this._friendId$.next(value.friendId);
        }
    }

    @ViewChild("autocomplete") public autocomplete!: AutocompleteComponent;

    @Output() public readonly selected: EventEmitter<IAutocompleteRow> = new EventEmitter<IAutocompleteRow>();
    @Output() public readonly iconClicked: EventEmitter<void> = new EventEmitter<void>();

    public friends: IAutocompleteRow[] = [];

    private _friendId$: BehaviorSubject<string> = new BehaviorSubject("");
    private _subs: Subscription[] = [];

    constructor(
        private _friendListService: FriendListService,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._friendListService.friends$
                .subscribe(
                    (friends: IAutocompleteRow[]) => {
                        this.friends = friends;
                        this._changeDetectorRef.detectChanges();
                    },
                ),
            this._friendListService.friends$.pipe(switchMap((_) => this._friendId$.pipe()))
                .pipe(debounceTime(100))
                .subscribe((friendId: string) => {
                    const friend = this.friends.find((x) => x.id === friendId);
                    if (friend) {
                        this.autocomplete.queryControl.patchValue(friend.name);
                    } else {
                        console.warn("friend not found", friendId);
                    }
                }),
        ];

        this._friendListService.fetchFriends();
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
