import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { API_ENDPOINTS, HttpService, LayoutService } from "@dotnetru/core";
import { BehaviorSubject, Observable } from "rxjs";
import { filter, map } from "rxjs/operators";

import { IVenue } from "./interfaces";

@Injectable()
export class VenueEditorService {
    private _venue$: BehaviorSubject<IVenue | null> = new BehaviorSubject<IVenue | null>(null);
    private _dataStore = {
        venue: {} as IVenue,
    };

    public get venue$(): Observable<IVenue> {
        return this._venue$.pipe(
            filter((x) => x !== null),
            map((x) => x as IVenue),
        );
    }

    constructor(
        private _layoutService: LayoutService,
        private _httpService: HttpService,
        private _router: Router,
    ) { }

    public hasChanges(venue: IVenue): boolean {
        return JSON.stringify(venue) !== JSON.stringify(this._dataStore.venue);
    }

    public fetchVenue(venueId: string): void {
        this._httpService.get<IVenue>(
            API_ENDPOINTS.getVenueUrl.replace("{{venueId}}", venueId),
            (venue: IVenue) => {
                this._dataStore.venue = venue;
                this._venue$.next(Object.assign({}, this._dataStore.venue));
            });
    }

    public addVenue(venue: IVenue): void {
        this._httpService.post<IVenue>(
            API_ENDPOINTS.addVenueUrl,
            venue,
            (x: IVenue) => {
                this._layoutService.showInfo("Площадка добавлена успешно");
                this._router.navigateByUrl(`venue-editor${venue ? `/${venue.id}` : ""}`);
            },
        );
    }

    public updateVenue(venue: IVenue): void {
        this._httpService.post<IVenue>(
            API_ENDPOINTS.updateVenueUrl,
            venue,
            (x: IVenue) => {
                this._layoutService.showInfo("Площадка изменена успешно");
                this._dataStore.venue = x;
                this._venue$.next(Object.assign({}, this._dataStore.venue));
            },
        );
    }

    public reset(): void {
        this._venue$.next(Object.assign({}, this._dataStore.venue));
    }
}
