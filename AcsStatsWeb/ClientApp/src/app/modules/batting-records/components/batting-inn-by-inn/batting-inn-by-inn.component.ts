import {Component, OnDestroy, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {InningsByInningsUiModel} from "../../models/batting-overall-ui.model";
import {Observable, Subscription} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {BattingOverallState} from "../../models/app-state";
import {LoadInnByInnBattingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IndividualBattingDetailsDto} from "../../models/individual-batting-details.dto";
import {SortOrder} from "../../../../models/sortorder.model";
import {BattingHelperService} from "../../services/batting-helper.service";
import {FindRecords} from "../../../../models/find-records.model";
import {RecordHelperService} from "../../../../services/record-helper.service";

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
  findBattingParams!: FindRecords;
  private batInnByInnSub$!: Subscription;
  count!: number;
  currentPage!: number;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>,
              private battingHelperService: BattingHelperService,
              private recordHelperService: RecordHelperService) {
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

      this.findBattingParams = params as FindRecords


      this.venue = this.recordHelperService.setVenue(this.findBattingParams.homeVenue.toLowerCase() == "true",
        this.findBattingParams.awayVenue.toLowerCase() == "true",
        this.findBattingParams.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadInnByInnBattingRecordsAction({payload: this.findBattingParams}))

      this.battingHelperService.loadSummaries(this.findBattingParams, this.battingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBattingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.batInnByInnSub$ = this.battingInnByInn$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count;
        this.currentPage = this.recordHelperService.getCurrentPage(this.findBattingParams)
      })

    });

  }

  sort(newSortOrder: SortOrder) {
    this.recordHelperService.sort(this.sortOrder, newSortOrder, this.sortDirection, this.router)
  }

  formatHighestScore(row: IndividualBattingDetailsDto) {
    return this.battingHelperService.formatHighestScore(row.notOut, row.playerScore)
  }

  getSortClass(sortOrder: SortOrder): IconProp {
    return this.recordHelperService.getSortClass(sortOrder, this.sortDirection)
  }

  navigate(startRow: number) {
    this.recordHelperService.navigate(startRow, this.router)
  }

  getIndex(ndx: number) {
    return ((this.currentPage - 1) * this.pageSize) + ndx + 1
  }
}
