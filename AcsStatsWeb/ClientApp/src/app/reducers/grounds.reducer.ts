import { Country } from '../models/country.model'
import * as countryActions from './../actions/countries.actions'
import {createReducer, on} from '@ngrx/store';
import { Ground } from '../models/ground.model';
import {LoadGroundsSuccessAction} from "../actions/grounds.actions";


export const initialState: ReadonlyArray<Ground> = [];
export const groundsReducer = createReducer(
  initialState,
  on(LoadGroundsSuccessAction, (state, grounds) => {
    return grounds.payload
  })
);

