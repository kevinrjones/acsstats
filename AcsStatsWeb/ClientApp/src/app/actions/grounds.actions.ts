import {Country} from '../models/country.model';
import {createAction, props} from '@ngrx/store'
import {Ground} from "../models/ground.model";
import {ErrorDetails} from "../models/error.model";


export const LOAD_GROUNDS = 'LOAD_GROUNDS;';
export const LOAD_GROUNDS_SUCCESS = 'LOAD_GROUNDS_SUCCESS;';


export const LoadGroundsAction = createAction(LOAD_GROUNDS, props<{ payload: string }>())
export const LoadGroundsSuccessAction = createAction(LOAD_GROUNDS_SUCCESS, props<{ payload: Ground[] }>())

