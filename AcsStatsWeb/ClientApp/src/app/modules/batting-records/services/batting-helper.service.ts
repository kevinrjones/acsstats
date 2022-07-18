import {Injectable} from "@angular/core";
import {Router} from "@angular/router";
import {SortOrder} from "../../../models/sortorder.model";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../models/app-state";
import {LoadRecordSummariesAction} from "../../../actions/recordsummary.actions";
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {FindRecords} from "../../../models/find-records.model";

@Injectable({providedIn: 'root'})
export class BattingHelperService {
  getPageInformation(findBattingParams: FindRecords) {
    let pageSize = parseInt(findBattingParams.pageSize)
    let pageNumber = parseInt((findBattingParams.startRow) + 1) / parseInt(findBattingParams.pageSize)

    return {
      pageSize,
      pageNumber
    }
  }

  getCurrentPage(findBattingParams: FindRecords) {
    return (parseInt(findBattingParams.startRow) / parseInt(findBattingParams.pageSize)) + 1
  }

  navigate(startRow: number, router: Router) {
    let url = router.url
      .replace(/startRow=\d+/, `startRow=${startRow}`)

    router.navigateByUrl(url);
  }

  getSortClass(sortOrder: SortOrder, sortDirection: string) {
    if (sortOrder == sortOrder) {
      return sortDirection == "DESC" ? faArrowDown : faArrowUp
    }
    return faArrowDown
  }

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

  setVenue(homeVenue: boolean, awayVenue: boolean, neutralVenue: boolean) {
    if (!homeVenue && !awayVenue && !neutralVenue) return "All Venues";
    if (homeVenue && awayVenue && neutralVenue) return "All Venues"
    if (homeVenue && awayVenue) return "Home and Away"
    if (homeVenue && neutralVenue) return "Home and Neutral"
    if (awayVenue && neutralVenue) return "Away and Neutral"
    if (homeVenue) return "Home Venues"
    if (awayVenue) return "Away Venues"
    if (neutralVenue) return "Neutral Venues"

    return "Unknown"
  }

  sort(oldSortOrder: SortOrder, newSortOrder: SortOrder, sortDirection: string, router: Router) {
    let newSortDirection = sortDirection
    if (newSortOrder == oldSortOrder) {
      newSortDirection = sortDirection == "ASC" ? "DESC" : "ASC"
    }
    let url = router.url
      .replace(/sortOrder=\d+/, `sortOrder=${newSortOrder}`)
      .replace(/sortDirection=\w+/, `sortDirection=${newSortDirection}`)
      .replace(/startRow=\w+/, "startRow=0")

    router.navigateByUrl(url);
  }

  formatHighestScore(notOut: boolean, score: number) {
    return notOut ? `${score}*` : `${score}&nbsp;`;
  }

  formatHighestScoreForInnings(innings: number, notOut: boolean, score: number) {
    return innings == 0 ? "-" :
      notOut ? `${score}*` : `${score}&nbsp;`;
  }

}
