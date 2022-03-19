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
        // const token = 'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJBY2NvdW50SWQiOiI3Y2ZhZTJiZC05NDUzLTQ4NjgtZmRhZi0wOGRhMDQwYjM4YTgiLCJuYmYiOjE2NDcwNzc2MDIsImV4cCI6MTY0NzY4MjQwMiwiaXNzIjoiRGlwbG9tYUNoYXQiLCJhdWQiOiJEaXBsb21hQ2hhdCJ9.OQLlOXPnrrOXwD1M_ftTNUCeDLE1TcA6RLZS_vNno50'

        // const headerDictionary = {
        //     'Authorization': `Bearer ${token}`
        // }

        // const headerDictionary = {
        //     'Content-Type': 'application/json',
        //     'Accept': 'application/json'
        // }

        // var requestOptions = {
        //     headers: new HttpHeaders(headerDictionary)
        // }

        // var accountDetailsObservable = this.httpClient
        //     .get("https://localhost:44317/account/details", requestOptions)
        //     .subscribe((value: any) => {

        //         alert("First Name: " + value.firstName);
        //         alert(JSON.stringify(value));
        //     });

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
