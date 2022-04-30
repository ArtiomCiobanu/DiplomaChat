import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    constructor(
        private router: Router,
        private httpClient: HttpClient,
        private cookieService: CookieService) { }

    onSubmit(form: any) {
        var loginRequestBody = {
            email: form.email,
            password: form.password
        }

        this.httpClient
            .post("https://localhost:44317/account/login", loginRequestBody)
            .subscribe({
                next: v => this.saveTokenAndLoadChat(v),
                error: e => { this.fail(e) }
            })
    }

    saveTokenAndLoadChat(response: any) {
        this.cookieService.set('AuthorizationToken', response.token);

        this.router.navigate(['/enter-nickname']);
    }

    fail(response: any) {
        alert(`Status: ${response.status}, Message: ${response.error.message}`)
    }
}
