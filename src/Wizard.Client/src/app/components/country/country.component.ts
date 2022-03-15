import {Component, OnInit, Input, ViewChild} from '@angular/core';
import {BackendClient} from 'src/app/services/backend.service';
import {CountryInfoModel} from 'src/app/models/country-info.model';
import {Observable, Subscription} from 'rxjs';
import {NgForm} from '@angular/forms';
import {Country, Province} from 'src/app/models/backend.model';

@Component({
  selector: 'app-country-component',
  templateUrl: './country.component.html',
  styleUrls: ['./country.component.css']
})
export class CountryComponent implements OnInit {
  private validateSubscription!: Subscription;

  countries: Country[] = [];
  countriesToProvinces: { [key: string]: Province[] } = {};

  @Input() countryInfo!: CountryInfoModel;
  @Input() validate!: Observable<void>;

  showErrors = false;

  @ViewChild(NgForm) form!: NgForm;

  constructor(private backend: BackendClient) {
  }

  ngOnInit(): void {
    this.getCountries();
    this.countryInfo.countryId = 0;
    this.countryInfo.provinceId = 0;

    this.validateSubscription = this.validate.subscribe(() => {
      this.showErrors = true;
    });
  }

  getCountries(): void {
    this.backend.getCountries().subscribe(data => {
      this.countries = data.countries;
    });
  }

  ngAfterViewInit() {
    this.form.statusChanges?.subscribe(
      result => this.countryInfo.isValid = result === 'VALID'
    );
  }

  onCountryChange(countryId: number) {
    this.countryInfo.provinceId = 0;

    if (this.countriesToProvinces[countryId]) {
      return;
    }

    this.backend.getProvinces(countryId).subscribe(data => {
      this.countriesToProvinces[countryId] = data.provinces;
    });
  }
}
