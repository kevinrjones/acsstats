import {createReducer, on} from '@ngrx/store';
import {LoadMatchDatesSuccessAction, LoadSeriesDatesSuccessAction} from "../actions/dates.actions";
import {MatchDate} from "../models/date.model";
import {DateTime} from "luxon";
import {LoadRecordSummariesAction, LoadRecordSummariesSuccessAction} from "../actions/recordsummary.actions";


export const initialSeriesDatesByMatchTypeState  = {};
export const recordSummaryReducer = createReducer(
  initialSeriesDatesByMatchTypeState,
  on(LoadRecordSummariesSuccessAction, (state, summary) => {
    return summary.payload
  })
);


