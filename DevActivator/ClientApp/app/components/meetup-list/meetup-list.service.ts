import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";

@Injectable()
export class MeetupListService {
    private _meetups$: BehaviorSubject<IAutocompleteRow[]> = new BehaviorSubject<IAutocompleteRow[]>([]);

    public get meetups$(): Observable<IAutocompleteRow[]> {
        return this._meetups$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchMeetups(): void {
        this._httpService.get<IAutocompleteRow[]>(
            API_ENDPOINTS.getMeetupsUrl,
            (meetups: IAutocompleteRow[]) => this._meetups$.next(meetups),
        );
    }
}
