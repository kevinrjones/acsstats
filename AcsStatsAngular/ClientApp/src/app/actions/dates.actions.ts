import {createAction, props} from '@ngrx/store'
import {Team} from "../models/team.model";
import {MatchDate} from "../models/date.model";

export const LOAD_SERIESDATES = 'LOAD_SERIESDATES;';
export const LOAD_SERIESDATES_SUCCESS = 'LOAD_SERIESDATES_SUCCESS;';
export const LOAD_SERIESDATES_FAILURE = 'LOAD_SERIESDATES_FAILURE;';

export const LoadSeriesDatesAction = createAction(LOAD_SERIESDATES, props<{ payload: string }>())
export const LoadSeriesDatesSuccessAction = createAction(LOAD_SERIESDATES_SUCCESS, props<{ payload: string[] }>())
export const LoadSeriesDatesFailureAction = createAction(LOAD_SERIESDATES_FAILURE)

export const LOAD_MATCHDATES = 'LOAD_MATCHDATES;';
export const LOAD_MATCHDATES_SUCCESS = 'LOAD_MATCHDATES_SUCCESS;';
export const LOAD_MATCHDATES_FAILURE = 'LOAD_MATCHDATES_FAILURE;';

export const LoadMatchDatesAction = createAction(LOAD_MATCHDATES, props<{ payload: string }>())
export const LoadMatchDatesSuccessAction = createAction(LOAD_MATCHDATES_SUCCESS, props<{ payload: MatchDate[] }>())
export const LoadMatchDatesFailureAction = createAction(LOAD_MATCHDATES_FAILURE)


