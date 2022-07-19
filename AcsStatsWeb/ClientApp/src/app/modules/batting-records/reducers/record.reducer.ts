import {createReducer, on} from '@ngrx/store';
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {
  LoadByGroundBattingRecordsAction,
  LoadByGroundBattingRecordsFailureAction,
  LoadByGroundBattingRecordsSuccessAction,
  LoadByHostBattingRecordsAction,
  LoadByHostBattingRecordsFailureAction,
  LoadByHostBattingRecordsSuccessAction,
  LoadByMatchBattingRecordsAction,
  LoadByMatchBattingRecordsFailureAction,
  LoadByMatchBattingRecordsSuccessAction,
  LoadByOppositionBattingRecordsAction,
  LoadByOppositionBattingRecordsFailureAction,
  LoadByOppositionBattingRecordsSuccessAction,
  LoadBySeasonBattingRecordsAction, LoadBySeasonBattingRecordsFailureAction,
  LoadBySeasonBattingRecordsSuccessAction,
  LoadBySeriesBattingRecordsFailureAction,
  LoadBySeriesBattingRecordsSuccessAction,
  LoadByYearBattingRecordsAction, LoadByYearBattingRecordsFailureAction,
  LoadByYearBattingRecordsSuccessAction,
  LoadInnByInnBattingRecordsAction,
  LoadInnByInnBattingRecordsSuccessAction,
  LoadOverallBattingRecordsAction,
  LoadOverallBattingRecordsSuccessAction
} from "../actions/records.actions";
import {IndividualBattingDetailsDto} from "../models/individual-batting-details.dto";
import {DateTime} from "luxon";
import {SortOrder} from 'src/app/models/sortorder.model';
import {
  LoadBySeriesBowlingRecordsAction,
  LoadInnByInnBowlingRecordsFailureAction
} from "../../bowling-records/actions/records.actions";
import {SetErrorState} from "../../../actions/error.actions";


export const initialBattingOverallRecordState = {
  sqlResults: {data: Array<BattingCareerRecordDto>(), count: 0},
  sortOrder: 4,
  sortDirection: "desc",
  error: {}
};
export const loadOverallBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadOverallBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadOverallBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
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
  }),
  on(LoadInnByInnBowlingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadInnByInnBattingRecordsAction, (state, records) => {
    return initialBattingInnByInnRecordState
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
  }),
  on(LoadByMatchBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadByMatchBattingRecordsAction, (state, records) => {
    return initialBattingInnByInnRecordState
  })
)

export const loadBySeriesBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeriesBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadBySeriesBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadBySeriesBowlingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);

export const loadByGroundBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByGroundBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadByGroundBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadByGroundBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);


export const loadByHostBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByHostBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadByHostBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadByHostBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);

export const loadByOppositionBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByOppositionBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadByOppositionBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadByOppositionBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);

export const loadByYearBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByYearBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadByYearBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadByYearBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);

export const loadBySeasonBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeasonBattingRecordsSuccessAction, (state, records) => {
    return {
      sqlResults: records.payload.sqlResults,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection,
      error: {}
    }
  }),
  on(LoadBySeasonBattingRecordsFailureAction, (state, error) => {
    return {
      ...state,
      error: error
    }
  }),
  on(LoadBySeasonBattingRecordsAction, (state, records) => {
    return initialBattingOverallRecordState
  })
);
