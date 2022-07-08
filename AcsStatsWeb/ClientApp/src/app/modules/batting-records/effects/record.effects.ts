import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY} from 'rxjs';
import {
  LoadByGroundBattingRecordsAction,
  LoadByGroundBattingRecordsSuccessAction,
  LoadByHostBattingRecordsAction,
  LoadByHostBattingRecordsSuccessAction,
  LoadByMatchBattingRecordsAction,
  LoadByMatchBattingRecordsSuccessAction,
  LoadByOppositionBattingRecordsAction,
  LoadByOppositionBattingRecordsSuccessAction,
  LoadBySeasonBattingRecordsAction,
  LoadBySeasonBattingRecordsSuccessAction,
  LoadBySeriesBattingRecordsAction,
  LoadBySeriesBattingRecordsSuccessAction,
  LoadByYearBattingRecordsAction,
  LoadByYearBattingRecordsSuccessAction,
  LoadInnByInnBattingRecordsAction,
  LoadInnByInnBattingRecordsSuccessAction,
  LoadOverallBattingRecordsAction,
  LoadOverallBattingRecordsSuccessAction
} from "../actions/records.actions";
import {BattingRecordService} from "../services/batting-record.service";

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
          map(players => LoadOverallBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });


  loadBattingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingInningsByInnings(action.payload)
        .pipe(
          map(players => LoadInnByInnBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByMatch(action.payload)
        .pipe(
          map(players => LoadByMatchBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeries(action.payload)
        .pipe(
          map(players => LoadBySeriesBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByGround(action.payload)
        .pipe(
          map(players => LoadByGroundBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByHostCountry(action.payload)
        .pipe(
          map(players => LoadByHostBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByOpposition(action.payload)
        .pipe(
          map(players => LoadByOppositionBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByYear(action.payload)
        .pipe(
          map(players => LoadByYearBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBattingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeason(action.payload)
        .pipe(
          map(players => LoadBySeasonBattingRecordsSuccessAction({payload: {sqlResults: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

}
