import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
    creatorNickname: string;

    private chatId: string;

    private requestOptions: {
        headers: HttpHeaders
    }

    constructor(
        private httpClient: HttpClient,
        private cookieService: CookieService,
        router: Router,
        route: ActivatedRoute) {

        this.chatId = route.snapshot.params['chatId']

        var authorizationToken = cookieService.get('AuthorizationToken');
        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.showChatDetails()
        this.joinChatRoom()
    }

    showChatDetails() {
        this.httpClient
            .get(`https://localhost:44306/rooms/${this.chatId}/details`, this.requestOptions)
            .subscribe({
                next: (response: any) => { this.creatorNickname = response.creatorNickname },
                error: e => alert(JSON.stringify(e))
            })

        this.httpClient
            .get(`https://localhost:44373/sessions/${this.chatId}/members`, this.requestOptions)
            .subscribe({
                next: (response: any) => alert(response),
                error: e => alert(e)
            })
    }

    joinChatRoom() {
        this.httpClient
            .get(`https://localhost:44306/rooms/${this.chatId}/join`, this.requestOptions)
            .subscribe({
                error: e => alert(JSON.stringify(e))
            })
    }
}
