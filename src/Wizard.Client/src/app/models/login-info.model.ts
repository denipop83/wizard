import {StepModel} from "./step.model.";

export interface LoginInfoModel extends StepModel {
  login: string;
  password: string;
}
