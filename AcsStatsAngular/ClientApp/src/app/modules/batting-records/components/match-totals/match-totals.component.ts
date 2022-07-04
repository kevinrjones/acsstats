import { Component, OnInit } from '@angular/core';
import {InningsByInningsUiModel} from "../../models/batting-overall-ui.model";
import {
  LoadByMatchBattingRecordsAction, SortBattingRecordsByMatchAction
} from "../../actions/records.actions";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IndividualBattingDetailsDto} from "../../models/individual-batting-details.dto";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {Observable} from "rxjs";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {FindBatting} from "../../models/find-batting-overall.model";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {SortOrder} from "../../../../models/sortorder.model";

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

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>) { }

  ngOnInit(): void {
    this.battingByMatch$ = this.battingStore.select(s => {
        return s.battingrecords.byMatch
      }
    )
    this.battingSummary$ = this.battingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      let fbo = params as FindBatting

      this.venue = this.setVenue(fbo.homeVenue.toLowerCase() == "true",
        fbo.awayVenue.toLowerCase() == "true",
        fbo.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadByMatchBattingRecordsAction({payload: fbo}))
      this.battingStore.dispatch(LoadRecordSummariesAction({
        payload: {
          matchType: fbo.matchType,
          teamId: fbo.teamId,
          opponentsId: fbo.opponentsId,
          groundId: fbo.groundId,
          hostCountryId: fbo.hostCountryId
        }
      }))

      this.battingByMatch$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
      })

    });
  }

  sort(sortOrder: SortOrder) {
    let sortDirection = this.sortDirection
    if (sortOrder == this.sortOrder) {
      sortDirection = this.sortDirection == "ASC" ? "DESC" : "ASC"
    }
    let url = this.router.url.replace(/sortOrder=\d+/, `sortOrder=${sortOrder}`)
      .replace(/sortDirection=\w+/, `sortDirection=${sortDirection}`)

    this.location.go(url)

    this.battingStore.dispatch(SortBattingRecordsByMatchAction({
      payload: {
        sortOrder,
        sortDirection
      }
    }))
  }

  setVenue(homeVenue: boolean, awayVenue: boolean, neutralVenue: boolean) {
    if(!homeVenue && !awayVenue && !neutralVenue) return "All Venues";
    if(homeVenue && awayVenue && neutralVenue) return "All Venues"
    if(homeVenue && awayVenue) return"Home and Away"
    if(homeVenue && neutralVenue) return"Home and Neutral"
    if(awayVenue && neutralVenue) return"Away and Neutral"
    if(homeVenue) return"Home Venues"
    if(awayVenue) return"Away Venues"
    if(neutralVenue) return"Neutral Venues"

    return "Unknown"
  }


  getSortClass(sortOrder: SortOrder) : IconProp {
    if(sortOrder == this.sortOrder){
      return this.sortDirection == "DESC" ? faArrowDown : faArrowUp
    }
    return faArrowDown
  }

  getScore(row: IndividualBattingDetailsDto) {
    return row.notOut ? `${row.playerScore}*` : `${row.playerScore} `;
  }
}
