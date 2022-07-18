import {Component, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {InningsByInningsUiModel} from "../../models/bowling-overall-ui.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../../models/app-state";
import {LoadInnByInnBowlingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {IndividualBowlingDetailsDto} from "../../models/individual-bowling-details.dto";
import {SortOrder} from "../../../../models/sortorder.model";
import {FindRecords} from "../../../../models/find-records.model";

@Component({
  selector: 'app-bowling-inn-by-inn',
  templateUrl: './bowling-inn-by-inn.component.html',
  styleUrls: ['./bowling-inn-by-inn.component.css']
})
export class BowlingInnByInnComponent implements OnInit {

  bowlingSummary$!: Observable<RecordsSummaryModel>;
  bowlingInnByInn$!: Observable<InningsByInningsUiModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;

  constructor(private router: Router,
              private route: ActivatedRoute,
              private location: Location,
              private bowlingStore: Store<BowlingOverallState>) { }

  ngOnInit(): void {
    this.bowlingInnByInn$ = this.bowlingStore.select(s => {
        return s.bowlingrecords.inningsByInnings
      }
    )
    this.bowlingSummary$ = this.bowlingStore.select(s => {
      return s.playerRecordSummary;
    })

    this.route.queryParams.subscribe(params => {

      let fbo = params as FindRecords

      this.venue = this.setVenue(fbo.homeVenue.toLowerCase() == "true",
        fbo.awayVenue.toLowerCase() == "true",
        fbo.neutralVenue.toLowerCase() == "true")

      this.bowlingStore.dispatch(LoadInnByInnBowlingRecordsAction({payload: fbo}))
      this.bowlingStore.dispatch(LoadRecordSummariesAction({
        payload: {
          matchType: fbo.matchType,
          teamId: fbo.teamId,
          opponentsId: fbo.opponentsId,
          groundId: fbo.groundId,
          hostCountryId: fbo.hostCountryId
        }
      }))

      this.bowlingInnByInn$.subscribe(payload => {
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
    let url = this.router.url
      .replace(/sortOrder=\d+/, `sortOrder=${sortOrder}`)
      .replace(/sortDirection=\w+/, `sortDirection=${sortDirection}`)
      .replace(/startRow=\w+/, "startRow=0")

    this.router.navigateByUrl(url);
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

  getOvers(row: IndividualBowlingDetailsDto) {
    let oversPart = Math.floor(row.playerBalls / row.ballsPerOver);
    var ballsPart = row.playerBalls % row.ballsPerOver;

    return row.playerBalls == 0 ? "-" : `${oversPart}.${ballsPart}`;
  }

  getEcon(runs: number, balls: number) {
    let economy = null
    if (balls != null && balls != 0)
    {
      economy = (runs / balls) * 6;
    }
    return economy != null ? economy.toFixed(2) : "-";
  }
}
