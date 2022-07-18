import {createAction, props} from '@ngrx/store'
import {Team} from "../models/team.model";
import {MatchDate} from "../models/date.model";
import {MatchSubTypeModel} from "../models/match-sub-type.model";

export const LOAD_MATCHSUBTYPES = 'LOAD_MATCHSUBTYPES;';
export const LOAD_MATCHSUBTYPES_SUCCESS = 'LOAD_MATCHSUBTYPES_SUCCESS;';
export const LOAD_MATCHSUBTYPES_FAILURE = 'LOAD_MATCHSUBTYPES_FAILURE;';

export const LoadMatchSubTypesAction = createAction(LOAD_MATCHSUBTYPES, props<{ payload: string }>())
export const LoadMatchSubTypesSuccessAction = createAction(LOAD_MATCHSUBTYPES_SUCCESS, props<{ payload: MatchSubTypeModel[] }>())
export const LoadMatchSubTypesFailureAction = createAction(LOAD_MATCHSUBTYPES_FAILURE)


