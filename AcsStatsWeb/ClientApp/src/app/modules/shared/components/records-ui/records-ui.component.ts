import {Component, Input, OnInit} from '@angular/core';
import {SaveRecordsFormAction} from "../../../../actions/form-state.actions";
import {LoadMatchSubTypesAction} from "../../../../actions/match-sub-types.actions";
import {LoadTeamsAction} from "../../../../actions/teams.actions";
import {LoadCountriesAction} from "../../../../actions/countries.actions";
import {LoadGroundsAction} from "../../../../actions/grounds.actions";
import {LoadMatchDatesAction, LoadSeriesDatesAction} from "../../../../actions/dates.actions";
import {Store} from "@ngrx/store";
import {AppState} from "../../../../models/app-state";
import {FormHelperService} from "../../../../services/form-helper.service";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {Observable, Subscription} from "rxjs";
import {Team} from "../../../../models/team.model";
import {Country} from "../../../../models/country.model";
import {Ground} from "../../../../models/ground.model";
import {MatchDate} from "../../../../models/date.model";
import {MatchSubTypeModel} from "../../../../models/match-sub-type.model";
import {FindRecords} from "../../../../models/find-records.model";
import {DateTime} from "luxon";
import {Router} from "@angular/router";

@Component({
  selector: 'app-records-ui',
  templateUrl: './records-ui.component.html',
  styleUrls: ['./records-ui.component.css']
})
export class RecordsUIComponent implements OnInit {
  recordsForm!: FormGroup;
  teams$!: Observable<Array<Team>>;
  countries$!: Observable<Array<Country>>;
  grounds$!: Observable<Array<Ground>>;
  seriesDates$!: Observable<Array<string>>;
  matchDates$!: Observable<Array<MatchDate>>;
  matchSubTypes$!: Observable<MatchSubTypeModel[]>;

  private matchDateSub!: Subscription;
  private defaultMatchType: string;
  private matchSubTypesSub!: Subscription;
  private matchTypeControlSub?: Subscription;
  private matchSubTypeControlSub?: Subscription;
  private formState$: Observable<FindRecords>;

  @Input()
  urlRoot!: string

  @Input()
  limitText!: string

  @Input()
  initialLimit!: number

  constructor(private formBuilder: FormBuilder,
              private router: Router,
              private store: Store<AppState>,
              private formHelperService: FormHelperService) {

    this.defaultMatchType = formHelperService.getDefaultMatchType()



    this.teams$ = this.store.select(s => s.teams)
    this.countries$ = this.store.select(s => s.countries)
    this.grounds$ = this.store.select(s => s.grounds)
    this.seriesDates$ = this.store.select(s => s.seriesDates)
    this.matchDates$ = this.store.select(s => s.matchDates)
    this.matchSubTypes$ = this.store.select(s => s.matchSubTypes)
    this.formState$ = this.store.select(s => s.formState)
  }

  get limit() { return this.recordsForm.get('limit'); }

  ngOnInit(): void {

    this.recordsForm = this.formBuilder.group({
      matchType: this.defaultMatchType,
      matchSubType: '',
      pageSize: '50',
      limit: new FormControl(this.initialLimit, Validators.required),
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

    this.dispatchInitializationActions(this.defaultMatchType);
    this.store.dispatch(LoadMatchSubTypesAction({payload: this.defaultMatchType}))

    const matchTypeControl = this.recordsForm.get('matchType')
    const matchSubTypeControl = this.recordsForm.get('matchSubType')

    this.matchTypeControlSub = matchTypeControl?.valueChanges.subscribe(
      value => {
        if (value != "" && value != undefined) {
          this.dispatchInitializationActions(value);
          this.store.dispatch(LoadMatchSubTypesAction({payload: value}))
        }
      }
    )

    this.matchSubTypeControlSub = matchSubTypeControl?.valueChanges.subscribe(
      value => {
        if(value != "" && value != undefined)
          this.dispatchInitializationActions(value);
      }
    )

    this.matchDateSub = this.matchDates$.subscribe((s: Array<MatchDate>) => {
      this.recordsForm.patchValue({'startDate': s[0]?.date, 'endDate': s[1]?.date})
    });

    this.matchSubTypesSub = this.matchSubTypes$.subscribe((s: Array<MatchSubTypeModel>) => {
      this.recordsForm.patchValue({'matchSubType': s[0]?.key})
    });

    this.formState$.subscribe((fs: FindRecords) => {
      if (fs.matchType != "") {
        this.recordsForm.patchValue(fs)
      }
    });

  }

  ngOnDestroy() {
    this.matchDateSub.unsubscribe()
    this.matchSubTypesSub.unsubscribe()
    this.matchTypeControlSub?.unsubscribe();
    this.matchSubTypeControlSub?.unsubscribe();
  }

  private dispatchInitializationActions(matchType: string) {
    this.store.dispatch(LoadTeamsAction({payload: matchType}))
    this.store.dispatch(LoadCountriesAction({payload: matchType}))
    this.store.dispatch(LoadGroundsAction({payload: matchType}))
    this.store.dispatch(LoadSeriesDatesAction({payload: matchType}))
    this.store.dispatch(LoadMatchDatesAction({payload: matchType}))
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

  find() {
    if(this.limit?.errors?.['required']){
      return
    }

    let route = ''
    switch (this.recordsForm.get('format')?.value) {
      case 1:
        route = `/records/${this.urlRoot}/overall`
        break;
      case 2:
        route = `/records/${this.urlRoot}/inningsbyinnings`
        break;
      case 3:
        route = `/records/${this.urlRoot}/matchtotals`
        break;
      case 4:
        route = `/records/${this.urlRoot}/seriesaverages`
        break;
      case 5:
        route = `/records/${this.urlRoot}/groundaverages`
        break;
      case 6:
        route = `/records/${this.urlRoot}/bycountry`
        break;
      case 7:
        route = `/records/${this.urlRoot}/byopposition`
        break;
      case 8:
        route = `/records/${this.urlRoot}/byyearofmatchstart`
        break;
      case 9:
        route = `/records/${this.urlRoot}/byseason`
        break;
    }

    let startDate = DateTime.fromISO(this.recordsForm.get('startDate')?.value).toSeconds()
    let endDate = DateTime.fromISO(this.recordsForm.get('endDate')?.value).toSeconds()

    let sortOrder = this.recordsForm.get('sortOrder') ? this.recordsForm.get('sortOrder')?.value : 4; // runs
    let sortDirection = this.recordsForm.get('sortDirection') ? this.recordsForm.get('sortDirection')?.value : "DESC";


    let queryParams: FindRecords = {
      matchType: this.recordsForm.get('matchType')?.value
      , matchSubType: this.recordsForm.get('matchSubType')?.value
      , teamId: this.recordsForm.get('teamId')?.value
      , opponentsId: this.recordsForm.get('opponentsId')?.value
      , groundId: this.recordsForm.get('groundId')?.value
      , hostCountryId: this.recordsForm.get('hostCountryId')?.value
      , homeVenue: this.recordsForm.get('homeVenue')?.value
      , awayVenue: this.recordsForm.get('awayVenue')?.value
      , neutralVenue: this.recordsForm.get('neutralVenue')?.value
      , sortOrder
      , sortDirection
      , startDate: startDate.toString()
      , endDate: endDate.toString()
      , season: this.recordsForm.get('season')?.value
      , matchWon: this.recordsForm.get('matchWon')?.value
      , matchLost: this.recordsForm.get('matchLost')?.value
      , matchDrawn: this.recordsForm.get('matchDrawn')?.value
      , matchTied: this.recordsForm.get('matchTied')?.value
      , limit: this.recordsForm.get('limit')?.value
      , startRow: '0'
      , pageSize: this.recordsForm.get('pageSize')?.value
    }

    this.store.dispatch(SaveRecordsFormAction({
      payload:
        this.recordsForm.getRawValue()
    }))

    this.router.navigate([route], {queryParams})
  }

  isNotFirstClass() {
    return this.formHelperService.isNotFirstClass(this.recordsForm.get('matchType')?.value)
  }

  isNotSeries() {
    let matchType = this.recordsForm.get('matchType')?.value;

    return this.formHelperService.isNotSeries(matchType)
  }
}
