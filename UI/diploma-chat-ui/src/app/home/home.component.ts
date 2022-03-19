import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent {
    constructor(
        private router: Router,
        private cookieService: CookieService) {

        if (cookieService.check('AuthorizationToken')) {
            this.router.navigate(['/chats']);
        }
    }
}
