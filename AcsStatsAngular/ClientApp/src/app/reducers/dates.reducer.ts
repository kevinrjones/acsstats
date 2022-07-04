import {createReducer, on} from '@ngrx/store';
import {LoadMatchDatesSuccessAction, LoadSeriesDatesSuccessAction} from "../actions/dates.actions";
import {MatchDate} from "../models/date.model";
import {DateTime} from "luxon";


export const initialSeriesDatesByMatchTypeState: string[]  = [];
export const seriesDatesReducer = createReducer(
  initialSeriesDatesByMatchTypeState,
  on(LoadSeriesDatesSuccessAction, (state, dates) => {
    return dates.payload
  })
);

export const initialMatchDatesByMatchTypeState: MatchDate[]  = [];
export const matchDatesReducer = createReducer(
  initialMatchDatesByMatchTypeState,
  on(LoadMatchDatesSuccessAction, (state, newState) => {
    let dates: MatchDate[] = newState.payload
    return dates.map(it => {
      return {date: DateTime.fromFormat(it.date, "d LLLL yyyy").toISODate(), dateOffset: it.dateOffset, matchType: it.matchType}
    })
  })
);

