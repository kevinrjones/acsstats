import {Team} from './team.model'
import {Country} from './country.model'
import { Ground } from './ground.model'
import {MatchDate} from "./date.model";
import {MatchSubTypeModel} from "./match-sub-type.model";
import {FindRecords} from "./find-records.model";
import {ErrorDetails} from "./error.model";

export interface AppState {
  teams: Team[],
  countries: Country[],
  grounds: Ground[],
  seriesDates: string[],
  matchDates: MatchDate[],
  matchSubTypes: MatchSubTypeModel[],
  formState: FindRecords,
  errorState: ErrorDetails,
  loading: boolean
}
