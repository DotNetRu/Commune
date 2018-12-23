import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

@Injectable()
export class TalkListService {
    private _talks$: BehaviorSubject<IAutocompleteRow[]> = new BehaviorSubject<IAutocompleteRow[]>([]);

    public get talks$(): Observable<IAutocompleteRow[]> {
        return this._talks$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchTalks(): void {
        this._httpService.get<IAutocompleteRow[]>(
            API_ENDPOINTS.getTalksUrl,
            (talks: IAutocompleteRow[]) => this._talks$.next(talks),
        );
    }
}
