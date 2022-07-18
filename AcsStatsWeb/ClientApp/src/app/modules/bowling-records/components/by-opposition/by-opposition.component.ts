import {Component, OnInit} from '@angular/core';
import {Observable} from "rxjs";
import {BowlingOverallUiModel} from "../../models/bowling-overall-ui.model";
import {RecordsSummaryModel} from "../../../../models/records-summary.model";
import {ActivatedRoute, Router} from "@angular/router";
import {Location} from "@angular/common";
import {Store} from "@ngrx/store";
import {BowlingOverallState} from "../../models/app-state";
import {LoadByOppositionBowlingRecordsAction} from "../../actions/records.actions";
import {LoadRecordSummariesAction} from "../../../../actions/recordsummary.actions";
import {faArrowDown, faArrowUp} from "@fortawesome/free-solid-svg-icons";
import {IconProp} from "@fortawesome/fontawesome-svg-core";
import {SortOrder} from "../../../../models/sortorder.model";
import {FindRecords} from "../../../../models/find-records.model";


@Component({
  selector: 'app-by-opposition',
  templateUrl: './by-opposition.component.html',
  styleUrls: ['./by-opposition.component.css']
})
export class ByOppositionComponent implements OnInit {
  bowlingByOpposition$!: Observable<BowlingOverallUiModel>;
  bowlingSummary$!: Observable<RecordsSummaryModel>;
  sortOrder!: number;
  private sortDirection!: string;
  importedSortOrder = SortOrder;
  venue!: string;


  constructor(private router: Router, private route: ActivatedRoute,
              private location: Location,
              private bowlingStore: Store<BowlingOverallState>) {
  }

  ngOnInit(): void {

    this.bowlingByOpposition$ = this.bowlingStore.select(s => {
        return s.bowlingrecords.byOpposition
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

      this.bowlingStore.dispatch(LoadByOppositionBowlingRecordsAction({payload: fbo}))
      this.bowlingStore.dispatch(LoadRecordSummariesAction({
        payload: {
          matchType: fbo.matchType,
          teamId: fbo.teamId,
          opponentsId: fbo.opponentsId,
          groundId: fbo.groundId,
          hostCountryId: fbo.hostCountryId
        }
      }))

      this.bowlingByOpposition$.subscribe(payload => {
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
    if (!homeVenue && !awayVenue && !neutralVenue) return "All Venues";
    if (homeVenue && awayVenue && neutralVenue) return "All Venues"
    if (homeVenue && awayVenue) return "Home and Away"
    if (homeVenue && neutralVenue) return "Home and Neutral"
    if (awayVenue && neutralVenue) return "Away and Neutral"
    if (homeVenue) return "Home Venues"
    if (awayVenue) return "Away Venues"
    if (neutralVenue) return "Neutral Venues"

    return "Unknown"
  }


  getSortClass(sortOrder: SortOrder): IconProp {
    if (sortOrder == this.sortOrder) {
      return this.sortDirection == "DESC" ? faArrowDown : faArrowUp
    }
    return faArrowDown
  }

  getBb(wickets: number, runs: number) {
    return `${wickets}/${runs}`
  }

  getEcon(runs: number, balls: number) {
    let economy = null
    if (balls != null && balls != 0) {
      economy = (runs / balls) * 6;
    }
    return economy != null ? economy.toFixed(2) : "-";
  }

  getStrikeRate(wickets: number, balls: number) {
    let sr = null
    if (balls != null && balls != 0) {
      sr = (balls / wickets);
    }
    return sr != null ? sr.toFixed(2) : "-";

  }
}
