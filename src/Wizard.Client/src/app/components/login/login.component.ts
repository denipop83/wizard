import {Component, OnInit, Input} from '@angular/core';
import {LoginInfoModel} from 'src/app/models/login-info.model';
import {FormBuilder, NgForm} from '@angular/forms';
import {Observable, Subscription} from 'rxjs';
import {ViewChild} from '@angular/core';

@Component({
  selector: 'app-login-component',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginInfoComponent implements OnInit {

  private validateSubscription!: Subscription;

  @Input() loginInfo!: LoginInfoModel;
  @Input() validate!: Observable<void>;

  confirmedPassword = '';

  showErrors = false;

  @ViewChild(NgForm) form!: NgForm;

  constructor(public formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.validateSubscription = this.validate.subscribe(() => {
      this.showErrors = true;
    });
  }

  ngAfterViewInit() {
    this.form.statusChanges?.subscribe(
      result => this.loginInfo.isValid = result === 'VALID'
    );
  }

  onLoginInput(login: string) {
    this.loginInfo.login = login;
  }

  onLoginChanged(login: string) {
    this.loginInfo.login = login;
  }
}
