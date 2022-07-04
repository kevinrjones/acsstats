import {Injectable} from "@angular/core";
import {PlayerSearchService} from "../modules/player/services/playersearch.service";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {TeamSearchService} from "../services/teamsearch.service";
import {LoadPlayersAction, LoadPlayersSuccessAction} from "../modules/player/actions/players.actions";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY} from "rxjs";
import {LoadTeamsAction, LoadTeamsSuccessAction} from "../actions/teams.actions";

@Injectable()
export class TeamEffects {
  constructor(
    private teamSearchService: TeamSearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadTeams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadTeamsAction),
      mergeMap(action => this.teamSearchService.findTeamsForMatchType(action.payload)
        .pipe(
          map(teams => LoadTeamsSuccessAction({payload: teams.result})),
          catchError(() => EMPTY)
        ))
    );
  });

}
