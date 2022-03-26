import { Component } from "@angular/core";

@Component({
    selector: 'chat-list-element',
    templateUrl: './chat-list-element.component.html',
    styleUrls: ['./chat-list-element.component.scss']
})
export class ChatListElementComponent {
    public Title: string
    public CreatorNickname: string

    public RoomId: string
}
