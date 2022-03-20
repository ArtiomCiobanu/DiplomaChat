import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
    chatId: string

    creatorNickname: string = ""

    constructor(
        route: ActivatedRoute,
        httpClient: HttpClient,
        cookieService: CookieService) {

        this.chatId = route.snapshot.params['chatId']

        var authorizationToken = cookieService.get('AuthorizationToken');

        var requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        httpClient
            .get(`https://localhost:44306/rooms/${this.chatId}/details`, requestOptions)
            .subscribe({
                next: response => this.success(response),
                error: e => alert(JSON.stringify(e))
            })
    }

    success(response: any) {
        this.creatorNickname = response.creatorNickname
    }

}
