import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {EMPTY, of} from "rxjs";
import {
  LoadSeriesDatesAction,
  LoadSeriesDatesSuccessAction,
  LoadMatchDatesAction,
  LoadMatchDatesSuccessAction, LoadMatchDatesFailureAction
} from "../actions/dates.actions";
import {DatesService} from "../services/dates.service";
import {Envelope} from "../models/envelope";
import {MatchDate} from "../models/date.model";
import {RecordHelperService} from "../services/record-helper.service";
import {
  LoadRecordSummariesAction,
  LoadRecordSummariesFailureAction,
  LoadRecordSummariesSuccessAction
} from "../actions/recordsummary.actions";
import {LoadCountriesFailureAction} from "../actions/countries.actions";
import {createError} from "../helpers/ErrorHelper";

@Injectable()
export class RecordSummaryEffects {
  constructor(
    private recordHelperService: RecordHelperService
    , private actions$: Actions
  ) {
  }

  // todo: in the map check that the envelope is valid and if not return an error
  loadrecordSummary$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(LoadRecordSummariesAction),
      mergeMap(action =>
        this.recordHelperService.getSummary(
          action.payload.matchType,
          action.payload.teamId,
          action.payload.opponentsId,
          action.payload.groundId,
          action.payload.hostCountryId)
          .pipe(
            map(result => LoadRecordSummariesSuccessAction({payload: result.result})),
            catchError(() => of(LoadRecordSummariesFailureAction({payload: createError(1)})))
          ))
    );
  });
}
