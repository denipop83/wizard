import {StepModel} from "./step.model.";

export interface CountryInfoModel extends StepModel {
  countryId: number;
  provinceId: number;
}
