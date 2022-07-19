import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY, of} from 'rxjs';
import {
  LoadByDecadeAction,
  LoadByDecadeFailureAction,
  LoadByDecadeSuccessAction,
  LoadByYearAction,
  LoadByYearFailureAction,
  LoadByYearSuccessAction,
  LoadScorecardAction,
  LoadScorecardFailureAction,
  LoadScorecardListAction,
  LoadScorecardListFailureAction,
  LoadScorecardListSuccessAction,
  LoadScorecardSuccessAction,
  LoadScorecardTournamentListAction
} from '../actions/scorecard.actions';
import {ScorecardSearchService} from '../services/scorecard-search.service';
import {LoadOverallBowlingRecordsFailureAction} from "../../bowling-records/actions/records.actions";
import {createError} from "../../../helpers/ErrorHelper";

@Injectable()
export class ScorecardEffects {
  constructor(
    private scorecardSearchService: ScorecardSearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map chack that the envelope is valid and if not return an error
  loadPlayers$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadScorecardListAction),
      mergeMap(action => this.scorecardSearchService.findMatches(action.payload)
        .pipe(
          map(players => LoadScorecardListSuccessAction({payload: players.result})),
          catchError(() => of(LoadScorecardListFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadTournaments$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadScorecardTournamentListAction),
      mergeMap(action => this.scorecardSearchService.findTournament(action.payload)
        .pipe(
          map(players => LoadScorecardListSuccessAction({payload: players.result})),
          catchError(() => of(LoadScorecardFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadScorecard$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadScorecardAction),
      mergeMap(action => this.scorecardSearchService.findCard(action.payload)
        .pipe(
          map(players => LoadScorecardSuccessAction({payload: players.result})),
          catchError(() => of(LoadScorecardFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadByDecade$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByDecadeAction),
      mergeMap(action => this.scorecardSearchService.findByDecade(action.payload)
        .pipe(
          map(decades => LoadByDecadeSuccessAction({payload: decades.result})),
          catchError(() => of(LoadByDecadeFailureAction({payload: createError(1)})))
        ))
    );
  });

  loadByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearAction),
      mergeMap(action => this.scorecardSearchService.findByYear(action.payload.year, action.payload.type)
        .pipe(
          map(decades => LoadByYearSuccessAction({payload: decades.result})),
          catchError(() => of(LoadByYearFailureAction({payload: createError(1)})))
        ))
    );
  });

}
