import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "speakerImageUrl" })
export class SpeakerImageUrlPipe implements PipeTransform {
    public transform(speakerId: string): string {
        return speakerId
            ? `/static/db/speakers/${speakerId}/avatar.jpg`
            : "";
    }
}
