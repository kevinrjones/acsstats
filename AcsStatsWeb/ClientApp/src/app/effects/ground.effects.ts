import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {of} from "rxjs";
import {LoadGroundsAction, LoadGroundsSuccessAction} from "../actions/grounds.actions";
import {GroundSearchService} from "../services/groundsearch.service";
import {createError} from "../helpers/ErrorHelper";
import {RaiseErrorAction} from "../actions/error.actions";
import { Envelope } from "../models/envelope";
import { Ground } from "../models/ground.model";


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
          map((grounds: Envelope<Ground[]>) => {
            if (grounds.errorMessage != null && grounds.errorMessage != "")
              return RaiseErrorAction({payload: createError(2, "Unable to get grounds")})

            return LoadGroundsSuccessAction({payload: grounds.result})
          }),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

}
