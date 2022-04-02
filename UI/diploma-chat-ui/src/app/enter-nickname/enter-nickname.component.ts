import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, ViewChild, ElementRef } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'enter-nickname',
    templateUrl: './enter-nickname.component.html',
    styleUrls: ['./enter-nickname.component.scss']
})
export class EnterNicknameComponent {

    private requestOptions: {
        headers: HttpHeaders
    };

    constructor(
        private router: Router,
        private cookieService: CookieService,
        private httpClient: HttpClient) {
        var authorizationToken = cookieService.get('AuthorizationToken');

        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.httpClient
            .get("https://localhost:44306/user/profile", this.requestOptions)
            .subscribe({
                next: response => this.success(response),
                error: e => this.fail(e)
            });
    }

    success(response: any) {
        this.cookieService.set('Nickname', response.nickname);
        this.cookieService.set('userId', response.userId);

        this.router.navigate(['chats']);
    }

    fail(response: any) {
        if (response.status != 409) {
            alert("Error")
            this.router.navigate(['']);
        }

        let content = document.getElementById('content')
        if (content != null) {
            content.className = "block"
        }
    }

    onSubmit(form: any) {
        var requestBody = {
            nickname: form.nickname
        }

        this.httpClient
            .post("https://localhost:44306/user/register", requestBody, this.requestOptions)
            .subscribe({
                next: response => this.router.navigate(['chats']),
                error: e => alert(JSON.stringify(e))
            });
    }
}
