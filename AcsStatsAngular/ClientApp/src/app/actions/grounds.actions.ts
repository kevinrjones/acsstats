import {Country} from '../models/country.model';
import {createAction, props} from '@ngrx/store'
import {Ground} from "../models/ground.model";


export const LOAD_GROUNDS = 'LOAD_GROUNDS;';
export const LOAD_GROUNDS_SUCCESS = 'LOAD_GROUNDS_SUCCESS;';
export const LOAD_GROUNDS_FAILURE = 'LOAD_GROUNDS_FAILURE;';


export const LoadGroundsAction = createAction(LOAD_GROUNDS, props<{ payload: string }>())
export const LoadGroundsSuccessAction = createAction(LOAD_GROUNDS_SUCCESS, props<{ payload: Ground[] }>())
export const LoadGroundsFailureAction = createAction(LOAD_GROUNDS_FAILURE)

