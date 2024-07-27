import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent  implements OnInit {
  registrationForm!: FormGroup;
  districts: any[] = [];

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.registrationForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      // addressId: ['', Validators.required],
      childNumber: ['', Validators.required],
      childGender: ['', Validators.required],
      childDateOfBirth: ['', Validators.required],
      phone: ['', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      street: ['', Validators.required],
      houseNumber: ['', Validators.required],
      cityId: ['', Validators.required],
      districtId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.authService.getDistricts().subscribe(data => {
      this.districts = data;
    });
  }
  

  completeRegistration(): void {
    if (this.registrationForm.valid) {
      this.authService.register(this.registrationForm.value).subscribe(() => {
        this.router.navigate(['/profile']);
      });
    }
  }
  profile: any = {};

//   completeRegistration() {
//     const email = this.authService.getCurrentUser();
//     if (email) {
//       this.authService.updateProfile(email, this.profile).subscribe(() => {
//         this.router.navigate(['/profile']);
//       });
//     }
// }
    

}


