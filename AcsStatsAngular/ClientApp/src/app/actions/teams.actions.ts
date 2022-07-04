import {createAction, props} from '@ngrx/store'
import {Team} from "../models/team.model";

export const LOAD_TEAMS = 'LOAD_TEAMS;';
export const LOAD_TEAMS_SUCCESS = 'LOAD_TEAMS_SUCCESS;';
export const LOAD_TEAMS_FAILURE = 'LOAD_TEAMS_FAILURE;';

export const LoadTeamsAction = createAction(LOAD_TEAMS, props<{ payload: string }>())
export const LoadTeamsSuccessAction = createAction(LOAD_TEAMS_SUCCESS, props<{ payload: Team[] }>())
export const LoadTeamsFailureAction = createAction(LOAD_TEAMS_FAILURE)


