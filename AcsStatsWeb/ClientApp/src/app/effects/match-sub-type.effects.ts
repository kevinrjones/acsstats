import {Injectable} from "@angular/core";
import {PlayerSearchService} from "../modules/player/services/playersearch.service";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {TeamSearchService} from "../services/teamsearch.service";
import {LoadPlayersAction, LoadPlayersSuccessAction} from "../modules/player/actions/players.actions";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY} from "rxjs";
import {LoadTeamsAction, LoadTeamsSuccessAction} from "../actions/teams.actions";
import {MatchSubTypeSearchService} from "../services/match-sub-type-search.service";
import {LoadMatchSubTypesAction, LoadMatchSubTypesSuccessAction} from "../actions/match-sub-types.actions";

@Injectable()
export class MatchSubTypeEffects {
  constructor(
    private subMatchSearchService: MatchSubTypeSearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadMatchSubType$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadMatchSubTypesAction),
      mergeMap(action => this.subMatchSearchService.getMatchSubTypesForMatchType(action.payload)
        .pipe(
          map((matchSubType) => LoadMatchSubTypesSuccessAction({payload: matchSubType})),
          catchError(() => EMPTY)
        ))
    );
  });

}
