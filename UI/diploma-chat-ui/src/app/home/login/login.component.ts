import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent {
    constructor(
        private router: Router,
        private httpClient: HttpClient) { }

    onSubmit(form: any) {
        var loginRequestBody = {
            email: form.email,
            password: form.password
        }

        this.httpClient
            .post("https://localhost:44317/account/login", loginRequestBody)
            .subscribe({
                next: v => this.success(v),
                error: e => { this.fail(e) }
            })
    }

    success(response: any) {
        alert(`token: ${JSON.stringify(response)}`)
        this.router.navigate(['/chats']);
    }

    fail(response: any) {
        alert(`Status: ${response.status}, Message: ${response.error.message}`)

    }
}
