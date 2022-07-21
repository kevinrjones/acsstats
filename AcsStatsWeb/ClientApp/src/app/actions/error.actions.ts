import {createAction, props} from "@ngrx/store";
import {FormState} from "../models/find-records.model";
import {ErrorDetails} from "../models/error.model";

const SET_ERROR_STATE = 'SET_ERROR_STATE;';
export const RaiseErrorAction = createAction(SET_ERROR_STATE, props<{ payload: ErrorDetails }>())
