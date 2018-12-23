import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { API_ENDPOINTS, HttpService, LayoutService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, map } from "rxjs/operators";

import { ISpeaker } from "./interfaces";

@Injectable()
export class SpeakerEditorService {
    private _speaker$: BehaviorSubject<ISpeaker | null> = new BehaviorSubject<ISpeaker | null>(null);
    private _dataStore = {
        speaker: {} as ISpeaker,
    };

    public get speaker$(): Observable<ISpeaker> {
        return this._speaker$.pipe(
            filter((x) => x !== null),
            map((x) => x as ISpeaker),
        );
    }

    constructor(
        private _layoutService: LayoutService,
        private _httpService: HttpService,
        private _router: Router,
    ) { }

    public hasChanges(speaker: ISpeaker): boolean {
        return JSON.stringify(speaker) !== JSON.stringify(this._dataStore.speaker);
    }

    public fetchSpeaker(speakerId: string): void {
        this._httpService.get<ISpeaker>(
            API_ENDPOINTS.getSpeakerUrl.replace("{{speakerId}}", speakerId),
            (speaker: ISpeaker) => {
                this._dataStore.speaker = speaker;
                this._speaker$.next(Object.assign({}, this._dataStore.speaker));
            });
    }

    public addSpeaker(speaker: ISpeaker): void {
        this._httpService.post<ISpeaker>(
            API_ENDPOINTS.addSpeakerUrl,
            speaker,
            (x: ISpeaker) => {
                this._layoutService.showInfo("Спикер добавлен успешно");
                this._router.navigateByUrl(`speaker-editor${speaker ? `/${speaker.id}` : ""}`);
            },
        );
    }

    public updateSpeaker(speaker: ISpeaker): void {
        this._httpService.post<ISpeaker>(
            API_ENDPOINTS.updateSpeakerUrl,
            speaker,
            (x: ISpeaker) => {
                this._layoutService.showInfo("Спикер изменён успешно");
                this._dataStore.speaker = x;
                this._speaker$.next(Object.assign({}, this._dataStore.speaker));
            },
        );
    }

    public reset(): void {
        this._speaker$.next(Object.assign({}, this._dataStore.speaker));
    }

    public storeSpeakerAvatar(speakerId: string, blob: Blob): void {
        const url: string = API_ENDPOINTS.storeSpeakerAvatarUrl
            .replace("{{speakerId}}", speakerId);

        const formData: FormData = new FormData();
        formData.append("formFile", blob, "avatar.jpg");

        this._httpService.put(url, formData, () => {
            this._layoutService.showInfo("Аватар загружен успешно");
        });
    }
}
