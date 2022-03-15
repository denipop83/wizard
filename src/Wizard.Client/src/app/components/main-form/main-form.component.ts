import {Component, OnInit} from '@angular/core';
import {LoginInfoModel} from 'src/app/models/login-info.model';
import {CountryInfoModel} from 'src/app/models/country-info.model';
import {Router} from '@angular/router';
import {Subject} from 'rxjs';
import {StepModel} from 'src/app/models/step.model.';
import {BackendClient} from 'src/app/services/backend.service';
import {AddRegistrationResult} from 'src/app/models/backend.model';

@Component({
  selector: 'app-main-form',
  templateUrl: './main-form.component.html',
  styleUrls: ['./main-form.component.css']
})

export class MainFormComponent implements OnInit {
  eventsSubject: Subject<void> = new Subject<void>();

  loginInfo: LoginInfoModel = {login: '', password: '', isValid: false};
  countryInfo: CountryInfoModel = {countryId: 0, provinceId: 0, isValid: false};

  steps: StepModel[] = [this.loginInfo, this.countryInfo]
  currentStep = 0;

  addRegistrationError = '';

  constructor(private router: Router, private backend: BackendClient) {
  }

  ngOnInit(): void {
  }

  onNextStep() {
    this.eventsSubject.next();

    if (!this.steps[this.currentStep].isValid) {
      return;
    }

    if (this.isLastStep()) {
      let data = {
        email: this.loginInfo.login,
        password: this.loginInfo.password,
        countryId: this.countryInfo.countryId,
        provinceId: this.countryInfo.provinceId
      };

      this.backend.addRegistration(data).subscribe(
        (result: AddRegistrationResult) =>  this.currentStep++,
        error => this.addRegistrationError = (error.error as AddRegistrationResult)?.error
      );
    } else {
      this.currentStep++;
    }
  }

  showButtonLabel() {
    return this.isLastStep() ? 'Save' : 'Proceed';
  }

  isLastStep() {
    return this.steps.length - 1 === this.currentStep;
  }

  onSubmit(): void {
    this.router.navigate(['/complete']);
  }

  isCompleted() {
    return this.currentStep >= this.steps.length;
  }
}
