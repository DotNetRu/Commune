import { Injectable } from "@angular/core";

import { RejectionReason } from "./enums";
import { IVerifiedFiles } from "./interfaces";

@Injectable()
export class FileService {
    private _supportedFileTypes: string[] = [];
    private _maximumFileSizeInBytes: number = 0;

    public configure(supportFileFormats: string[], maximumFileSize: number): void {
        this._supportedFileTypes = supportFileFormats;
        this._maximumFileSizeInBytes = maximumFileSize;
        console.warn(this._supportedFileTypes);
    }

    public verifyFiles(event: Event): IVerifiedFiles {
        const result: IVerifiedFiles = {
            acceptedFiles: [],
            rejectedFiles: [],
        };

        const target: EventTarget | null = event.target || event.srcElement;
        if (!(target instanceof HTMLInputElement)) {
            return result;
        }

        if (target.files === null || target.files.length === 0) {
            return result;
        }

        for (const currentFile of Array.from(target.files)) {
            if (this._supportedFileTypes.length > 0) {
                const fileTypeIndex: number = this._supportedFileTypes.indexOf(currentFile.type);
                if (fileTypeIndex === -1) {
                    result.rejectedFiles.push({ file: currentFile, reason: RejectionReason.FileType });
                    continue;
                }
            }

            if (this._maximumFileSizeInBytes > 0) {
                if (this._maximumFileSizeInBytes < currentFile.size) {
                    result.rejectedFiles.push({ file: currentFile, reason: RejectionReason.FileSize });
                    continue;
                }
            }

            result.acceptedFiles.push({ file: currentFile });
        }

        return result;
    }
}
