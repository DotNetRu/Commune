import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";

@Injectable()
export class FriendListService {
    private _friends$: BehaviorSubject<IAutocompleteRow[]> = new BehaviorSubject<IAutocompleteRow[]>([]);

    public get friends$(): Observable<IAutocompleteRow[]> {
        return this._friends$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchFriends(): void {
        this._httpService.get<IAutocompleteRow[]>(
            API_ENDPOINTS.getFriendsUrl,
            (friends: IAutocompleteRow[]) => this._friends$.next(friends),
        );
    }
}
