import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY, of} from "rxjs";
import {CountrySearchService} from "../services/countrysearch.service";
import {
  LoadCountriesAction,
  LoadCountriesSuccessAction
} from "../actions/countries.actions";
import {createError} from "../helpers/ErrorHelper";
import {RaiseErrorAction} from "../actions/error.actions";
import {Envelope} from "../models/envelope";
import {Country} from "../models/country.model";
import {LoadMatchDatesSuccessAction} from "../actions/dates.actions";

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
          map((countries: Envelope<Country[]>) => {

              if (countries.errorMessage != null && countries.errorMessage != "")
                return RaiseErrorAction({payload: createError(2, "Unable to get countries")})

              return LoadCountriesSuccessAction({payload: countries.result})

            }
          ),
          catchError((err) => of(RaiseErrorAction({payload: createError(1)})))
        ))
    );
  });

}
