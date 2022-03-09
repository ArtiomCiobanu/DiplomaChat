import { Component, Input } from "@angular/core";

@Component({
    selector: 'proper-chat-message',
    templateUrl: './proper-chat-message.component.html',
    styleUrls: ['./proper-chat-message.component.scss']
})
export class ProperChatMessageComponent {
    @Input() text: string = "";
}
