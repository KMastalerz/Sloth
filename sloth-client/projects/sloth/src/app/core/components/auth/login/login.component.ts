import { Component, inject, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, LoginParam } from 'sloth-http';
import { ButtonComponent, ButtonLinkComponent, PasswordInputComponent, TextInputComponent } from 'sloth-ui';
import { AuthStateService } from 'sloth-utilities';

@Component({
    selector: 'app-login',
    imports: [TextInputComponent, PasswordInputComponent, ButtonComponent, 
        ButtonLinkComponent, ReactiveFormsModule],
    templateUrl: './login.component.html',
    styleUrl: './login.component.scss'
})
export class LoginComponent implements OnInit {
    private authService = inject(AuthService);
    private authStateService = inject(AuthStateService);
    private router = inject(Router);

    protected displayLoginError = signal<boolean>(false);
    protected displayServerError = signal<boolean>(false);

    protected loginForm = new FormGroup({
        login: new FormControl('', {
            validators: [Validators.required]
        }),
        password: new FormControl('', {
            validators: [Validators.required]
        })
    })

    ngOnInit(): void {
        console.log('[LoginComponent] token cleared');
        this.authStateService.clearAccessTokenResponse();
    }

    protected async onSubmit(): Promise<void> {
        console.log('[LoginComponent]', this.loginForm);
        
        if(this.loginForm.invalid) {
            this.displayLoginError.set(true);
            return;
        }

        this.displayLoginError.set(false);
        this.displayServerError.set(false);

        const loginParam: LoginParam = {
            login: this.loginForm.value.login!,
            password: this.loginForm.value.password!
        }

        const accessTokenResponse = await this.authService.loginAsync(loginParam);

        if(accessTokenResponse.success) {
            this.authStateService.casheAccessTokenResponse(accessTokenResponse.data);
            this.router.navigate(['']);
        }
        else if(accessTokenResponse.responseCode < 500) {
            this.displayLoginError.set(true);
        }
        else {
             this.displayServerError.set(true);
        }
    }
}
