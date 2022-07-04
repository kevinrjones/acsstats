import {createAction, props} from "@ngrx/store";
import {FindBowling} from "../models/find-bowling-overall.model";
import {BowlingOverallUiModel, InningsByInningsUiModel} from "../models/bowling-overall-ui.model";

const LOAD_OVERALL_BOWLINGRECORDS = 'LOAD_OVERALL_BOWLINGRECORDS;';
const LOAD_OVERALL_BOWLINGRECORDS_SUCCESS = 'LOAD_OVERALL_BOWLINGRECORDS_SUCCESS;';
const LOAD_OVERALL_BOWLINGRECORDS_FAILURE = 'LOAD_OVERALL_BOWLINGRECORDS_FAILURE;';

export const LoadOverallBowlingRecordsAction = createAction(LOAD_OVERALL_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadOverallBowlingRecordsSuccessAction = createAction(LOAD_OVERALL_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadOverallBowlingRecordsFailureAction = createAction(LOAD_OVERALL_BOWLINGRECORDS_FAILURE)

const OVERALL_BOWLING_RECORDS_SORT = 'OVERALL_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsOverallAction = createAction(OVERALL_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_INNBYINN_BOWLINGRECORDS =         'LOAD_INNBYINN_BOWLINGRECORDS;';
const LOAD_INNBYINN_BOWLINGRECORDS_SUCCESS = 'LOAD_INNBYINN_BOWLINGRECORDS_SUCCESS;';
const LOAD_INNBYINN_BOWLINGRECORDS_FAILURE = 'LOAD_INNBYINN_BOWLINGRECORDS_FAILURE;';

export const LoadInnByInnBowlingRecordsAction =        createAction(LOAD_INNBYINN_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadInnByInnBowlingRecordsSuccessAction = createAction(LOAD_INNBYINN_BOWLINGRECORDS_SUCCESS, props<{ payload: InningsByInningsUiModel }>())
export const LoadInnByInnBowlingRecordsFailureAction = createAction(LOAD_INNBYINN_BOWLINGRECORDS_FAILURE)

const INNBYINN_BOWLING_RECORDS_SORT = 'OVERALL_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsInningsByInningsAction = createAction(INNBYINN_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYMATCH_BOWLINGRECORDS =         'LOAD_BYMATCH_BOWLINGRECORDS;';
const LOAD_BYMATCH_BOWLINGRECORDS_SUCCESS = 'LOAD_BYMATCH_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYMATCH_BOWLINGRECORDS_FAILURE = 'LOAD_BYMATCH_BOWLINGRECORDS_FAILURE;';

export const LoadByMatchBowlingRecordsAction =        createAction(LOAD_BYMATCH_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadByMatchBowlingRecordsSuccessAction = createAction(LOAD_BYMATCH_BOWLINGRECORDS_SUCCESS, props<{ payload: InningsByInningsUiModel }>())
export const LoadByMatchBowlingRecordsFailureAction = createAction(LOAD_BYMATCH_BOWLINGRECORDS_FAILURE)

const BYMATCH_BOWLING_RECORDS_SORT = 'BYMATCH_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsByMatchAction = createAction(BYMATCH_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYSERIES_BOWLINGRECORDS =         'LOAD_BYSERIES_BOWLINGRECORDS;';
const LOAD_BYSERIES_BOWLINGRECORDS_SUCCESS = 'LOAD_BYSERIES_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYSERIES_BOWLINGRECORDS_FAILURE = 'LOAD_BYSERIES_BOWLINGRECORDS_FAILURE;';

export const LoadBySeriesBowlingRecordsAction =        createAction(LOAD_BYSERIES_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadBySeriesBowlingRecordsSuccessAction = createAction(LOAD_BYSERIES_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadBySeriesBowlingRecordsFailureAction = createAction(LOAD_BYSERIES_BOWLINGRECORDS_FAILURE)

const BYSERIES_BOWLING_RECORDS_SORT = 'BYSERIES_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsBySeriesAction = createAction(BYSERIES_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYGROUND_BOWLINGRECORDS =         'LOAD_BYGROUND_BOWLINGRECORDS;';
const LOAD_BYGROUND_BOWLINGRECORDS_SUCCESS = 'LOAD_BYGROUND_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYGROUND_BOWLINGRECORDS_FAILURE = 'LOAD_BYGROUND_BOWLINGRECORDS_FAILURE;';

export const LoadByGroundBowlingRecordsAction =        createAction(LOAD_BYGROUND_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadByGroundBowlingRecordsSuccessAction = createAction(LOAD_BYGROUND_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadByGroundBowlingRecordsFailureAction = createAction(LOAD_BYGROUND_BOWLINGRECORDS_FAILURE)

const BYGROUND_BOWLING_RECORDS_SORT = 'BYGROUND_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsByGroundAction = createAction(BYGROUND_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYHOST_BOWLINGRECORDS =         'LOAD_BYHOST_BOWLINGRECORDS;';
const LOAD_BYHOST_BOWLINGRECORDS_SUCCESS = 'LOAD_BYHOST_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYHOST_BOWLINGRECORDS_FAILURE = 'LOAD_BYHOST_BOWLINGRECORDS_FAILURE;';

export const LoadByHostBowlingRecordsAction =        createAction(LOAD_BYHOST_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadByHostBowlingRecordsSuccessAction = createAction(LOAD_BYHOST_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadByHostBowlingRecordsFailureAction = createAction(LOAD_BYHOST_BOWLINGRECORDS_FAILURE)

const BYHOST_BOWLING_RECORDS_SORT = 'BYHOST_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsByHostAction = createAction(BYHOST_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYOPPOSITION_BOWLINGRECORDS =         'LOAD_BYOPPOSITION_BOWLINGRECORDS;';
const LOAD_BYOPPOSITION_BOWLINGRECORDS_SUCCESS = 'LOAD_BYOPPOSITION_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYOPPOSITION_BOWLINGRECORDS_FAILURE = 'LOAD_BYOPPOSITION_BOWLINGRECORDS_FAILURE;';

export const LoadByOppositionBowlingRecordsAction =        createAction(LOAD_BYOPPOSITION_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadByOppositionBowlingRecordsSuccessAction = createAction(LOAD_BYOPPOSITION_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadByOppositionBowlingRecordsFailureAction = createAction(LOAD_BYOPPOSITION_BOWLINGRECORDS_FAILURE)

const BYOPPOSITION_BOWLING_RECORDS_SORT = 'BYOPPOSITION_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsByOppositionAction = createAction(BYOPPOSITION_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYSEASON_BOWLINGRECORDS =         'LOAD_BYSEASON_BOWLINGRECORDS;';
const LOAD_BYSEASON_BOWLINGRECORDS_SUCCESS = 'LOAD_BYSEASON_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYSEASON_BOWLINGRECORDS_FAILURE = 'LOAD_BYSEASON_BOWLINGRECORDS_FAILURE;';

export const LoadBySeasonBowlingRecordsAction =        createAction(LOAD_BYSEASON_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadBySeasonBowlingRecordsSuccessAction = createAction(LOAD_BYSEASON_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadBySeasonBowlingRecordsFailureAction = createAction(LOAD_BYSEASON_BOWLINGRECORDS_FAILURE)

const BYSEASON_BOWLING_RECORDS_SORT = 'BYSEASON_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsBySeasonAction = createAction(BYSEASON_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYYEAR_BOWLINGRECORDS =         'LOAD_BYYEAR_BOWLINGRECORDS;';
const LOAD_BYYEAR_BOWLINGRECORDS_SUCCESS = 'LOAD_BYYEAR_BOWLINGRECORDS_SUCCESS;';
const LOAD_BYYEAR_BOWLINGRECORDS_FAILURE = 'LOAD_BYYEAR_BOWLINGRECORDS_FAILURE;';

export const LoadByYearBowlingRecordsAction =        createAction(LOAD_BYYEAR_BOWLINGRECORDS, props<{ payload: FindBowling }>())
export const LoadByYearBowlingRecordsSuccessAction = createAction(LOAD_BYYEAR_BOWLINGRECORDS_SUCCESS, props<{ payload: BowlingOverallUiModel }>())
export const LoadByYearBowlingRecordsFailureAction = createAction(LOAD_BYYEAR_BOWLINGRECORDS_FAILURE)

const BYYEAR_BOWLING_RECORDS_SORT = 'BYYEAR_BOWLING_RECORDS_SORT;';

export const SortBowlingRecordsByYearAction = createAction(BYYEAR_BOWLING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

