import {Injectable} from "@angular/core";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../models/app-state";
import {LoadRecordSummariesAction} from "../../../actions/recordsummary.actions";
import {FindRecords} from "../../../models/find-records.model";

@Injectable({providedIn: 'root'})
export class BattingHelperService {

   loadSummaries(findBattingParams: FindRecords, battingStore: Store<BattingOverallState>) {
    battingStore.dispatch(LoadRecordSummariesAction({
      payload: {
        matchType: findBattingParams.matchType,
        teamId: findBattingParams.teamId,
        opponentsId: findBattingParams.opponentsId,
        groundId: findBattingParams.groundId,
        hostCountryId: findBattingParams.hostCountryId
      }
    }))
  }

  formatHighestScore(notOut: boolean, score: number) {
    return notOut ? `${score}*` : `${score}&nbsp;`;
  }

  formatHighestScoreForInnings(innings: number, notOut: boolean, score: number) {
    return innings == 0 ? "-" :
      notOut ? `${score}*` : `${score}&nbsp;`;
  }

}
