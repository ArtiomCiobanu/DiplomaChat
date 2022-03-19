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
    FullName: string = ""

    constructor(
        private router: Router,
        private cookieService: CookieService,
        private httpClient: HttpClient) {

        var authorizationToken = cookieService.get('AuthorizationToken');

        var requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.httpClient
            .get("https://localhost:44317/account/details", requestOptions)
            .subscribe({
                next: response => this.setFullName(response)
            });
    }

    setFullName(response: any) {
        this.FullName = `${response.firstName} ${response.lastName}`
    }

    logout() {
        this.cookieService.delete('AuthorizationToken');

        this.router.navigate(['']);        
    }
}
