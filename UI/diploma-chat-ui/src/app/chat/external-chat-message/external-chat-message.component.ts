import { Component, Input } from "@angular/core";

@Component({
    selector: 'external-chat-message',
    templateUrl: './external-chat-message.component.html',
    styleUrls: ['./external-chat-message.component.scss']
})
export class ExternalChatMessageComponent {
    @Input() text: string = "";
    @Input() sender: string = "";
}
