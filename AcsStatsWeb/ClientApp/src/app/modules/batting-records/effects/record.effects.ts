import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY, of} from 'rxjs';
import {
  LoadByGroundBattingRecordsAction, LoadByGroundBattingRecordsFailureAction,
  LoadByGroundBattingRecordsSuccessAction,
  LoadByHostBattingRecordsAction, LoadByHostBattingRecordsFailureAction,
  LoadByHostBattingRecordsSuccessAction,
  LoadByMatchBattingRecordsAction, LoadByMatchBattingRecordsFailureAction,
  LoadByMatchBattingRecordsSuccessAction,
  LoadByOppositionBattingRecordsAction, LoadByOppositionBattingRecordsFailureAction,
  LoadByOppositionBattingRecordsSuccessAction,
  LoadBySeasonBattingRecordsAction, LoadBySeasonBattingRecordsFailureAction,
  LoadBySeasonBattingRecordsSuccessAction,
  LoadBySeriesBattingRecordsAction, LoadBySeriesBattingRecordsFailureAction,
  LoadBySeriesBattingRecordsSuccessAction,
  LoadByYearBattingRecordsAction, LoadByYearBattingRecordsFailureAction,
  LoadByYearBattingRecordsSuccessAction,
  LoadInnByInnBattingRecordsAction, LoadInnByInnBattingRecordsFailureAction,
  LoadInnByInnBattingRecordsSuccessAction,
  LoadOverallBattingRecordsAction,
  LoadOverallBattingRecordsSuccessAction
} from "../actions/records.actions";
import {BattingRecordService} from "../services/batting-record.service";
import {LoadCountriesFailureAction} from "../../../actions/countries.actions";
import {createError} from "../../../helpers/ErrorHelper";
import {SetErrorState} from "../../../actions/error.actions";

@Injectable()
export class RecordEffects {
  constructor(
    private battingRecordsSearchService: BattingRecordService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadBattingRecordsOverall$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadOverallBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingOverall(action.payload)
        .pipe(
          map(players => LoadOverallBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError((err) => {
            return of(SetErrorState({payload: createError(1)}))
          })
        ))
    );
  });


  loadBattingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingInningsByInnings(action.payload)
        .pipe(
          map(players => LoadInnByInnBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadInnByInnBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByMatch(action.payload)
        .pipe(
          map(players => LoadByMatchBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadByMatchBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeries(action.payload)
        .pipe(
          map(players => LoadBySeriesBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadBySeriesBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByGround(action.payload)
        .pipe(
          map(players => LoadByGroundBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadByGroundBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByHostCountry(action.payload)
        .pipe(
          map(players => LoadByHostBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadByHostBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByOpposition(action.payload)
        .pipe(
          map(players => LoadByOppositionBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadByOppositionBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByYear(action.payload)
        .pipe(
          map(players => LoadByYearBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadByYearBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeason(action.payload)
        .pipe(
          map(players => LoadBySeasonBattingRecordsSuccessAction({
            payload: {
              sqlResults: players.result,
              sortOrder: action.payload.sortOrder,
              sortDirection: action.payload.sortDirection
            }
          })),
          catchError(() => of(LoadBySeasonBattingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

}
