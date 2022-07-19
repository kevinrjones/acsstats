import {createAction, props} from '@ngrx/store'
import {FindScorecard} from '../models/scorecard-search.model';
import {ScorecardListItem} from '../models/scorecard-list-item.model';
import {Scorecard} from '../models/scorecard.model';
import {ErrorDetails} from "../../../models/error.model";



const LOAD_SCORECARD_LIST = 'LOAD_SCORECARD_LIST;';
const LOAD_SCORECARD_LIST_SUCCESS = 'LOAD_SCORECARD_LIST_SUCCESS;';
const LOAD_SCORECARD_LIST_FAILURE = 'LOAD_SCORECARD_LIST_FAILURE;';
const LOAD_SCORECARD_LIST_TOURNAMENT = 'LOAD_SCORECARD_LIST_TOURNAMENT;';

export const LoadScorecardListAction = createAction(LOAD_SCORECARD_LIST, props<{ payload: FindScorecard }>())
export const LoadScorecardTournamentListAction = createAction(LOAD_SCORECARD_LIST_TOURNAMENT, props<{ payload: string }>())
export const LoadScorecardListSuccessAction = createAction(LOAD_SCORECARD_LIST_SUCCESS, props<{ payload: ScorecardListItem[] }>())
export const LoadScorecardListFailureAction = createAction(LOAD_SCORECARD_LIST_FAILURE, props<{ payload: ErrorDetails }>())


const LOAD_SCORECARD = 'LOAD_SCORECARD;';
const LOAD_SCORECARD_SUCCESS = 'LOAD_SCORECARD_SUCCESS;';
const LOAD_SCORECARD_FAILURE = 'LOAD_SCORECARD_FAILURE;';

export const LoadScorecardAction = createAction(LOAD_SCORECARD, props<{ payload: string }>())
export const LoadScorecardSuccessAction = createAction(LOAD_SCORECARD_SUCCESS, props<{ payload: Scorecard }>())
export const LoadScorecardFailureAction = createAction(LOAD_SCORECARD_FAILURE, props<{ payload: ErrorDetails }>())

const LOAD_BYDECADE = 'LOAD_BYDECADE;';
const LOAD_BYDECADE_SUCCESS = 'LOAD_BYDECADE_SUCCESS;';
const LOAD_BYDECADE_FAILURE = 'LOAD_BYDECADE_FAILURE;';

export const LoadByDecadeAction = createAction(LOAD_BYDECADE, props<{ payload: string }>())
export const LoadByDecadeSuccessAction = createAction(LOAD_BYDECADE_SUCCESS, props<{ payload: {[decade: number]: string[]} }>())
export const LoadByDecadeFailureAction = createAction(LOAD_BYDECADE_FAILURE, props<{ payload: ErrorDetails }>())

const LOAD_BYYEAR =         'LOAD_BYYEAR;';
const LOAD_BYYEAR_SUCCESS = 'LOAD_BYYEAR_SUCCESS;';
const LOAD_BYYEAR_FAILURE = 'LOAD_BYYEAR_FAILURE;';

export const LoadByYearAction =        createAction(LOAD_BYYEAR, props<{ payload: { type: string, year: string } }>())
export const LoadByYearSuccessAction = createAction(LOAD_BYYEAR_SUCCESS, props<{ payload: string[] }>())
export const LoadByYearFailureAction = createAction(LOAD_BYYEAR_FAILURE, props<{ payload: ErrorDetails }>())
