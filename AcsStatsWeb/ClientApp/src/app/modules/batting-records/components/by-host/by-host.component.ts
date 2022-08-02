import {Component, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {BattingOverallUiModel} from "../../models/batting-overall-ui.model";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {FindRecords} from "../../../../models/find-records.model";
import {LoadByHostBattingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {BattingCareerRecordDto} from "../../models/batting-overall.model";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {SortOrder} from "../../../../models/sortorder.model";
import {BattingHelperService} from "../../services/batting-helper.service";
import {RecordHelperService} from "../../../../services/record-helper.service";

@Component({
  selector: 'app-by-host',
  templateUrl: './by-host.component.html',
  styleUrls: ['./by-host.component.css']
})
export class ByHostComponent implements OnInit {
  battingByHost$!: Observable<BattingOverallUiModel>;
  battingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  pageSize!: number;
  pageNumber!: number;
  venue!: string;
  findBattingParams!: FindRecords;
  count!: number;
  currentPage!: number;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>,
              private battingHelperService: BattingHelperService,
              private recordHelperService: RecordHelperService) {
  }

  ngOnInit(): void {

    this.battingByHost$ = this.battingStore.select(s => {
        return s.battingrecords.byHost
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

      this.battingStore.dispatch(LoadByHostBattingRecordsAction({payload: this.findBattingParams}))
      this.battingHelperService.loadSummaries(this.findBattingParams, this.battingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBattingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.battingByHost$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
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
