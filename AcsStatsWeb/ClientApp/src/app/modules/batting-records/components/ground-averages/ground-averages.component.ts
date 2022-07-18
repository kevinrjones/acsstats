import {Component, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {BattingOverallUiModel} from "../../models/batting-overall-ui.model";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {FindRecords} from "../../../../models/find-records.model";
import {LoadByGroundBattingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {BattingCareerRecordDto} from "../../models/batting-overall.model";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {SortOrder} from "../../../../models/sortorder.model";
import {BattingHelperService} from "../../services/batting-helper.service";

@Component({
  selector: 'app-ground-averages',
  templateUrl: './ground-averages.component.html',
  styleUrls: ['./ground-averages.component.css']
})
export class GroundAveragesComponent implements OnInit {
  battingByGround$!: Observable<BattingOverallUiModel>;
  battingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;
  pageSize!: number;
  pageNumber!: number;
  findBattingParams!: FindRecords;
  private batInnByInnSub$!: Subscription;
  count!: number;
  currentPage!: number;
  maxlength= 20


  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>,
              private battingHelperService: BattingHelperService) {
  }

  ngOnInit(): void {

    this.battingByGround$ = this.battingStore.select(s => {
        return s.battingrecords.byGround
      }
    )
    this.battingSummary$ = this.battingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      this.findBattingParams = params as FindRecords

      this.venue = this.battingHelperService.setVenue(this.findBattingParams.homeVenue.toLowerCase() == "true",
        this.findBattingParams.awayVenue.toLowerCase() == "true",
        this.findBattingParams.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadByGroundBattingRecordsAction({payload: this.findBattingParams}))
      this.battingHelperService.loadSummaries(this.findBattingParams, this.battingStore)

      let pageInfo = this.battingHelperService.getPageInformation(this.findBattingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.battingByGround$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count

        this.currentPage = this.battingHelperService.getCurrentPage(this.findBattingParams)
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
    this.battingHelperService.sort(this.sortOrder, newSortOrder, this.sortDirection, this.router)
  }

  getSortClass(sortOrder: SortOrder): IconProp {
    return this.battingHelperService.getSortClass(sortOrder, this.sortDirection)
  }

  navigate(startRow: number) {
    this.battingHelperService.navigate(startRow, this.router)
  }


  getGround(row: BattingCareerRecordDto): string {
    return row.ground.length < this.maxlength ? row.ground : row.ground.substring(0, this.maxlength) + "...";
  }
}
