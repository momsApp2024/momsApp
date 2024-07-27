import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import firebase from 'firebase/compat/app';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  // constructor(private authService: AuthService,
  //   private afAuth: AngularFireAuth
  // ) { }
  email!: string;
  password!: string;
  constructor(private authService: AuthService, private router: Router) { }


  ngOnInit(): void {


  }
  // updates: any = {};


  // updateProfile() {
  //   const userId = 1; // Replace with the actual user ID
  //   this.authService.updateProfile(userId, this.updates).subscribe(() => {
  //     console.log('Profile updated successfully');
  //   });
  // }
  async signInWithGoogle() {
    try {
      await this.authService.signInWithGoogle();
      // Optionally, you can navigate to another page or perform other actions upon successful sign-in.
    } catch (error) {
      // Handle error
    }
  }
  // async loginWithFacebook() {
  //   const provider = new firebase.auth.FacebookAuthProvider();
  //   provider.addScope('email');
  //   try {
  //     const result = await this.afAuth.signInWithPopup(provider);
  //     // Handle the result as needed
  //   } catch (error) {
  //     console.error('Error logging in with Facebook:', error);
  //   }
  // }
  // async signOut() {
  //   try {
  //     await this.authService.signOut();
  //     // Optionally, you can navigate to another page or perform other actions upon successful sign-out.
  //   } catch (error) {
  //     // Handle error

  //   }

  // }


  // login() {
  //   this.authService.login(this.email, this.password).subscribe(response => {
  //     if (response.RequiresRegistration) {
  //       this.router.navigate(['./compoonents/registration']);
  //     } else {
  //       this.router.navigate(['./compoonents/profile']);
  //     }
  //   });
  // }
  login() {
    this.authService.login(this.email, this.password).subscribe(
      response => {
        if (response.RequiresRegistration) {
          this.router.navigate(['./components/registration']);
        } else {
          this.router.navigate(['./components/profile']);
        }
      },
      error => {
        if (error === 'Invalid credentials') {
          // Handle invalid credentials case
          alert('Invalid password. Please try again.');
        } else if (error === 'New user') {
          // Handle new user case
          this.router.navigate(['./components/registration']);
          //this.router.navigate(['./components/registration']);
        } else {
          // Handle other errors
          alert('An unknown error occurred.');
        }
      }
    );
  }

  loginWithGoogle() {
    // Implement Google login logic and then call externalLogin with the received user info
    const googleUser = { email: 'google@example.com', firstName: 'Google', lastName: 'User' };
    this.authService.externalLogin(googleUser.email, googleUser.firstName, googleUser.lastName).subscribe(response => {
      if (response.RequiresRegistration) {
        this.router.navigate(['./compoonents/registration']);
      } else {
        this.router.navigate(['./compoonents/profile']);
      }
    });
  }

  loginWithFacebook() {
    // Implement Facebook login logic and then call externalLogin with the received user info
    const facebookUser = { email: 'facebook@example.com', firstName: 'Facebook', lastName: 'User' };
    this.authService.externalLogin(facebookUser.email, facebookUser.firstName, facebookUser.lastName).subscribe(response => {
      if (response.RequiresRegistration) {
        this.router.navigate(['./compoonents/registration']);
      } else {
        this.router.navigate(['./compoonents/profile']);
      }
    });
  }

} 