import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
    firstName: string = "";
    lastName: string = "";
    email: string = "";
    phone: string = "";

    constructor(
        private router: Router,
        private httpClient: HttpClient) { }

    //onSubmit(form: NgForm) {
    onSubmit(form: any) {
        // const token = 'eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJBY2NvdW50SWQiOiI3Y2ZhZTJiZC05NDUzLTQ4NjgtZmRhZi0wOGRhMDQwYjM4YTgiLCJuYmYiOjE2NDcwNzc2MDIsImV4cCI6MTY0NzY4MjQwMiwiaXNzIjoiRGlwbG9tYUNoYXQiLCJhdWQiOiJEaXBsb21hQ2hhdCJ9.OQLlOXPnrrOXwD1M_ftTNUCeDLE1TcA6RLZS_vNno50'

        // const headerDictionary = {
        //     'Authorization': `Bearer ${token}`
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

        //var email = form.controls["firstName"]
        alert(form.email)

        // var registrationRequestBody = {
        //     email: "string",
        //     password: "string",
        //     firstName: "string",
        //     lastName: "string"
        // }

        // var a = this.httpClient
        //     .post("https://localhost:44317/account/register", registrationRequestBody);

        this.router.navigate(['/chats']);
    }
}
