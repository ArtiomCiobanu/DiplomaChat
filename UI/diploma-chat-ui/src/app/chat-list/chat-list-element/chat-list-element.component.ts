import { Component } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'chat-list-element',
    templateUrl: './chat-list-element.component.html',
    styleUrls: ['./chat-list-element.component.scss']
})
export class ChatListElementComponent {
    public Title: string
    public CreatorNickname: string

    public roomId: string

    private requestOptions: {
        headers: HttpHeaders
    }

    constructor(
        private httpClient: HttpClient,
        private router: Router,
        cookieService: CookieService,
        route: ActivatedRoute) {
        var authorizationToken = cookieService.get('AuthorizationToken');

        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }
        this.roomId = route.snapshot.params['chatId']
    }

    onClick() {
        this.joinChatRoom()
        this.router.navigate([`chats/${this.roomId}`]);
    }

    joinChatRoom() {
        this.httpClient
            .get(`https://localhost:44306/rooms/${this.roomId}/join`, this.requestOptions)
            .subscribe({
                error: e => alert(JSON.stringify(e))
            })
    }
}
