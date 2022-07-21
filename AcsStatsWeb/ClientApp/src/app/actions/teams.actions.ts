import {createAction, props} from '@ngrx/store'
import {Team} from "../models/team.model";
import {ErrorDetails} from "../models/error.model";

export const LOAD_TEAMS = 'LOAD_TEAMS;';
export const LOAD_TEAMS_SUCCESS = 'LOAD_TEAMS_SUCCESS;';

export const LoadTeamsAction = createAction(LOAD_TEAMS, props<{ payload: string }>())
export const LoadTeamsSuccessAction = createAction(LOAD_TEAMS_SUCCESS, props<{ payload: Team[] }>())


