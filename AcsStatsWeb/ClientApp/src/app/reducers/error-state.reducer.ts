import {createReducer, on} from "@ngrx/store";
import {SaveRecordsFormAction} from "../actions/form-state.actions";
import {SetErrorState} from "../actions/error.actions";
import {ErrorDetails} from "../models/error.model";


export const initialErrorState: ErrorDetails = {id: 0, message: null};
export const setErrorState = createReducer(
  initialErrorState,
  on(SetErrorState, (state, records) => {
    return records.payload;
  })
);
