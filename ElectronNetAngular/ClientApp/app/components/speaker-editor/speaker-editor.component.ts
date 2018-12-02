import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { FILE_SIZES, LABELS, LayoutService, MIME_TYPES, PATTERNS } from "@dotnetru/core";
import { IAcceptedFile, IRejectedFile, RejectionReason } from "@dotnetru/file-dialog";
import { Subscription } from "rxjs";

import { ISpeaker } from "./interfaces";
import { SpeakerEditorService } from "./speaker-editor.service";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [SpeakerEditorService],
    selector: "mtp-speaker-editor",
    styleUrls: ["./speaker-editor.component.css"],
    templateUrl: "./speaker-editor.component.html",
})
export class SpeakerEditorComponent implements OnInit, OnDestroy {
    public readonly LABELS = LABELS;
    public readonly PATTERNS = PATTERNS;
    public readonly AVATAR_MIME_TYPES = MIME_TYPES.AVATAR;
    public readonly AVATAR_MAX_SIZE = FILE_SIZES.AVATAR_MAX_SIZE;

    // todo: create service method getDefaultSpeaker
    public speaker: ISpeaker = {
        companyName: "test company name",
        description: "test description",
        id: "Test-Id",
        name: "Test Name",
    };

    public editMode: boolean = true;

    private _subs: Subscription[] = [];

    constructor(
        private _speakerEditorService: SpeakerEditorService,
        private _layoutService: LayoutService,
        private _activatedRoute: ActivatedRoute,
        private _router: Router,
        private _changeDetectorRef: ChangeDetectorRef,
    ) { }

    public ngOnInit(): void {
        this._subs = [
            this._activatedRoute.params
                .subscribe((params) => {
                    if (typeof params.speakerId === "string" && params.speakerId.length > 0) {
                        this._speakerEditorService.fetchSpeaker(params.speakerId);
                    } else {
                        this.editMode = false;
                    }
                }),
            this._speakerEditorService.speaker$
                .subscribe((speaker: ISpeaker) => {
                    this.speaker = speaker;
                    this._changeDetectorRef.detectChanges();
                }),
        ];
    }

    public ngOnDestroy(): void {
        this._subs.forEach((x) => x.unsubscribe);
    }

    public goBack(): void {
        if (!this._speakerEditorService.hasChanges(this.speaker)) {
            this._router.navigateByUrl("/speaker-list");
        } else {
            this._layoutService.showWarning("Потеря введенных данных предотвращена");
        }
    }

    public save(): void {
        if (this.editMode) {
            this._speakerEditorService.updateSpeaker(this.speaker);
        } else {
            this._speakerEditorService.addSpeaker(this.speaker);
        }
    }

    public reset(): void {
        this._speakerEditorService.reset();
    }

    public onFilesAccepted(files: IAcceptedFile[]): void {
        for (const acceptedFile of files) {
            this._speakerEditorService.storeSpeakerAvatar(this.speaker.id, acceptedFile.file);
        }
    }

    public onFilesRejected(files: IRejectedFile[]): void {
        let msg: string = "Невозможно загрузить файлы.";

        const oversized: IRejectedFile | undefined = files
            .find((x: IRejectedFile) => x.reason === RejectionReason.FileSize);
        if (oversized) {
            msg += ` Максимальный размер не должен превышать [${this.AVATAR_MAX_SIZE}].`;
        }

        // unique file types
        const fileTypes: string[] = files
            .filter((x: IRejectedFile) => x.reason === RejectionReason.FileType)
            .map((x: IRejectedFile) => x.file.type)
            .filter((value: string, index: number, self: string[]) => self.indexOf(value) === index);
        if (fileTypes.length > 0) {
            msg += `Типы файлов не поддерживаются [${fileTypes.join(", ")}].`;
        }

        this._layoutService.showWarning(msg);
    }
}
