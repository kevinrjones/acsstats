import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY} from "rxjs";
import {CountrySearchService} from "../services/countrysearch.service";
import {LoadCountriesAction, LoadCountriesSuccessAction} from "../actions/countries.actions";

@Injectable()
export class CountryEffects {
  constructor(
    private countrySearchService: CountrySearchService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadTeams$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadCountriesAction),
      mergeMap(action => this.countrySearchService.findCountriesForMatchType(action.payload)
        .pipe(
          map(countries => LoadCountriesSuccessAction({payload: countries.result})),
          catchError(() => EMPTY)
        ))
    );
  });

}
