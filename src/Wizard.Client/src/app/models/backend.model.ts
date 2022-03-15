export interface CountriesResult {
  countries: Country[]
}

export interface ProvincesResult {
  provinces: Province[]
}

export interface Country {
  id: number;
  name: string;
}

export interface Province {
  id: number;
  name: string;
}

export interface RegisterInfo {
  email: string;
  password: string;
  countryId: number;
  provinceId: number;
}

export interface AddRegistrationResult {
  email: string;
  error: string;
}
