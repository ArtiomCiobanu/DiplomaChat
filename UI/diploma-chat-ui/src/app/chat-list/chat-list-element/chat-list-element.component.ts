import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'chat-list-element',
    templateUrl: './chat-list-element.component.html',
    styleUrls: ['./chat-list-element.component.scss']
})
export class ChatListElementComponent {
    public Title: string
    public CreatorNickname: string

    public RoomId: string

    constructor(private router: Router) {
    }

    onClick() {
        this.router.navigate([`chats/${this.RoomId}`]);
    }
}
