import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "requiredErrorMessage" })
export class RequiredErrorMessagePipe implements PipeTransform {
    public transform = (fieldName: string): string => `"${fieldName}" не заполнено`;
}
