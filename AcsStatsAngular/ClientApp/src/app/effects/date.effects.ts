import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY} from "rxjs";
import {
  LoadSeriesDatesAction,
  LoadSeriesDatesSuccessAction,
  LoadMatchDatesAction,
  LoadMatchDatesSuccessAction, LoadMatchDatesFailureAction
} from "../actions/dates.actions";
import {DatesService} from "../services/dates.service";
import {Envelope} from "../models/envelope";
import {MatchDate} from "../models/date.model";

@Injectable()
export class DateEffects {
  constructor(
    private datesService: DatesService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadSeriesDates$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadSeriesDatesAction),
      mergeMap(action => this.datesService.getSeriesDatesForMatchType(action.payload)
        .pipe(
          map(seriesDates => LoadSeriesDatesSuccessAction({payload: seriesDates.result})),
          catchError(() => EMPTY)
        ))
    );
  });

  loadMatchDates$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadMatchDatesAction),
      mergeMap(action => this.datesService.getMatchDatesForMatchType(action.payload)
        .pipe(
          map((matchDates: Envelope<MatchDate[]>) => {
              if (matchDates.errorMessage != null && matchDates.errorMessage != "")
                return LoadMatchDatesFailureAction

              let dates = matchDates.result
              if (dates.length != 2)
                return LoadMatchDatesFailureAction

              return LoadMatchDatesSuccessAction({payload: dates})
            }
          ),
          catchError(() => EMPTY)
        ))
    );
  });

}
