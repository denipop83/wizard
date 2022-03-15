import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainFormComponent } from './components/main-form/main-form.component'
import { LoginInfoComponent } from './components/login/login.component';
import { CountryComponent } from './components/country/country.component';

import { MustMatchDirective } from './validators/mustmatch.validator';
import { ValidateEmailDirective } from './validators/email.validator';
import { ValidatePassDirective } from './validators/pass.validator';
import { ValidateDefaultnessDirective } from './validators/notdefault.validator';
import { SuccessComponent } from './components/success/success.component';

@NgModule({
  declarations: [
    AppComponent,
    MainFormComponent,
    LoginInfoComponent,
    CountryComponent,
    MustMatchDirective,
    ValidateEmailDirective,
    ValidatePassDirective,
    ValidateDefaultnessDirective,
    SuccessComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
