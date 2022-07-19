import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY, of} from 'rxjs';
import {
  LoadByGroundBowlingRecordsAction, LoadByGroundBowlingRecordsFailureAction,
  LoadByGroundBowlingRecordsSuccessAction,
  LoadByHostBowlingRecordsAction, LoadByHostBowlingRecordsFailureAction,
  LoadByHostBowlingRecordsSuccessAction,
  LoadByMatchBowlingRecordsAction, LoadByMatchBowlingRecordsFailureAction,
  LoadByMatchBowlingRecordsSuccessAction,
  LoadByOppositionBowlingRecordsAction, LoadByOppositionBowlingRecordsFailureAction,
  LoadByOppositionBowlingRecordsSuccessAction,
  LoadBySeasonBowlingRecordsAction, LoadBySeasonBowlingRecordsFailureAction,
  LoadBySeasonBowlingRecordsSuccessAction,
  LoadBySeriesBowlingRecordsAction, LoadBySeriesBowlingRecordsFailureAction,
  LoadBySeriesBowlingRecordsSuccessAction,
  LoadByYearBowlingRecordsAction, LoadByYearBowlingRecordsFailureAction,
  LoadByYearBowlingRecordsSuccessAction,
  LoadInnByInnBowlingRecordsAction, LoadInnByInnBowlingRecordsFailureAction,
  LoadInnByInnBowlingRecordsSuccessAction,
  LoadOverallBowlingRecordsAction, LoadOverallBowlingRecordsFailureAction,
  LoadOverallBowlingRecordsSuccessAction
} from "../actions/records.actions";
import {BowlingRecordService} from "../services/bowling-record.service";
import {
  LoadByYearBattingRecordsFailureAction,
  LoadInnByInnBattingRecordsFailureAction
} from "../../batting-records/actions/records.actions";
import {createError} from "../../../helpers/ErrorHelper";

@Injectable()
export class RecordEffects {
  constructor(
    private bowlingRecordsSearchService: BowlingRecordService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadBowlingRecordsOverall$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadOverallBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingOverall(action.payload)
        .pipe(
          map(players => LoadOverallBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadOverallBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });


  loadBowlingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingInningsByInnings(action.payload)
        .pipe(
          map(players => LoadInnByInnBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadInnByInnBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByMatch(action.payload)
        .pipe(
          map(players => LoadByMatchBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadByMatchBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeries(action.payload)
        .pipe(
          map(players => LoadBySeriesBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadBySeriesBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByGround(action.payload)
        .pipe(
          map(players => LoadByGroundBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadByGroundBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByHostCountry(action.payload)
        .pipe(
          map(players => LoadByHostBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadByHostBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByOpposition(action.payload)
        .pipe(
          map(players => LoadByOppositionBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadByOppositionBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByYear(action.payload)
        .pipe(
          map(players => LoadByYearBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => of(LoadByYearBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeason(action.payload)
        .pipe(
          map(players => LoadBySeasonBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(LoadBySeasonBowlingRecordsFailureAction({payload: createError(1)})))
        ))
    );
  });

}
