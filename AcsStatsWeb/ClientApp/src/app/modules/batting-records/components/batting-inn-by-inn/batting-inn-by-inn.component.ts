import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {InningsByInningsUiModel} from "../../models/batting-overall-ui.model";
import {Observable, Subscription} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {BattingOverallState} from "../../models/app-state";
import {FindBatting} from "../../models/find-batting-overall.model";
import {LoadInnByInnBattingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IndividualBattingDetailsDto} from "../../models/individual-batting-details.dto";
import {SortOrder} from "../../../../models/sortorder.model";

@Component({
  selector: 'app-batting-inn-by-inn',
  templateUrl: './batting-inn-by-inn.component.html',
  styleUrls: ['./batting-inn-by-inn.component.css']
})
export class BattingInnByInnComponent implements OnInit, OnDestroy {

  battingSummary$!: Observable<RecordsSummaryModel>;
  battingInnByInn$!: Observable<InningsByInningsUiModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  pageSize!: number;
  pageNumber!: number;
  venue!: string;
  findBattingParams!: FindBatting;
  private batInnByInnSub$!: Subscription;
  count!: number;
  currentPage!: number;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>) {
  }

  ngOnDestroy() : void {
    this.batInnByInnSub$.unsubscribe()
  }

  ngOnInit(): void {
    this.battingInnByInn$ = this.battingStore.select(s => {
        return s.battingrecords.inningsByInnings
      }
    )
    this.battingSummary$ = this.battingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      this.findBattingParams = params as FindBatting

      this.pageSize = parseInt(this.findBattingParams.pageSize)
      this.pageNumber = parseInt((this.findBattingParams.startRow) + 1) / parseInt(this.findBattingParams.pageSize)

      this.venue = this.setVenue(this.findBattingParams.homeVenue.toLowerCase() == "true",
        this.findBattingParams.awayVenue.toLowerCase() == "true",
        this.findBattingParams.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadInnByInnBattingRecordsAction({payload: this.findBattingParams}))
      this.battingStore.dispatch(LoadRecordSummariesAction({
        payload: {
          matchType: this.findBattingParams.matchType,
          teamId: this.findBattingParams.teamId,
          opponentsId: this.findBattingParams.opponentsId,
          groundId: this.findBattingParams.groundId,
          hostCountryId: this.findBattingParams.hostCountryId
        }
      }))

      this.batInnByInnSub$ = this.battingInnByInn$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count;

        this.currentPage = (parseInt(this.findBattingParams.startRow)/parseInt(this.findBattingParams.pageSize)) + 1

      })

    });

  }

  sort(sortOrder: SortOrder) {
    let sortDirection = this.sortDirection
    if (sortOrder == this.sortOrder) {
      sortDirection = this.sortDirection == "ASC" ? "DESC" : "ASC"
    }
    let url = this.router.url
      .replace(/sortOrder=\d+/, `sortOrder=${sortOrder}`)
      .replace(/sortDirection=\w+/, `sortDirection=${sortDirection}`)
      .replace(/startRow=\w+/, "startRow=0")

    this.router.navigateByUrl(url);
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


  getSortClass(sortOrder: SortOrder): IconProp {
    if (sortOrder == this.sortOrder) {
      return this.sortDirection == "DESC" ? faArrowDown : faArrowUp
    }
    return faArrowDown
  }

  getHighestScore(row: IndividualBattingDetailsDto) {
    return row.notOut ? `${row.playerScore}*` : `${row.playerScore} `;
  }
  
  navigate(startRow: number) {

    let url = this.router.url
      .replace(/startRow=\d+/, `startRow=${startRow}`)

    this.router.navigateByUrl(url);
  }

}
