import {createAction, props} from '@ngrx/store'
import {Team} from "../models/team.model";
import {MatchDate} from "../models/date.model";
import {RecordsSummaryModel} from "../models/records-summary.model";
import {ErrorDetails} from "../models/error.model";

export const LOAD_RECORDSUMMARIES = 'LOAD_RECORDSUMMARIES;';
export const LOAD_RECORDSUMMARIES_SUCCESS = 'LOAD_RECORDSUMMARIES_SUCCESS;';

export const LoadRecordSummariesAction = createAction(LOAD_RECORDSUMMARIES, props<{
  payload: {
    matchType: string, teamId: number, opponentsId: number, groundId: number, hostCountryId: number
  }
}>())
export const LoadRecordSummariesSuccessAction = createAction(LOAD_RECORDSUMMARIES_SUCCESS, props<{ payload: RecordsSummaryModel }>())


