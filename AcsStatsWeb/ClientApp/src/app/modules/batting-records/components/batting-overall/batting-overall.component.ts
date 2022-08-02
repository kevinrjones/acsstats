import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from '@angular/common';
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {LoadOverallBattingRecordsAction} from "../../actions/records.actions";
import {Observable, Subscription} from "rxjs";
import {BattingOverallUiModel} from "../../models/batting-overall-ui.model";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {faArrowDown, faArrowUp} from '@fortawesome/free-solid-svg-icons';
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {BattingCareerRecordDto} from "../../models/batting-overall.model";
import {SortOrder} from "../../../../models/sortorder.model";
import {BattingHelperService} from "../../services/batting-helper.service";
import {FindRecords} from "../../../../models/find-records.model";
import {RecordHelperService} from "../../../../services/record-helper.service";

@Component({
  selector: 'app-batting-overall',
  templateUrl: './batting-overall.component.html',
  styleUrls: ['./batting-overall.component.css']
})
export class BattingOverallComponent implements OnInit {
  battingOverall$!: Observable<BattingOverallUiModel>;
  battingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;

  pageSize!: number;
  pageNumber!: number;
  findBattingParams!: FindRecords;
  count!: number;
  currentPage!: number;
  private battingOverallSub$!: Subscription;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>,
              private battingHelperService: BattingHelperService,
              private recordHelperService: RecordHelperService) {
  }

  ngOnDestroy() {
    this.battingOverallSub$.unsubscribe();
  }

  ngOnInit(): void {

    this.battingOverall$ = this.battingStore.select(s => {
        return s.battingrecords.overall
      }
    )
    this.battingSummary$ = this.battingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      this.findBattingParams = params as FindRecords


      this.venue = this.recordHelperService.setVenue(this.findBattingParams.homeVenue.toLowerCase() == "true",
        this.findBattingParams.awayVenue.toLowerCase() == "true",
        this.findBattingParams.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadOverallBattingRecordsAction({payload: this.findBattingParams}))
      this.battingHelperService.loadSummaries(this.findBattingParams, this.battingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBattingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.battingOverallSub$ = this.battingOverall$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection

        this.count = payload.sqlResults.count;

        this.currentPage = this.recordHelperService.getCurrentPage(this.findBattingParams)
      })

    });
  }

  formatHighestScore(row: BattingCareerRecordDto) {
    return this.battingHelperService.formatHighestScoreForInnings(row.innings, row.notOut, row.highestScore)
  }

  getStrikeRate(runs: number, balls: number) {
    if (balls == 0) return "-"
    return ((runs / balls) * 100).toFixed(2)
  }

  sort(newSortOrder: SortOrder) {
    this.recordHelperService.sort(this.sortOrder, newSortOrder, this.sortDirection, this.router)
  }

  getSortClass(sortOrder: SortOrder): IconProp {
    return this.recordHelperService.getSortClass(sortOrder, this.sortDirection)
  }

  navigate(startRow: number) {
    this.recordHelperService.navigate(startRow, this.router)
  }


  getAverage = (innings: number, notOuts: number, avg: number) => this.recordHelperService.getAverage(innings, notOuts, avg);
  getIndex(ndx: number) {
    return ((this.currentPage - 1) * this.pageSize) + ndx + 1
  }
}
