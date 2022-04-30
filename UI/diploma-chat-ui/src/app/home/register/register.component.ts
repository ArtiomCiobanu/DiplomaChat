import { HttpClient } from "@angular/common/http";
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

    constructor(
        private router: Router,
        private httpClient: HttpClient,
        private cookieService: CookieService) { }

    onSubmit(form: any) {
        var registrationRequestBody = {
            email: form.email,
            password: form.password,
            firstName: form.firstName,
            lastName: form.lastName
        }

        this.httpClient
            .post("https://localhost:44317/account/register", registrationRequestBody)
            .subscribe({
                next: () => this.success(registrationRequestBody),
                error: e => this.fail(e),
            })
    }

    success(userCredentials: {
        email: string,
        password: string
    }) {
        this.httpClient
            .post("https://localhost:44317/account/login", userCredentials)
            .subscribe({
                next: response => this.saveTokenAndLoadChat(response),
                error: error => { this.fail(error) }
            })
    }

    saveTokenAndLoadChat(response: any) {
        this.cookieService.set('AuthorizationToken', response.token)

        this.router.navigate(['/enter-nickname']);
    }

    fail(response: any) {
        alert(`Error. Response: ${JSON.stringify(response)}`)
    }
}
