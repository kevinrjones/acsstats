import {createAction, props} from "@ngrx/store";
import {FormState} from "../models/find-records.model";

const SAVE_BATTINGRECORDS_FORM = 'SAVE_BATTINGRECORDS_FORM;';
export const SaveRecordsFormAction = createAction(SAVE_BATTINGRECORDS_FORM, props<{ payload: FormState }>())
