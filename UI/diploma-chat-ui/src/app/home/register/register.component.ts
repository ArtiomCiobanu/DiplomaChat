import { Component } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
    selector: 'register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

    constructor(private router: Router) { }

    onSubmit(form: NgForm) {
        alert("Submit: " + form.value)

        this.router.navigate(['/chats']);
    }
}
