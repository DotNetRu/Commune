import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "friendImageUrl" })
export class FriendImageUrlPipe implements PipeTransform {
    public transform(friendId: string): string {
        return friendId
            ? `/static/db/friends/${friendId}/logo.png`
            : "";
    }
}
