import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'chat-list',
    templateUrl: './chat-list.component.html',
    styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent {
    nickname: string = ""
    requestOptions: {
        headers: HttpHeaders
    }

    constructor(
        private httpClient: HttpClient,
        private router: Router,
        private cookieService: CookieService) {

        this.nickname = cookieService.get('Nickname')

        var authorizationToken = this.cookieService.get('AuthorizationToken');

        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.loadChats()
    }

    loadChats() {
        var offset = 0;
        var limit = 10;

        this.httpClient
            .get(`https://localhost:44306/rooms/created?offset=${offset}&limit=${limit}`, this.requestOptions)
            .subscribe({
                next: response => this.chatListReceived(response)
            })
    }

    chatListReceived(response: any) {
        alert(JSON.stringify(response.gameSessions))
    }

    logout() {
        this.cookieService.deleteAll();

        this.router.navigate(['']);
    }

    createNewChat() {
        this.httpClient
            .get("https://localhost:44306/rooms/create", this.requestOptions)
            .subscribe({
                next: response => this.successfullyCreated(response),
                error: e => alert(e)
            });
    }

    successfullyCreated(response: any) {
        this.router.navigate([`chats/${response.sessionId}`]);
    }
}
