import { RejectionReason } from "./enums";

export interface IAcceptedFile {
    file: File;
}

export interface IRejectedFile extends IAcceptedFile {
    reason: RejectionReason;
}

export interface IVerifiedFiles {
    acceptedFiles: IAcceptedFile[];
    rejectedFiles: IRejectedFile[];
}
