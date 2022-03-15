import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AddRegistrationResult, CountriesResult, ProvincesResult, RegisterInfo} from 'src/app/models/backend.model';

import {environment} from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BackendClient {

  url = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getCountries(): Observable<CountriesResult> {
    return this.http.get<CountriesResult>(`${this.url}/countries`)
  }

  getProvinces(countryId: number) {
    return this.http.get<ProvincesResult>(`${this.url}/provinces`,
      {
        params: new HttpParams().set('countryId', countryId)
      })
  }

  addRegistration(registerInfo: RegisterInfo) {
    return this.http.post<AddRegistrationResult>(`${this.url}/add-registration`, registerInfo);
  }
}


