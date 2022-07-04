import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from '@angular/common';
import {Store} from "@ngrx/store";
import {BattingOverallState} from "../../models/app-state";
import {FindBatting} from "../../models/find-batting-overall.model";
import {LoadOverallBattingRecordsAction} from "../../actions/records.actions";
import {Observable} from "rxjs";
import {BattingOverallUiModel} from "../../models/batting-overall-ui.model";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {faArrowDown, faArrowUp} from '@fortawesome/free-solid-svg-icons';
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {BattingCareerRecordDto} from "../../models/batting-overall.model";
import {SortOrder} from "../../../../models/sortorder.model";

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


  constructor(private router: Router, private route: ActivatedRoute,
              private location: Location,
              private battingStore: Store<BattingOverallState>) {
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

      let fbo = params as FindBatting

      this.venue = this.setVenue(fbo.homeVenue.toLowerCase() == "true",
        fbo.awayVenue.toLowerCase() == "true",
        fbo.neutralVenue.toLowerCase() == "true")

      this.battingStore.dispatch(LoadOverallBattingRecordsAction({payload: fbo}))
      this.battingStore.dispatch(LoadRecordSummariesAction({
        payload: {
          matchType: fbo.matchType,
          teamId: fbo.teamId,
          opponentsId: fbo.opponentsId,
          groundId: fbo.groundId,
          hostCountryId: fbo.hostCountryId
        }
      }))

      this.battingOverall$.subscribe(payload => {
        this.sortOrder = payload.sortOrder
        this.sortDirection = payload.sortDirection
      })

    });
  }

  formatHighestScore(row: BattingCareerRecordDto) {
    return row.innings == 0 ? "-" :
      row.notOut ? `${row.highestScore}*` : `${row.highestScore} `;

  }

  getStrikeRate(runs: number, balls: number) {
    if (balls == 0) return "-"
    return ((runs / balls) * 100).toFixed(2)
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

    // this.location.go(url)
    //
    // // this.battingStore.dispatch(LoadOverallBattingRecordsAction({payload: fbo}))
    // this.battingStore.dispatch(SortBattingRecordsOverallAction({
    //   payload: {
    //     sortOrder,
    //     sortDirection
    //   }
    // }))
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

}
