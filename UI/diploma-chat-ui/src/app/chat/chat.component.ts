import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
    chatId: string;

    constructor(private route: ActivatedRoute) {
        this.chatId = route.snapshot.params['chatId']
    }
}
