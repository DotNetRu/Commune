import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { FILE_SIZES, LABELS, LayoutService, MIME_TYPES, PATTERNS } from "@dotnetru/core";
import { IAutocompleteRow } from "@dotnetru/shared/autocomplete";
import { Subscription } from "rxjs";

import { ISpeakerReference, ITalk } from "./interfaces";
import { TalkEditorService } from "./talk-editor.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [TalkEditorService],
    selector: "mtp-talk-editor",
    styleUrls: ["./talk-editor.component.css"],
    templateUrl: "./talk-editor.component.html",
})
export class TalkEditorComponent implements OnInit, OnDestroy {
    public readonly LABELS = LABELS;
    public readonly PATTERNS = PATTERNS;
    public readonly AVATAR_MIME_TYPES = MIME_TYPES.AVATAR;
    public readonly AVATAR_MAX_SIZE = FILE_SIZES.AVATAR_MAX_SIZE;

    // todo: create service method getDefaultTalk
    public talk: ITalk = { id: "", speakerIds: [], title: "", description: "" };

    public editMode: boolean = true;

    private _subs: Subscription[] = [];

    constructor(
        private _talkEditorService: TalkEditorService,
        private _layoutService: LayoutService,
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._activatedRoute.params
                .subscribe((params: Params) => {
                    if (typeof params.talkId === "string" && params.talkId.length > 0) {
                        this._talkEditorService.fetchTalk(params.talkId);
                    } else {
                        this.editMode = false;
                    }
                }),
            this._talkEditorService.talk$
                .subscribe((talk: ITalk) => {
                    this.talk = talk;
                    this._changeDetectorRef.detectChanges();
                }),
        ];
    }

    public ngOnDestroy(): void {
        this._subs.forEach((x) => x.unsubscribe);
    }

    public goBack(): void {
        if (!this._talkEditorService.hasChanges(this.talk)) {
            this._router.navigateByUrl("/talk-list");
        } else {
            this._layoutService.showWarning("Потеря введенных данных предотвращена");
        }
    }

    public save(): void {
        if (this.editMode) {
            this._talkEditorService.updateTalk(this.talk);
        } else {
            this._talkEditorService.addTalk(this.talk);
        }
    }

    public reset(): void {
        this._talkEditorService.reset();
    }

    public onSpeakerSelected(row: IAutocompleteRow, index: number): void {
        this.talk.speakerIds[index] = { speakerId: row.id };
    }

    public removeSpeaker(index: number): void {
        this.talk.speakerIds.splice(index, 1);
    }

    public addSpeaker(): void {
        this.talk.speakerIds.push({ speakerId: "" });
    }
}
