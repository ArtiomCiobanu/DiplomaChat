import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
    chatId: string;

    CreatorNickname: string;

    constructor(
        route: ActivatedRoute,
        httpClient: HttpClient) {

        this.chatId = route.snapshot.params['chatId']

        httpClient.get(`https://localhost:44306/user/${}/create`)
    }
}
