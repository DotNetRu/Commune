import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

@Injectable()
export class VenueListService {
    private _venues$: BehaviorSubject<IAutocompleteRow[]> = new BehaviorSubject<IAutocompleteRow[]>([]);

    public get venues$(): Observable<IAutocompleteRow[]> {
        return this._venues$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchVenues(): void {
        this._httpService.get<IAutocompleteRow[]>(
            API_ENDPOINTS.getVenuesUrl,
            (venues: IAutocompleteRow[]) => this._venues$.next(venues),
        );
    }
}
