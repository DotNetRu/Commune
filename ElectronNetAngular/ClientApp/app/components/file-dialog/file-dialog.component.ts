import { ChangeDetectionStrategy, Component, ElementRef, EventEmitter, Input, Output, ViewChild } from "@angular/core";

import { FileService } from "./file.service";
import { IAcceptedFile, IRejectedFile, IVerifiedFiles } from "./interfaces";

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [FileService],
    selector: "mtp-file-dialog",
    templateUrl: "./file-dialog.component.html",
})
export class FileDialogComponent {
    @ViewChild("inputFile") public nativeInputFile?: ElementRef;

    @Input() public disabled: boolean = false;
    @Input() public accept: string = "";
    @Input() public multiple: boolean = true;
    @Input() public maxFileSize: number = 0;

    @Output() public readonly filesAccepted: EventEmitter<IAcceptedFile[]> = new EventEmitter<IAcceptedFile[]>();
    @Output() public readonly filesRejected: EventEmitter<IRejectedFile[]> = new EventEmitter<IRejectedFile[]>();

    constructor(
        private _fileService: FileService,
    ) { }

    public onNativeInputFileSelect(event: Event): void {
        this._fileService.configure(this.accept.split(",").filter((x) => x !== ""), this.maxFileSize);
        const result: IVerifiedFiles = this._fileService.verifyFiles(event);

        this.filesAccepted.emit(result.acceptedFiles);
        this.filesRejected.emit(result.rejectedFiles);

        if (this.nativeInputFile) {
            this.nativeInputFile.nativeElement.value = "";
        }

        this.preventAndStopEventPropagation(event);
    }

    public uploadFile(): void {
        this.nativeInputFile!.nativeElement.click();
    }

    private preventAndStopEventPropagation(event: Event): void {
        event.preventDefault();
        event.stopPropagation();
    }
}
