import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { FILE_SIZES, LABELS, LayoutService, MIME_TYPES, PATTERNS } from "@dotnetru/core";
import { City } from "@dotnetru/shared/city-select";
import { Subscription } from "rxjs";

import { IVenue } from "./interfaces";
import { VenueEditorService } from "./venue-editor.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [VenueEditorService],
    selector: "mtp-venue-editor",
    styleUrls: ["./venue-editor.component.css"],
    templateUrl: "./venue-editor.component.html",
})
export class VenueEditorComponent implements OnInit, OnDestroy {
    public readonly LABELS = LABELS;
    public readonly PATTERNS = PATTERNS;

    // todo: create service method getDefaultVenue
    public venue: IVenue = { id: "", city: City.Spb, name: "", address: "", mapUrl: "" };

    public editMode: boolean = true;

    private _subs: Subscription[] = [];

    constructor(
        private _venueEditorService: VenueEditorService,
        private _layoutService: LayoutService,
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._activatedRoute.params
                .subscribe((params: Params) => {
                    if (typeof params.venueId === "string" && params.venueId.length > 0) {
                        this._venueEditorService.fetchVenue(params.venueId);
                    } else {
                        this.editMode = false;
                    }
                }),
            this._venueEditorService.venue$
                .subscribe((venue: IVenue) => {
                    this.venue = venue;
                    this._changeDetectorRef.detectChanges();
                }),
        ];
    }

    public ngOnDestroy(): void {
        this._subs.forEach((x) => x.unsubscribe);
    }

    public goBack(): void {
        if (!this._venueEditorService.hasChanges(this.venue)) {
            this._router.navigateByUrl("/venue-list");
        } else {
            this._layoutService.showWarning("Потеря введенных данных предотвращена");
        }
    }

    public save(): void {
        if (this.editMode) {
            this._venueEditorService.updateVenue(this.venue);
        } else {
            this._venueEditorService.addVenue(this.venue);
        }
    }

    public reset(): void {
        this._venueEditorService.reset();
    }

    // public onSpeakerSelected(row: IAutocompleteRow, index: number): void {
    //     this.venue.speakerIds[index] = { speakerId: row.id };
    // }

    // public removeSpeaker(index: number): void {
    //     this.venue.speakerIds.splice(index, 1);
    // }

    // public addSpeaker(): void {
    //     this.venue.speakerIds.push({ speakerId: "" });
    // }
}
