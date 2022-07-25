import {Component, OnInit} from '@angular/core';
import {InningsByInningsUiModel} from "../../models/batting-overall-ui.model";
import {LoadByMatchBattingRecordsAction} from "../../actions/records.actions";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IndividualBattingDetailsDto} from "../../models/individual-batting-details.dto";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {Observable, Subscription} from "rxjs";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {FindRecords} from "../../../../models/find-records.model";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {SortOrder} from "../../../../models/sortorder.model";
import {BattingHelperService} from "../../services/batting-helper.service";
import {RecordHelperService} from "../../../../services/record-helper.service";

@Component({
  selector: 'app-match-totals',
  templateUrl: './match-totals.component.html',
  styleUrls: ['./match-totals.component.css']
})
export class MatchTotalsComponent implements OnInit {
  battingSummary$!: Observable<RecordsSummaryModel>;
  battingByMatch$!: Observable<InningsByInningsUiModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;
  pageSize!: number;
  pageNumber!: number;
  findBattingParams!: FindRecords;
  count!: number;
  currentPage!: number;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>,
              private battingHelperService: BattingHelperService,
              private recordHelperService: RecordHelperService) { }

  ngOnInit(): void {
    this.battingByMatch$ = this.battingStore.select(s => {
        return s.battingrecords.byMatch
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

      this.battingStore.dispatch(LoadByMatchBattingRecordsAction({payload: this.findBattingParams}))
      this.battingHelperService.loadSummaries(this.findBattingParams, this.battingStore)

      let pageInfo = this.recordHelperService.getPageInformation(this.findBattingParams)

      this.pageSize = pageInfo.pageSize
      this.pageNumber = pageInfo.pageNumber

      this.battingByMatch$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
        this.count = payload.sqlResults.count

        this.currentPage = this.recordHelperService.getCurrentPage(this.findBattingParams)
      })

    });
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

  getScore(row: IndividualBattingDetailsDto) {
    return row.notOut ? `${row.playerScore}*` : `${row.playerScore} `;
  }
}
