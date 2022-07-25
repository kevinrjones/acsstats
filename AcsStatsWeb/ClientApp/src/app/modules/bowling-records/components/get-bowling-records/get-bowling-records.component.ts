import { Component, OnInit } from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {Team} from "../../../../models/team.model";
import {Country} from "../../../../models/country.model";
import {Ground} from "../../../../models/ground.model";
import {MatchDate} from "../../../../models/date.model";
import {FormBuilder, FormGroup} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {AppState} from "../../../../models/app-state";
import {LoadTeamsAction} from "../../../../actions/teams.actions";
import {LoadCountriesAction} from "../../../../actions/countries.actions";
import {LoadGroundsAction} from "../../../../actions/grounds.actions";
import {LoadMatchDatesAction, LoadSeriesDatesAction} from "../../../../actions/dates.actions";
import {DateTime} from "luxon";
import {SortOrder} from "../../../../models/sortorder.model";
import {AppSettingsService} from "../../../../services/app-settings.service";
import {FindRecords} from "../../../../models/find-records.model";
import {MatchSubTypeModel} from "../../../../models/match-sub-type.model";
import {LoadMatchSubTypesAction} from "../../../../actions/match-sub-types.actions";
import {SaveRecordsFormAction} from "../../../../actions/form-state.actions";

@Component({
  selector: 'app-get-bowling-records',
  templateUrl: './get-bowling-records.component.html',
  styleUrls: ['./get-bowling-records.component.css']
})
export class GetBowlingRecordsComponent implements OnInit {

  teams$!: Observable<Array<Team>>;
  countries$!: Observable<Array<Country>>;
  grounds$!: Observable<Array<Ground>>;
  seriesDates$!: Observable<Array<string>>;
  matchDates$!: Observable<Array<MatchDate>>;
  matchSubTypes$!: Observable<MatchSubTypeModel[]>;

  bowlingRecordsForm!: FormGroup;
  private matchDateSub!: Subscription;
  private defaultMatchType: string;
  private matchSubTypesSub!: Subscription;
  private matchTypeControlSub?: Subscription;
  private matchSubTypeControlSub?: Subscription;
  private formState$: Observable<FindRecords>;

  constructor(private route: ActivatedRoute,
              private fb: FormBuilder,
              private router: Router,
              private store: Store<AppState>,
              private settings: AppSettingsService) {

    this.defaultMatchType = settings.getDefaultMatchType()

    this.bowlingRecordsForm = this.fb.group({
      matchType: this.defaultMatchType,
      matchSubType: '',
      pageSize: '50',
      limit: 1,
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
    this.formState$ = this.store.select(s => s.formState)

  }

  ngOnInit(): void {
    this.dispatchInitializationActions(this.defaultMatchType);
    this.store.dispatch(LoadMatchSubTypesAction({payload: this.defaultMatchType}))

    const matchTypeControl = this.bowlingRecordsForm.get('matchType')
    const matchSubTypeControl = this.bowlingRecordsForm.get('matchSubType')

    matchTypeControl?.valueChanges.subscribe(
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
      this.bowlingRecordsForm.patchValue({'startDate': s[0]?.date, 'endDate': s[1]?.date})
    });

    this.matchSubTypesSub = this.matchSubTypes$.subscribe((s: Array<MatchSubTypeModel>) => {
      this.bowlingRecordsForm.patchValue({'matchSubType': s[0]?.key})
    });

    this.formState$.subscribe((fs: FindRecords) => {
      if (fs.matchType != "") {
        this.bowlingRecordsForm.patchValue(fs)
      }
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
    switch (this.bowlingRecordsForm.get('format')?.value) {
      case 1:
        route = '/records/bowling/overall'
        break;
      case 2:
        route = '/records/bowling/inningsbyinnings'
        break;
      case 3:
        route = '/records/bowling/matchtotals'
        break;
      case 4:
        route = '/records/bowling/seriesaverages'
        break;
      case 5:
        route = '/records/bowling/groundaverages'
        break;
      case 6:
        route = '/records/bowling/bycountry'
        break;
      case 7:
        route = '/records/bowling/byopposition'
        break;
      case 8:
        route = '/records/bowling/byyearofmatchstart'
        break;
      case 9:
        route = '/records/bowling/byseason'
        break;
    }

    let startDate = DateTime.fromISO(this.bowlingRecordsForm.get('startDate')?.value).toSeconds()
    let endDate = DateTime.fromISO(this.bowlingRecordsForm.get('endDate')?.value).toSeconds()

    let sortOrder = this.bowlingRecordsForm.get('sortOrder') ? this.bowlingRecordsForm.get('sortOrder')?.value : SortOrder.wickets; // runs
    let sortDirection = this.bowlingRecordsForm.get('sortDirection') ? this.bowlingRecordsForm.get('sortDirection')?.value : "DESC";


    let queryParams: FindRecords = {
      matchType: this.bowlingRecordsForm.get('matchType')?.value
      , matchSubType: this.bowlingRecordsForm.get('matchSubType')?.value
      , teamId: this.bowlingRecordsForm.get('teamId')?.value
      , opponentsId: this.bowlingRecordsForm.get('opponentsId')?.value
      , groundId: this.bowlingRecordsForm.get('groundId')?.value
      , hostCountryId: this.bowlingRecordsForm.get('hostCountryId')?.value
      , homeVenue: this.bowlingRecordsForm.get('homeVenue')?.value
      , awayVenue: this.bowlingRecordsForm.get('awayVenue')?.value
      , neutralVenue: this.bowlingRecordsForm.get('neutralVenue')?.value
      , sortOrder
      , sortDirection
      , startDate: startDate.toString()
      , endDate: endDate.toString()
      , season: this.bowlingRecordsForm.get('season')?.value
      , matchWon: this.bowlingRecordsForm.get('matchWon')?.value
      , matchLost: this.bowlingRecordsForm.get('matchLost')?.value
      , matchDrawn: this.bowlingRecordsForm.get('matchDrawn')?.value
      , matchTied: this.bowlingRecordsForm.get('matchTied')?.value
      , limit: this.bowlingRecordsForm.get('limit')?.value
      , startRow: '0'
      , pageSize: this.bowlingRecordsForm.get('pageSize')?.value
    }

    this.store.dispatch(SaveRecordsFormAction({
      payload:
        this.bowlingRecordsForm.getRawValue()
    }))

    this.router.navigate([route], {queryParams})

  }

  public reset() {
    this.store.dispatch(SaveRecordsFormAction({
      payload: {
        matchType: this.defaultMatchType,
        matchSubType: '',
        teamId: 0,
        opponentsId: 0,
        groundId: 0,
        hostCountryId: 0,
        homeVenue: '',
        awayVenue: '',
        neutralVenue: '',
        startDate: '',
        endDate: '',
        season: '0',
        matchWon: 0,
        matchLost: 0,
        matchDrawn: 0,
        matchTied: 0,
        limit: 100,
        startRow: '0',
        pageSize: '50',
        format: 1
      }
    }))
    this.dispatchInitializationActions(this.defaultMatchType);
    this.store.dispatch(LoadMatchSubTypesAction({payload: ""}))
    this.store.dispatch(LoadMatchSubTypesAction({payload: this.defaultMatchType}))
  }

}
