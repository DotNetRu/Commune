import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "patternErrorMessage" })
export class PatternErrorMessagePipe implements PipeTransform {
    public transform = (pattern: string): string => `Регулярка не пройдена: ${pattern}`;
}
