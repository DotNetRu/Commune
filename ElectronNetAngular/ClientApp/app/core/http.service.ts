import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";

import { LayoutService } from "./layout.service";

@Injectable()
export class HttpService {
    private _requestsCount: number = 0;

    constructor(
        @Inject("BASE_URL") private _baseUrl: string,
        private _httpClient: HttpClient,
        private _layoutService: LayoutService,
    ) { }

    public get<T>(url: string, cb: (x: T) => void, errCb?: (x: any) => void): void {
        this.requestStarted();
        this._httpClient.get<T>(this.fromBase(url)).subscribe(
            (res: T) => this.onSuccess(res, cb),
            (err) => this.onError(err, errCb),
            () => this.onComplete(),
        );
    }

    public post<T>(url: string, body: any, cb: (result: T) => void, errCb?: (x: any) => void): void {
        this.requestStarted();
        this._httpClient.post<T>(this.fromBase(url), body).subscribe(
            (res: T) => this.onSuccess(res, cb),
            (err) => this.onError(err, errCb),
            () => this.onComplete(),
        );
    }

    public put<T>(url: string, body: any, cb: (result: T) => void, errCb?: (x: any) => void): void {
        this.requestStarted();
        this._httpClient.put<T>(this.fromBase(url), body).subscribe(
            (res: T) => this.onSuccess(res, cb),
            (err) => this.onError(err, errCb),
            () => this.onComplete(),
        );
    }

    private fromBase(url: string) {
        return `${this._baseUrl}${url}`;
    }

    private requestStarted(): void {
        ++this._requestsCount;
        this._layoutService.showProgress();
    }

    private onSuccess<T>(res: T, cb: (x: T) => void): void {
        cb(res);
    }

    private onError = (err: any, errCb?: (x: any) => void): void => {
        console.error(err);
        if (errCb) {
            errCb(err);
        } else {
            this._layoutService.showError(err.message);
        }
    }

    private onComplete(): void {
        if (this._requestsCount > 0) {
            --this._requestsCount;
            if (this._requestsCount === 0) {
                this._layoutService.hideProgress();
            }
        } else {
            console.warn("try decrease requestsCount below zero");
        }
    }
}
