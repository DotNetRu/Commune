import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { API_ENDPOINTS, HttpService, LayoutService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, map } from "rxjs/operators";

import { IMeetup } from "./interfaces";

@Injectable()
export class MeetupEditorService {
    private _meetup$: BehaviorSubject<IMeetup | null> = new BehaviorSubject<IMeetup | null>(null);
    private _dataStore = {
        meetup: {} as IMeetup,
    };

    public get meetup$(): Observable<IMeetup> {
        return this._meetup$.pipe(
            filter((x) => x !== null),
            map((x) => x as IMeetup),
        );
    }

    constructor(
        private _layoutService: LayoutService,
        private _httpService: HttpService,
        private _router: Router,
    ) { }

    public hasChanges(meetup: IMeetup): boolean {
        return JSON.stringify(meetup) !== JSON.stringify(this._dataStore.meetup);
    }

    public fetchMeetup(meetupId: string): void {
        this._httpService.get<IMeetup>(
            API_ENDPOINTS.getMeetupUrl.replace("{{meetupId}}", meetupId),
            (meetup: IMeetup) => {
                this._dataStore.meetup = meetup;
                this._meetup$.next(Object.assign({}, this._dataStore.meetup));
            });
    }

    public addMeetup(meetup: IMeetup): void {
        this._httpService.post<IMeetup>(
            API_ENDPOINTS.addMeetupUrl,
            meetup,
            (x: IMeetup) => {
                this._layoutService.showInfo("Доклад добавлен успешно");
                this._router.navigateByUrl(`meetup-editor${meetup ? `/${meetup.id}` : ""}`);
            },
        );
    }

    public updateMeetup(meetup: IMeetup): void {
        this._httpService.post<IMeetup>(
            API_ENDPOINTS.updateMeetupUrl,
            meetup,
            (x: IMeetup) => {
                this._layoutService.showInfo("Доклад изменён успешно");
                this._dataStore.meetup = x;
                this._meetup$.next(Object.assign({}, this._dataStore.meetup));
            },
        );
    }

    public reset(): void {
        this._meetup$.next(Object.assign({}, this._dataStore.meetup));
    }
}
