import {createReducer, on} from '@ngrx/store';
import {LoadMatchDatesSuccessAction, LoadSeriesDatesSuccessAction} from "../actions/dates.actions";
import {MatchDate} from "../models/date.model";
import {DateTime} from "luxon";
import {SetLoadingAction} from "../actions/loading.actions";


export const loadingStateReducer = createReducer(
  false,
  on(SetLoadingAction, (state, loading) => {
    return loading.payload
  })
);

