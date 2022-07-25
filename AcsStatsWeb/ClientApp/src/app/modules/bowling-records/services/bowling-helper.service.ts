import {Injectable} from "@angular/core";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../models/app-state";
import {LoadRecordSummariesAction} from "../../../actions/recordsummary.actions";
import {FindRecords} from "../../../models/find-records.model";
import {IndividualBowlingDetailsDto} from "../models/individual-bowling-details.dto";

@Injectable({providedIn: 'root'})
export class BowlingHelperService {

   loadSummaries(findBattingParams: FindRecords, bowlingStore: Store<BowlingOverallState>) {
    bowlingStore.dispatch(LoadRecordSummariesAction({
      payload: {
        matchType: findBattingParams.matchType,
        teamId: findBattingParams.teamId,
        opponentsId: findBattingParams.opponentsId,
        groundId: findBattingParams.groundId,
        hostCountryId: findBattingParams.hostCountryId
      }
    }))
  }

  getOvers(row: IndividualBowlingDetailsDto) {
    let oversPart = Math.floor(row.playerBalls / row.ballsPerOver);
    var ballsPart = row.playerBalls % row.ballsPerOver;

    return row.playerBalls == 0 ? "-" : `${oversPart}.${ballsPart}`;
  }

  getBb(wickets: number, runs: number) {
    return `${wickets}/${runs}`
  }

  getEcon(runs: number, balls: number) {
    let economy = null
    if (balls != null && balls != 0)
    {
      economy = (runs / balls) * 6;
    }
    return economy != null ? economy.toFixed(2) : "-";
  }


}
