import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY, of} from 'rxjs';
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
import {createError} from "../../../helpers/ErrorHelper";
import {RaiseErrorAction} from "../../../actions/error.actions";
import { Envelope } from 'src/app/models/envelope';
import {BowlingCareerRecordDto} from "../models/bowling-overall.model";

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
          map((players: Envelope<BowlingCareerRecordDto[]>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the match records")})

            return LoadOverallBowlingRecordsSuccessAction({
              payload: {
                data: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });


  loadBowlingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingInningsByInnings(action.payload)
        .pipe(
          map(players => LoadInnByInnBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByMatch(action.payload)
        .pipe(
          map(players => LoadByMatchBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeries(action.payload)
        .pipe(
          map(players => LoadBySeriesBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByGround(action.payload)
        .pipe(
          map(players => LoadByGroundBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByHostCountry(action.payload)
        .pipe(
          map(players => LoadByHostBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByOpposition(action.payload)
        .pipe(
          map(players => LoadByOppositionBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingByYear(action.payload)
        .pipe(
          map(players => LoadByYearBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBowlingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBowlingRecordsAction),
      mergeMap(action => this.bowlingRecordsSearchService.getBowlingBySeason(action.payload)
        .pipe(
          map(players => LoadBySeasonBowlingRecordsSuccessAction({payload: {data: players.result, sortOrder: action.payload.sortOrder, sortDirection: action.payload.sortDirection}})),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

}
