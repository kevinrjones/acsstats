import {Injectable} from "@angular/core";
import {Actions, createEffect, ofType} from "@ngrx/effects";
import {catchError, map, mergeMap} from "rxjs/operators";
import {of} from "rxjs";
import {RecordHelperService} from "../services/record-helper.service";
import {LoadRecordSummariesAction, LoadRecordSummariesSuccessAction} from "../actions/recordsummary.actions";
import {createError, handleError} from "../helpers/ErrorHelper";
import {RaiseErrorAction} from "../actions/error.actions";
import { Envelope } from "../models/envelope";
import { RecordsSummaryModel } from "../models/records-summary.model";

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
            map((result: Envelope<RecordsSummaryModel>) => {
              if (result.errorMessage != null && result.errorMessage != "")
                return RaiseErrorAction({payload: createError(2, "Unable to get the record summary")})

              return LoadRecordSummariesSuccessAction({payload: result.result})
            }),
            catchError((err: any) => {
              let error = handleError(err, "Unknown Error")
              return of(RaiseErrorAction({payload: error}))
            })
          ))
    );
  });
}
