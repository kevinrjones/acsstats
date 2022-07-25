import {Component, OnInit} from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {InningsByInningsUiModel} from "../../models/bowling-overall-ui.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../../models/app-state";
import {LoadByMatchBowlingRecordsAction} from "../../actions/records.actions";
import {IndividualBowlingDetailsDto} from "../../models/individual-bowling-details.dto";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {SortOrder} from "../../../../models/sortorder.model";
import {FindRecords} from "../../../../models/find-records.model";
import {RecordHelperService} from "../../../../services/record-helper.service";
import {BowlingHelperService} from "../../services/bowling-helper.service";

@Component({
  selector: 'app-match-totals',
  templateUrl: './match-totals.component.html',
  styleUrls: ['./match-totals.component.css']
})
export class MatchTotalsComponent implements OnInit {

  bowlingSummary$!: Observable<RecordsSummaryModel>;
  bowlingMatch$!: Observable<InningsByInningsUiModel>;
  sortOrder!: number;
  pageSize!: number;
  pageNumber!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;
  findBowlingParams!: FindRecords
  private bowlMatchSub$!: Subscription;
  count!: number;
  currentPage!: number;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private bowlingStore: Store<BowlingOverallState>,
              private bowlingHelperService: BowlingHelperService,
              private recordHelperService: RecordHelperService) { }

  ngOnDestroy(): void {
    this.bowlMatchSub$.unsubscribe()
  }


  ngOnInit(): void {
    this.bowlingMatch$ = this.bowlingStore.select(s => {
        return s.bowlingrecords.byMatch
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

      this.bowlingStore.dispatch(LoadByMatchBowlingRecordsAction({payload: this.findBowlingParams}))
      this.bowlingHelperService.loadSummaries(this.findBowlingParams, this.bowlingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBowlingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.bowlMatchSub$ = this.bowlingMatch$.subscribe(payload => {
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

  getSortClass(sortOrder: SortOrder): IconProp {
    return this.recordHelperService.getSortClass(sortOrder, this.sortDirection)
  }

  getOvers(row: IndividualBowlingDetailsDto) {
    return this.bowlingHelperService.getOvers(row)
  }

  navigate(startRow: number) {
    this.recordHelperService.navigate(startRow, this.router)
  }

}

