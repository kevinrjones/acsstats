import {createReducer, on} from '@ngrx/store';
import {BowlingCareerRecordDto} from "../models/bowling-overall.model";
import {
  LoadByGroundBowlingRecordsSuccessAction,
  LoadByHostBowlingRecordsSuccessAction,
  LoadByMatchBowlingRecordsSuccessAction,
  LoadByOppositionBowlingRecordsSuccessAction,
  LoadBySeasonBowlingRecordsSuccessAction,
  LoadBySeriesBowlingRecordsSuccessAction,
  LoadByYearBowlingRecordsSuccessAction,
  LoadInnByInnBowlingRecordsSuccessAction,
  LoadOverallBowlingRecordsSuccessAction
} from "../actions/records.actions";
import {IndividualBowlingDetailsDto} from "../models/individual-bowling-details.dto";
import {DateTime} from "luxon";
import {SortOrder} from 'src/app/models/sortorder.model';


export const initialBowlingOverallRecordState = {
  data: Array<BowlingCareerRecordDto>(),
  sortOrder: SortOrder.wickets,
  sortDirection: "desc"
};
export const loadOverallBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadOverallBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const initialBowlingInnByInnRecordState = {
  data: Array<IndividualBowlingDetailsDto>(),
  sortOrder: SortOrder.wickets,
  sortDirection: "desc"
};
export const loadInnByInnBowlingReducer = createReducer(
  initialBowlingInnByInnRecordState,
  on(LoadInnByInnBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
)

export const loadByMatchBowlingReducer = createReducer(
  initialBowlingInnByInnRecordState,
  on(LoadByMatchBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
)

export const loadBySeriesBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadBySeriesBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByGroundBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByGroundBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);


export const loadByHostBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByHostBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByOppositionBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByOppositionBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadByYearBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByYearBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);

export const loadBySeasonBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadBySeasonBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  })
);
