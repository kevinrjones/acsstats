import {Component, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {BowlingOverallUiModel, InningsByInningsUiModel} from "../../models/bowling-overall-ui.model";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../../models/app-state";
import {LoadBySeriesBowlingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {SortOrder} from "../../../../models/sortorder.model";
import {FindRecords} from "../../../../models/find-records.model";
import {BowlingHelperService} from "../../services/bowling-helper.service";
import {RecordHelperService} from "../../../../services/record-helper.service";
import {IndividualBowlingDetailsDto} from "../../models/individual-bowling-details.dto";

@Component({
  selector: 'app-series-averages',
  templateUrl: './series-averages.component.html',
  styleUrls: ['./series-averages.component.css']
})
export class SeriesAveragesComponent implements OnInit {
  bowlingSeries$!: Observable<BowlingOverallUiModel>;
  bowlingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;
  pageSize!: number;
  pageNumber!: number;
  findBowlingParams!: FindRecords
  private bowlMatchSub$!: Subscription;
  count!: number;
  currentPage!: number;


  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private bowlingStore: Store<BowlingOverallState>,
              private bowlingHelperService: BowlingHelperService,
              private recordHelperService: RecordHelperService) {
  }

  ngOnInit(): void {

    this.bowlingSeries$ = this.bowlingStore.select(s => {
        return s.bowlingrecords.bySeries
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

      this.bowlingStore.dispatch(LoadBySeriesBowlingRecordsAction({payload: this.findBowlingParams}))
      this.bowlingHelperService.loadSummaries(this.findBowlingParams, this.bowlingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBowlingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.bowlMatchSub$ = this.bowlingSeries$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count;
        this.currentPage = this.recordHelperService.getCurrentPage(this.findBowlingParams)
      })

    });
  }

  getBb = (wickets: number, runs: number) => this.bowlingHelperService.getBb(wickets, runs);

  getEcon = (runs: number, balls: number) => this.bowlingHelperService.getEcon(runs, balls);

  sort(newSortOrder: SortOrder) {
    this.recordHelperService.sort(this.sortOrder, newSortOrder, this.sortDirection, this.router)
  }

  getSortClass(sortOrder: SortOrder): IconProp {
    return this.recordHelperService.getSortClass(sortOrder, this.sortDirection)
  }

  getOvers(row: IndividualBowlingDetailsDto) {
    return this.bowlingHelperService.getOvers(row)
  }

  navigate(startRow: number) {
    this.recordHelperService.navigate(startRow, this.router)
  }

  getIndex(ndx: number) {
    return ((this.currentPage - 1) * this.pageSize) + ndx + 1
  }
}
