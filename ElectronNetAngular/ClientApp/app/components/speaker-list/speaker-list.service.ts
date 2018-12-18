import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";

@Injectable()
export class SpeakerListService {
    private _speakers$: BehaviorSubject<IAutocompleteRow[]> = new BehaviorSubject<IAutocompleteRow[]>([]);

    public get speakers$(): Observable<IAutocompleteRow[]> {
        return this._speakers$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchSpeakers(): void {
        this._httpService.get<IAutocompleteRow[]>(
            API_ENDPOINTS.getSpeakersUrl,
            (speakers: IAutocompleteRow[]) => this._speakers$.next(speakers),
        );
    }
}
