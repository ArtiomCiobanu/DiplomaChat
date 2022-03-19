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
    Nickname: string = ""

    constructor(
        private httpClient: HttpClient,
        private router: Router,
        private cookieService: CookieService) {

        this.Nickname = cookieService.get('Nickname')
    }

    logout() {
        this.cookieService.deleteAll();

        this.router.navigate(['']);
    }

    createNewChat() {
        var authorizationToken = this.cookieService.get('AuthorizationToken');

        var requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.httpClient
            .get("https://localhost:44306/rooms/create", requestOptions)
            .subscribe({
                next: response => alert(response),
                error: e => alert(e)
            });

        //this.router.navigate(['chats/new']);
    }
}
