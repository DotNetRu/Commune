import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { API_ENDPOINTS, HttpService, LayoutService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, map } from "rxjs/operators";

import { ITalk } from "./interfaces";

@Injectable()
export class TalkEditorService {
    private _talk$: BehaviorSubject<ITalk | null> = new BehaviorSubject<ITalk | null>(null);
    private _dataStore = {
        talk: {} as ITalk,
    };

    public get talk$(): Observable<ITalk> {
        return this._talk$.pipe(
            filter((x) => x !== null),
            map((x) => x as ITalk),
        );
    }

    constructor(
        private _layoutService: LayoutService,
        private _httpService: HttpService,
        private _router: Router,
    ) { }

    public hasChanges(talk: ITalk): boolean {
        return JSON.stringify(talk) !== JSON.stringify(this._dataStore.talk);
    }

    public fetchTalk(talkId: string): void {
        this._httpService.get<ITalk>(
            API_ENDPOINTS.getTalkUrl.replace("{{talkId}}", talkId),
            (talk: ITalk) => {
                this._dataStore.talk = talk;
                this._talk$.next(Object.assign({}, this._dataStore.talk));
            });
    }

    public addTalk(talk: ITalk): void {
        this._httpService.post<ITalk>(
            API_ENDPOINTS.addTalkUrl,
            talk,
            (x: ITalk) => {
                this._layoutService.showInfo("Доклад добавлен успешно");
                this._router.navigateByUrl(`talk-editor${talk ? `/${talk.id}` : ""}`);
            },
        );
    }

    public updateTalk(talk: ITalk): void {
        this._httpService.post<ITalk>(
            API_ENDPOINTS.updateTalkUrl,
            talk,
            (x: ITalk) => {
                this._layoutService.showInfo("Доклад изменён успешно");
                this._dataStore.talk = x;
                this._talk$.next(Object.assign({}, this._dataStore.talk));
            },
        );
    }

    public reset(): void {
        this._talk$.next(Object.assign({}, this._dataStore.talk));
    }
}
