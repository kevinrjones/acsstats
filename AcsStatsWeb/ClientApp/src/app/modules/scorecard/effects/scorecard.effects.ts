import {Injectable} from '@angular/core';
import {Actions, createEffect, ofType} from '@ngrx/effects';
import {catchError, map, mergeMap} from 'rxjs/operators';
import {EMPTY} from 'rxjs';
import {
  LoadByDecadeAction, LoadByDecadeSuccessAction, LoadByYearAction, LoadByYearSuccessAction,
  LoadScorecardAction,
  LoadScorecardListAction,
  LoadScorecardListSuccessAction, LoadScorecardSuccessAction,
  LoadScorecardTournamentListAction
} from '../actions/scorecard.actions';
import {ScorecardSearchService} from '../services/scorecard-search.service';

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
          catchError(() => EMPTY)
        ))
    );
  });

  loadTournaments$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadScorecardTournamentListAction),
      mergeMap(action => this.scorecardSearchService.findTournament(action.payload)
        .pipe(
          map(players => LoadScorecardListSuccessAction({payload: players.result})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadScorecard$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadScorecardAction),
      mergeMap(action => this.scorecardSearchService.findCard(action.payload)
        .pipe(
          map(players => LoadScorecardSuccessAction({payload: players.result})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadByDecade$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByDecadeAction),
      mergeMap(action => this.scorecardSearchService.findByDecade(action.payload)
        .pipe(
          map(decades => LoadByDecadeSuccessAction({payload: decades.result})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadByYear$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadByYearAction),
      mergeMap(action => this.scorecardSearchService.findByYear(action.payload.year, action.payload.type)
        .pipe(
          map(decades => LoadByYearSuccessAction({payload: decades.result})),
          catchError(() => EMPTY)
        ))
    );
  });

}
