import {Team} from './team.model'
import {Country} from './country.model'
import { Ground } from './ground.model'
import {MatchDate} from "./date.model";

export interface AppState {
  teams: Team[],
  countries: Country[],
  grounds: Ground[],
  seriesDates: string[],
  matchDates: MatchDate[],
}