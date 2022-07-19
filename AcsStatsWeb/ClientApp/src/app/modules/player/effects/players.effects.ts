import {Injectable} from '@angular/core';
import {PlayerSearchService} from '../services/playersearch.service';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY, of} from 'rxjs';
import {
  LoadPlayerBattingDetailsAction, LoadPlayerBattingDetailsFailureAction,
  LoadPlayerBattingDetailsSuccessAction,
  LoadPlayerBiographyAction, LoadPlayerBiographyFailureAction,
  LoadPlayerBiographySuccessAction,
  LoadPlayerBowlingDetailsAction, LoadPlayerBowlingDetailsFailureAction,
  LoadPlayerBowlingDetailsSuccessAction,
  LoadPlayerOverallAction, LoadPlayerOverallFailureAction,
  LoadPlayerOverallSuccessAction,
  LoadPlayersAction, LoadPlayersFailureAction,
  LoadPlayersSuccessAction
} from '../actions/players.actions';
import {LoadOverallBowlingRecordsFailureAction} from "../../bowling-records/actions/records.actions";
import {createError} from "../../../helpers/ErrorHelper";

@Injectable()
export class PlayerEffects {
  constructor(
    private playerSearchService: PlayerSearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadPlayers$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadPlayersAction),
      mergeMap(action => this.playerSearchService.findPlayers(action.payload)
        .pipe(
          map(players => LoadPlayersSuccessAction({payload: players.result})),
          catchError(() => of(LoadPlayersFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadPlayerBiography$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadPlayerBiographyAction),
      mergeMap(action => this.playerSearchService.getPlayerBiography(action.payload)
        .pipe(
          map(players => LoadPlayerBiographySuccessAction({payload: players.result})),
          catchError(() => of(LoadPlayerBiographyFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadPlayerOverall$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadPlayerOverallAction),
      mergeMap(action => this.playerSearchService.getPlayerOverall(action.payload)
        .pipe(
          map(players => LoadPlayerOverallSuccessAction({payload: players.result})),
          catchError(() => of(LoadPlayerOverallFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadPlayerBattingOverall$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadPlayerBattingDetailsAction),
      mergeMap(action => this.playerSearchService.getPlayerBattingOverall(action.payload)
        .pipe(
          map(players => LoadPlayerBattingDetailsSuccessAction({payload: players.result})),
          catchError(() => of(LoadPlayerBattingDetailsFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadPlayerBowlingOverall$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadPlayerBowlingDetailsAction),
      mergeMap(action => this.playerSearchService.getPlayerBowlingOverall(action.payload)
        .pipe(
          map(players => LoadPlayerBowlingDetailsSuccessAction({payload: players.result})),
          catchError(() => of(LoadPlayerBowlingDetailsFailureAction({payload: createError(1)})))
        ))
    );
  });

}
