import {createReducer, on} from '@ngrx/store';
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {
  LoadByGroundBattingRecordsSuccessAction,
  LoadByHostBattingRecordsSuccessAction,
  LoadByMatchBattingRecordsSuccessAction,
  LoadByOppositionBattingRecordsSuccessAction,
  LoadBySeasonBattingRecordsSuccessAction,
  LoadBySeriesBattingRecordsSuccessAction,
  LoadByYearBattingRecordsSuccessAction,
  LoadInnByInnBattingRecordsSuccessAction,
  LoadOverallBattingRecordsSuccessAction
} from "../actions/records.actions";
import {IndividualBattingDetailsDto} from "../models/individual-batting-details.dto";
import {DateTime} from "luxon";
import {SortOrder} from 'src/app/models/sortorder.model';


export const initialBattingOverallRecordState = {
  sqlResults: {data: Array<BattingCareerRecordDto>(), count:0 },
  sortOrder: 4,
  sortDirection: "desc"
};
export const loadOverallBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadOverallBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const initialBattingInnByInnRecordState = {
  sqlResults: {data: Array<IndividualBattingDetailsDto>(), count: 0},
  sortOrder: 4,
  sortDirection: "desc"
};
export const loadInnByInnBattingReducer = createReducer(
  initialBattingInnByInnRecordState,
  on(LoadInnByInnBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
)

export const loadByMatchBattingReducer = createReducer(
  initialBattingInnByInnRecordState,
  on(LoadByMatchBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
)

export const loadBySeriesBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeriesBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByGroundBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByGroundBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);


export const loadByHostBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByHostBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByOppositionBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByOppositionBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByYearBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByYearBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadBySeasonBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeasonBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);
