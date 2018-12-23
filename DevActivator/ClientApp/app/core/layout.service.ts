import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";

import { IMessage } from "./interfaces";

@Injectable()
export class LayoutService {
    private _messages$: Subject<IMessage> = new Subject<IMessage>();
    private _progress$: BehaviorSubject<boolean | number> = new BehaviorSubject<boolean | number>(false);

    public get messages$(): Observable<IMessage> {
        return this._messages$.pipe();
    }

    public get progress$(): Observable<boolean | number> {
        return this._progress$.pipe();
    }

    public showInfo(text: string): void {
        const message: IMessage = {
            duration: 3000,
            severity: "info",
            text,
        };
        this._messages$.next(message);
    }

    public showWarning(text: string): void {
        const message: IMessage = {
            duration: 10000,
            severity: "warn",
            text,
        };
        this._messages$.next(message);
    }

    public showError(text: string): void {
        const message: IMessage = {
            duration: 60000,
            severity: "error",
            text,
        };
        this._messages$.next(message);
    }

    public showProgress(percentage: number = 0): void {
        this._progress$.next(percentage || true);
    }

    public hideProgress(): void {
        this._progress$.next(false);
    }
}
