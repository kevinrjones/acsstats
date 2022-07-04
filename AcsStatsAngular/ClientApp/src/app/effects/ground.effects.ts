import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {TeamSearchService} from "../services/teamsearch.service";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY} from "rxjs";
import {LoadGroundsAction, LoadGroundsSuccessAction} from "../actions/grounds.actions";
import {GroundSearchService} from "../services/groundsearch.service";


@Injectable()
export class GroundEffects {
  constructor(
    private groundSearchService: GroundSearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadTeams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadGroundsAction),
      mergeMap(action => this.groundSearchService.findGroundsForMatchType(action.payload)
        .pipe(
          map(grounds => LoadGroundsSuccessAction({payload: grounds.result})),
          catchError(() => EMPTY)
        ))
    );
  });

}
