import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {of} from 'rxjs';
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
import {createError} from "../../../helpers/ErrorHelper";
import {RaiseErrorAction} from "../../../actions/error.actions";
import {Envelope} from 'src/app/models/envelope';
import {SqlResultsEnvelope} from "../../../models/sqlresultsenvelope.model";
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {IndividualBattingDetailsDto} from "../models/individual-batting-details.dto";

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
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the overall records")})

            return LoadOverallBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });


  loadBattingRecordsInningsByInnings$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadInnByInnBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingInningsByInnings(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<IndividualBattingDetailsDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the inningsrecords")})

            return LoadInnByInnBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByMatch$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByMatchBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByMatch(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<IndividualBattingDetailsDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the match records")})

            return LoadByMatchBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsBySeries$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeriesBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeries(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the seriesrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByGround$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByGroundBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByGround(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the groundrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByHost$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByHostBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByHostCountry(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the host countryrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByOpposition$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByOppositionBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByOpposition(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the oppositionrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingByYear(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the yearlyrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

  loadBattingRecordsBySeason$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadBySeasonBattingRecordsAction),
      mergeMap(action => this.battingRecordsSearchService.getBattingBySeason(action.payload)
        .pipe(
          map((players: Envelope<SqlResultsEnvelope<BattingCareerRecordDto[]>>) => {
            if (players.errorMessage != null && players.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get the seasonrecords")})
            return LoadBySeriesBattingRecordsSuccessAction({
              payload: {
                sqlResults: players.result,
                sortOrder: action.payload.sortOrder,
                sortDirection: action.payload.sortDirection
              }
            })
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

}
