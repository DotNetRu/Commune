import { Injectable } from "@angular/core";
import { API_ENDPOINTS, HttpService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter } from "rxjs/operators";

import { ISpeakerRow } from "./interfaces";

@Injectable()
export class SpeakerListService {
    private _speakers$: BehaviorSubject<ISpeakerRow[]> = new BehaviorSubject<ISpeakerRow[]>([]);

    public get speakers$(): Observable<ISpeakerRow[]> {
        return this._speakers$.pipe(filter((x) => x.length > 0));
    }

    constructor(
        private _httpService: HttpService,
    ) { }

    public fetchSpeakers(): void {
        this._httpService.get<ISpeakerRow[]>(
            API_ENDPOINTS.getSpeakersUrl,
            (speakers: ISpeakerRow[]) => this._speakers$.next(speakers),
        );
    }
}
