import {Component, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {BowlingOverallUiModel} from "../../models/bowling-overall-ui.model";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../../models/app-state";
import {LoadByYearBowlingRecordsAction} from "../../actions/records.actions";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {SortOrder} from "../../../../models/sortorder.model";
import {FindRecords} from "../../../../models/find-records.model";
import {BowlingHelperService} from "../../services/bowling-helper.service";
import {RecordHelperService} from "../../../../services/record-helper.service";

@Component({
  selector: 'app-by-year-of-match-start',
  templateUrl: './by-year-of-match-start.component.html',
  styleUrls: ['./by-year-of-match-start.component.css']
})
export class ByYearOfMatchStartComponent implements OnInit {
  bowlingStore$!: Observable<BowlingOverallUiModel>;
  bowlingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  pageSize!: number;
  pageNumber!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;
  findBowlingParams!: FindRecords
  private bowlInnByInnSub$!: Subscription;
  count!: number;
  currentPage!: number;
  private bowlingStoreSub$!: Subscription;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private bowlingStore: Store<BowlingOverallState>,
              private bowlingHelperService: BowlingHelperService,
              private recordHelperService: RecordHelperService) {
  }

  ngOnDestroy() {
    this.bowlingStoreSub$.unsubscribe();
  }

  ngOnInit(): void {

    this.bowlingStore$ = this.bowlingStore.select(s => {
        return s.bowlingrecords.byYear
      }
    )
    this.bowlingSummary$ = this.bowlingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      this.findBowlingParams = params as FindRecords

      this.venue = this.recordHelperService.setVenue(this.findBowlingParams.homeVenue.toLowerCase() == "true",
        this.findBowlingParams.awayVenue.toLowerCase() == "true",
        this.findBowlingParams.neutralVenue.toLowerCase() == "true")

      this.bowlingStore.dispatch(LoadByYearBowlingRecordsAction({payload: this.findBowlingParams}))
      this.bowlingHelperService.loadSummaries(this.findBowlingParams, this.bowlingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBowlingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.bowlingStoreSub$ = this.bowlingStore$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count;
        this.currentPage = this.recordHelperService.getCurrentPage(this.findBowlingParams)
      })

    });
  }

  sort(newSortOrder: SortOrder) {
    this.recordHelperService.sort(this.sortOrder, newSortOrder, this.sortDirection, this.router)
  }

  getSortClass = (sortOrder: SortOrder): IconProp => this.recordHelperService.getSortClass(sortOrder, this.sortDirection);


  getBb = (wickets: number, runs: number) => this.bowlingHelperService.getBb(wickets, runs);

  getEcon = (runs: number, balls: number) => this.bowlingHelperService.getEcon(runs, balls);

  navigate(startRow: number) {
    this.recordHelperService.navigate(startRow, this.router)
  }
}
