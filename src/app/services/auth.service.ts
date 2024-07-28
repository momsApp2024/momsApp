import { Injectable } from '@angular/core';
import firebase from 'firebase/compat/app';
import 'firebase/compat/auth';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private afAuth: AngularFireAuth,
    private http: HttpClient
  ) { }
  private apiUrl = 'http://localhost:5140/api/UserProfile';
  private districtsApiUrl = 'http://localhost:5140/api/userprofile/districts';  // Adjust URL as needed

  public currentUserEmail: string | null = null;

  register(user: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, user);
  }

  getProfile(email: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${email}`);
  }

  updateProfile(email: string, profile: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${email}`, profile);
  }
  // login(email: string, password: string): Observable<any> {
  //   return this.http.post<any>(`${this.apiUrl}/login`, { email, password });
  // }
  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password }).pipe(
      catchError(this.handleError)
    );
  }
  getDistricts(): Observable<any[]> {
    return this.http.get<any[]>(this.districtsApiUrl)
    .pipe(
      tap(data => console.log('Data received from API:', data)),  // Log the data
      catchError(this.handleError)
    );  }
  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';
    if (error.status === 401) {
      errorMessage = 'Invalid credentials';
    } else if (error.status === 404) {
      errorMessage = 'New user';
    }
    return throwError(errorMessage);
  }


  externalLogin(email: string, firstName: string, lastName: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/external-login`, { email, firstName, lastName });
  }

  setCurrentUser(email: string): void {
    this.currentUserEmail = email;
  }

  getCurrentUser(): string | null {
    return this.currentUserEmail;
  }
  // updateProfile(id: number, updates: any): Observable<void> {
  //   return this.http.patch<void>(`${this.apiUrl}/${id}`, updates);
  // }
  async signInWithGoogle() {
    try {
      const provider = new firebase.auth.GoogleAuthProvider();
      const credential = await this.afAuth.signInWithPopup(provider);
      return credential.user;
    } catch (error) {
      console.error('Error signing in with Google:', error);
      throw error;
    }
  }
  async signInWithFacebook() {
    try {
      const provider = new firebase.auth.FacebookAuthProvider();
      await this.afAuth.signInWithPopup(provider);
    } catch (error: unknown) {
      if (this.isAuthError(error)) {
        if (error.code === 'auth/account-exists-with-different-credential') {
          const credentialError = error as firebase.auth.AuthError & {
            email?: string;
            credential?: firebase.auth.AuthCredential;
          };
          const email = credentialError.email;
          const pendingCredential = credentialError.credential;

          if (email && pendingCredential) {
            const signInMethods = await this.afAuth.fetchSignInMethodsForEmail(email);

            if (signInMethods.length > 0) {
              const existingProvider = this.getProvider(signInMethods[0]);
              const result = await this.afAuth.signInWithPopup(existingProvider);
              await result.user?.linkWithCredential(pendingCredential);
              console.log('Accounts linked successfully');
            } else {
              console.error('No existing providers found');
            }
          } else {
            console.error('Email or pending credential is undefined');
          }
        } else {
          console.error('Error logging in with Facebook:', error);
        }
      } else {
        console.error('Unknown error:', error);
      }
    }
  }

  isAuthError(error: unknown): error is firebase.auth.AuthError {
    return typeof error === 'object' && error !== null && 'code' in error;
  }

  getProvider(providerId: string) {
    switch (providerId) {
      case firebase.auth.GoogleAuthProvider.PROVIDER_ID:
        return new firebase.auth.GoogleAuthProvider();
      case firebase.auth.FacebookAuthProvider.PROVIDER_ID:
        return new firebase.auth.FacebookAuthProvider();
      default:
        throw new Error(`No provider implemented for ${providerId}`);
    }
  }
  async signOut() {
    try {
      await this.afAuth.signOut();
    } catch (error) {
      console.error('Error signing out:', error);
      throw error;
    }
  }
}
