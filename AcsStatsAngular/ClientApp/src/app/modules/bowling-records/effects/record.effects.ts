import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY} from 'rxjs';
import {
  LoadByGroundBowlingRecordsAction,
  LoadByGroundBowlingRecordsSuccessAction,
  LoadByHostBowlingRecordsAction,
  LoadByHostBowlingRecordsSuccessAction,
  LoadByMatchBowlingRecordsAction,
  LoadByMatchBowlingRecordsSuccessAction,
  LoadByOppositionBowlingRecordsAction,
  LoadByOppositionBowlingRecordsSuccessAction,
  LoadBySeasonBowlingRecordsAction,
  LoadBySeasonBowlingRecordsSuccessAction,
  LoadBySeriesBowlingRecordsAction,
  LoadBySeriesBowlingRecordsSuccessAction,
  LoadByYearBowlingRecordsAction,
  LoadByYearBowlingRecordsSuccessAction,
  LoadInnByInnBowlingRecordsAction,
  LoadInnByInnBowlingRecordsSuccessAction,
  LoadOverallBowlingRecordsAction,
  LoadOverallBowlingRecordsSuccessAction
} from "../actions/records.actions";
import {BowlingRecordService} from "../services/bowling-record.service";

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
          catchError(() => EMPTY)
        ))
    );
  });


  loadBowlingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingInningsByInnings(action.payload)
        .pipe(
          map(players => LoadInnByInnBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByMatch(action.payload)
        .pipe(
          map(players => LoadByMatchBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeries(action.payload)
        .pipe(
          map(players => LoadBySeriesBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByGround(action.payload)
        .pipe(
          map(players => LoadByGroundBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByHostCountry(action.payload)
        .pipe(
          map(players => LoadByHostBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByOpposition(action.payload)
        .pipe(
          map(players => LoadByOppositionBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByYear(action.payload)
        .pipe(
          map(players => LoadByYearBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadBowlingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeason(action.payload)
        .pipe(
          map(players => LoadBySeasonBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError(() => EMPTY)
        ))
    );
  });

}
