import {Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {Team} from 'src/app/models/team.model';
import {LoadTeamsAction} from "../../../../actions/teams.actions";
import {Observable, Subscription} from "rxjs";
import {AppState} from "../../../../models/app-state";
import {Country} from "../../../../models/country.model";
import {LoadCountriesAction} from "../../../../actions/countries.actions";
import {LoadGroundsAction} from "../../../../actions/grounds.actions";
import {Ground} from 'src/app/models/ground.model';
import {LoadMatchDatesAction, LoadSeriesDatesAction} from "../../../../actions/dates.actions";
import {MatchDate} from "../../../../models/date.model";
import {DateTime} from "luxon";
import {FindBatting} from "../../models/find-batting-overall.model";
import {AppSettingsService} from "../../../../services/app-settings.service";
import {LoadMatchSubTypesAction} from "../../../../actions/match-sub-types.actions";
import {MatchSubTypeModel} from "../../../../models/match-sub-type.model";

@Component({
  selector: 'app-get-batting-records',
  templateUrl: './get-batting-records.component.html',
  styleUrls: ['./get-batting-records.component.css']
})
export class GetBattingRecordsComponent implements OnInit, OnDestroy {

  teams$!: Observable<Array<Team>>;
  countries$!: Observable<Array<Country>>;
  grounds$!: Observable<Array<Ground>>;
  seriesDates$!: Observable<Array<string>>;
  matchDates$!: Observable<Array<MatchDate>>;
  matchSubTypes$!: Observable<MatchSubTypeModel[]>;

  battingRecordsForm!: FormGroup;
  private matchDateSub!: Subscription;
  private defaultMatchType: string;
  private matchSubTypesSub!: Subscription;
  private matchTypeControlSub?: Subscription;
  private matchSubTypeControlSub?: Subscription;

  constructor(private route: ActivatedRoute,
              private fb: FormBuilder,
              private router: Router,
              private store: Store<AppState>,
              private settings: AppSettingsService) {

    this.defaultMatchType = settings.getDefaultMatchType()

    this.battingRecordsForm = this.fb.group({
      matchType: this.defaultMatchType,
      matchSubType: '',
      pageSize: '50',
      limit: 100,
      teamId: 0,
      opponentsId: 0,
      homeVenue: false,
      awayVenue: false,
      neutralVenue: false,
      hostCountryId: 0,
      groundId: 0,
      startDate: '',
      endDate: '',
      season: 0,
      matchWon: 0,
      matchLost: 0,
      matchDrawn: 0,
      matchTied: 0,
      format: 1,
    });

    this.teams$ = this.store.select(s => s.teams)
    this.countries$ = this.store.select(s => s.countries)
    this.grounds$ = this.store.select(s => s.grounds)
    this.seriesDates$ = this.store.select(s => s.seriesDates)
    this.matchDates$ = this.store.select(s => s.matchDates)
    this.matchSubTypes$ = this.store.select(s => s.matchSubTypes)
  }

  ngOnInit(): void {
    this.dispatchInitializationActions(this.defaultMatchType);
    this.store.dispatch(LoadMatchSubTypesAction({payload: this.defaultMatchType}))

    const matchTypeControl = this.battingRecordsForm.get('matchType')
    const matchSubTypeControl = this.battingRecordsForm.get('matchSubType')

    this.matchTypeControlSub = matchTypeControl?.valueChanges.subscribe(
      value => {
        this.dispatchInitializationActions(value);
        this.store.dispatch(LoadMatchSubTypesAction({payload: value}))
      }
    )

    this.matchSubTypeControlSub = matchSubTypeControl?.valueChanges.subscribe(
      value => {
        this.dispatchInitializationActions(value);
      }
    )

    this.matchDateSub = this.matchDates$.subscribe((s: Array<MatchDate>) => {
      this.battingRecordsForm.patchValue({'startDate': s[0]?.date, 'endDate': s[1]?.date})
    });

    this.matchSubTypesSub = this.matchSubTypes$.subscribe((s: Array<MatchSubTypeModel>) => {
      this.battingRecordsForm.patchValue({'matchSubType': s[0]?.key})
    });

  }

  private dispatchInitializationActions(matchStype: string) {
    this.store.dispatch(LoadTeamsAction({payload: matchStype}))
    this.store.dispatch(LoadCountriesAction({payload: matchStype}))
    this.store.dispatch(LoadGroundsAction({payload: matchStype}))
    this.store.dispatch(LoadSeriesDatesAction({payload: matchStype}))
    this.store.dispatch(LoadMatchDatesAction({payload: matchStype}))
  }

  ngOnDestroy() {
    this.matchDateSub.unsubscribe()
    this.matchSubTypesSub.unsubscribe()
    this.matchTypeControlSub?.unsubscribe();
    this.matchSubTypeControlSub?.unsubscribe();
  }

  public find() {

    // todo: validation?

    let route = ''
    switch (this.battingRecordsForm.get('format')?.value) {
      case 1:
        route = '/records/batting/overall'
        break;
      case 2:
        route = '/records/batting/inningsbyinnings'
        break;
      case 3:
        route = '/records/batting/matchtotals'
        break;
      case 4:
        route = '/records/batting/seriesaverages'
        break;
      case 5:
        route = '/records/batting/groundaverages'
        break;
      case 6:
        route = '/records/batting/bycountry'
        break;
      case 7:
        route = '/records/batting/byopposition'
        break;
      case 8:
        route = '/records/batting/byyearofmatchstart'
        break;
      case 9:
        route = '/records/batting/byseason'
        break;
    }

    let startDate = DateTime.fromISO(this.battingRecordsForm.get('startDate')?.value).toSeconds()
    let endDate = DateTime.fromISO(this.battingRecordsForm.get('endDate')?.value).toSeconds()

    let sortOrder = this.battingRecordsForm.get('sortOrder') ? this.battingRecordsForm.get('sortOrder')?.value : 4; // runs
    let sortDirection = this.battingRecordsForm.get('sortDirection') ? this.battingRecordsForm.get('sortDirection')?.value : "DESC";


    let queryParams: FindBatting = {
      matchType: this.battingRecordsForm.get('matchType')?.value
      , matchSubType: this.battingRecordsForm.get('matchSubType')?.value
      , teamId: this.battingRecordsForm.get('teamId')?.value
      , opponentsId: this.battingRecordsForm.get('opponentsId')?.value
      , groundId: this.battingRecordsForm.get('groundId')?.value
      , hostCountryId: this.battingRecordsForm.get('hostCountryId')?.value
      , homeVenue: this.battingRecordsForm.get('homeVenue')?.value
      , awayVenue: this.battingRecordsForm.get('awayVenue')?.value
      , neutralVenue: this.battingRecordsForm.get('neutralVenue')?.value
      , sortOrder
      , sortDirection
      , startDate: startDate.toString()
      , endDate: endDate.toString()
      , season: this.battingRecordsForm.get('season')?.value
      , matchWon: this.battingRecordsForm.get('matchWon')?.value
      , matchLost: this.battingRecordsForm.get('matchLost')?.value
      , matchDrawn: this.battingRecordsForm.get('matchDrawn')?.value
      , matchTied: this.battingRecordsForm.get('matchTied')?.value
      , limit: this.battingRecordsForm.get('limit')?.value
      , startRow: '0'
      , pageSize: this.battingRecordsForm.get('pageSize')?.value
    }

    this.router.navigate([route], {queryParams})

  }

}
