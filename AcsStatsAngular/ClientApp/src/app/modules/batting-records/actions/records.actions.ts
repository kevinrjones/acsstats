import {createAction, props} from "@ngrx/store";
import {FindBatting} from "../models/find-batting-overall.model";
import {BattingOverallUiModel, InningsByInningsUiModel} from "../models/batting-overall-ui.model";

const LOAD_OVERALL_BATTINGRECORDS = 'LOAD_OVERALL_BATTINGRECORDS;';
const LOAD_OVERALL_BATTINGRECORDS_SUCCESS = 'LOAD_OVERALL_BATTINGRECORDS_SUCCESS;';
const LOAD_OVERALL_BATTINGRECORDS_FAILURE = 'LOAD_OVERALL_BATTINGRECORDS_FAILURE;';

export const LoadOverallBattingRecordsAction = createAction(LOAD_OVERALL_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadOverallBattingRecordsSuccessAction = createAction(LOAD_OVERALL_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadOverallBattingRecordsFailureAction = createAction(LOAD_OVERALL_BATTINGRECORDS_FAILURE)

const OVERALL_BATTING_RECORDS_SORT = 'OVERALL_BATTING_RECORDS_SORT;';

export const SortBattingRecordsOverallAction = createAction(OVERALL_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_INNBYINN_BATTINGRECORDS =         'LOAD_INNBYINN_BATTINGRECORDS;';
const LOAD_INNBYINN_BATTINGRECORDS_SUCCESS = 'LOAD_INNBYINN_BATTINGRECORDS_SUCCESS;';
const LOAD_INNBYINN_BATTINGRECORDS_FAILURE = 'LOAD_INNBYINN_BATTINGRECORDS_FAILURE;';

export const LoadInnByInnBattingRecordsAction =        createAction(LOAD_INNBYINN_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadInnByInnBattingRecordsSuccessAction = createAction(LOAD_INNBYINN_BATTINGRECORDS_SUCCESS, props<{ payload: InningsByInningsUiModel }>())
export const LoadInnByInnBattingRecordsFailureAction = createAction(LOAD_INNBYINN_BATTINGRECORDS_FAILURE)

const INNBYINN_BATTING_RECORDS_SORT = 'OVERALL_BATTING_RECORDS_SORT;';

export const SortBattingRecordsInningsByInningsAction = createAction(INNBYINN_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYMATCH_BATTINGRECORDS =         'LOAD_BYMATCH_BATTINGRECORDS;';
const LOAD_BYMATCH_BATTINGRECORDS_SUCCESS = 'LOAD_BYMATCH_BATTINGRECORDS_SUCCESS;';
const LOAD_BYMATCH_BATTINGRECORDS_FAILURE = 'LOAD_BYMATCH_BATTINGRECORDS_FAILURE;';

export const LoadByMatchBattingRecordsAction =        createAction(LOAD_BYMATCH_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadByMatchBattingRecordsSuccessAction = createAction(LOAD_BYMATCH_BATTINGRECORDS_SUCCESS, props<{ payload: InningsByInningsUiModel }>())
export const LoadByMatchBattingRecordsFailureAction = createAction(LOAD_BYMATCH_BATTINGRECORDS_FAILURE)

const BYMATCH_BATTING_RECORDS_SORT = 'BYMATCH_BATTING_RECORDS_SORT;';

export const SortBattingRecordsByMatchAction = createAction(BYMATCH_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYSERIES_BATTINGRECORDS =         'LOAD_BYSERIES_BATTINGRECORDS;';
const LOAD_BYSERIES_BATTINGRECORDS_SUCCESS = 'LOAD_BYSERIES_BATTINGRECORDS_SUCCESS;';
const LOAD_BYSERIES_BATTINGRECORDS_FAILURE = 'LOAD_BYSERIES_BATTINGRECORDS_FAILURE;';

export const LoadBySeriesBattingRecordsAction =        createAction(LOAD_BYSERIES_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadBySeriesBattingRecordsSuccessAction = createAction(LOAD_BYSERIES_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadBySeriesBattingRecordsFailureAction = createAction(LOAD_BYSERIES_BATTINGRECORDS_FAILURE)

const BYSERIES_BATTING_RECORDS_SORT = 'BYSERIES_BATTING_RECORDS_SORT;';

export const SortBattingRecordsBySeriesAction = createAction(BYSERIES_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYGROUND_BATTINGRECORDS =         'LOAD_BYGROUND_BATTINGRECORDS;';
const LOAD_BYGROUND_BATTINGRECORDS_SUCCESS = 'LOAD_BYGROUND_BATTINGRECORDS_SUCCESS;';
const LOAD_BYGROUND_BATTINGRECORDS_FAILURE = 'LOAD_BYGROUND_BATTINGRECORDS_FAILURE;';

export const LoadByGroundBattingRecordsAction =        createAction(LOAD_BYGROUND_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadByGroundBattingRecordsSuccessAction = createAction(LOAD_BYGROUND_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadByGroundBattingRecordsFailureAction = createAction(LOAD_BYGROUND_BATTINGRECORDS_FAILURE)

const BYGROUND_BATTING_RECORDS_SORT = 'BYGROUND_BATTING_RECORDS_SORT;';

export const SortBattingRecordsByGroundAction = createAction(BYGROUND_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYHOST_BATTINGRECORDS =         'LOAD_BYHOST_BATTINGRECORDS;';
const LOAD_BYHOST_BATTINGRECORDS_SUCCESS = 'LOAD_BYHOST_BATTINGRECORDS_SUCCESS;';
const LOAD_BYHOST_BATTINGRECORDS_FAILURE = 'LOAD_BYHOST_BATTINGRECORDS_FAILURE;';

export const LoadByHostBattingRecordsAction =        createAction(LOAD_BYHOST_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadByHostBattingRecordsSuccessAction = createAction(LOAD_BYHOST_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadByHostBattingRecordsFailureAction = createAction(LOAD_BYHOST_BATTINGRECORDS_FAILURE)

const BYHOST_BATTING_RECORDS_SORT = 'BYHOST_BATTING_RECORDS_SORT;';

export const SortBattingRecordsByHostAction = createAction(BYHOST_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYOPPOSITION_BATTINGRECORDS =         'LOAD_BYOPPOSITION_BATTINGRECORDS;';
const LOAD_BYOPPOSITION_BATTINGRECORDS_SUCCESS = 'LOAD_BYOPPOSITION_BATTINGRECORDS_SUCCESS;';
const LOAD_BYOPPOSITION_BATTINGRECORDS_FAILURE = 'LOAD_BYOPPOSITION_BATTINGRECORDS_FAILURE;';

export const LoadByOppositionBattingRecordsAction =        createAction(LOAD_BYOPPOSITION_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadByOppositionBattingRecordsSuccessAction = createAction(LOAD_BYOPPOSITION_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadByOppositionBattingRecordsFailureAction = createAction(LOAD_BYOPPOSITION_BATTINGRECORDS_FAILURE)

const BYOPPOSITION_BATTING_RECORDS_SORT = 'BYOPPOSITION_BATTING_RECORDS_SORT;';

export const SortBattingRecordsByOppositionAction = createAction(BYOPPOSITION_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYSEASON_BATTINGRECORDS =         'LOAD_BYSEASON_BATTINGRECORDS;';
const LOAD_BYSEASON_BATTINGRECORDS_SUCCESS = 'LOAD_BYSEASON_BATTINGRECORDS_SUCCESS;';
const LOAD_BYSEASON_BATTINGRECORDS_FAILURE = 'LOAD_BYSEASON_BATTINGRECORDS_FAILURE;';

export const LoadBySeasonBattingRecordsAction =        createAction(LOAD_BYSEASON_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadBySeasonBattingRecordsSuccessAction = createAction(LOAD_BYSEASON_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadBySeasonBattingRecordsFailureAction = createAction(LOAD_BYSEASON_BATTINGRECORDS_FAILURE)

const BYSEASON_BATTING_RECORDS_SORT = 'BYSEASON_BATTING_RECORDS_SORT;';

export const SortBattingRecordsBySeasonAction = createAction(BYSEASON_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())

const LOAD_BYYEAR_BATTINGRECORDS =         'LOAD_BYYEAR_BATTINGRECORDS;';
const LOAD_BYYEAR_BATTINGRECORDS_SUCCESS = 'LOAD_BYYEAR_BATTINGRECORDS_SUCCESS;';
const LOAD_BYYEAR_BATTINGRECORDS_FAILURE = 'LOAD_BYYEAR_BATTINGRECORDS_FAILURE;';

export const LoadByYearBattingRecordsAction =        createAction(LOAD_BYYEAR_BATTINGRECORDS, props<{ payload: FindBatting }>())
export const LoadByYearBattingRecordsSuccessAction = createAction(LOAD_BYYEAR_BATTINGRECORDS_SUCCESS, props<{ payload: BattingOverallUiModel }>())
export const LoadByYearBattingRecordsFailureAction = createAction(LOAD_BYYEAR_BATTINGRECORDS_FAILURE)

const BYYEAR_BATTING_RECORDS_SORT = 'BYYEAR_BATTING_RECORDS_SORT;';

export const SortBattingRecordsByYearAction = createAction(BYYEAR_BATTING_RECORDS_SORT, props<{ payload: { sortOrder: number, sortDirection: string } }>())
