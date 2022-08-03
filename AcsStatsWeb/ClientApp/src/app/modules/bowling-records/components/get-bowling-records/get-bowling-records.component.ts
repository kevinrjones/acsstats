import { Component, OnInit } from '@angular/core';
import {Observable, Subscription} from "rxjs";
import {Team} from "../../../../models/team.model";
import {Country} from "../../../../models/country.model";
import {Ground} from "../../../../models/ground.model";
import {MatchDate} from "../../../../models/date.model";
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Store} from "@ngrx/store";
import {AppState} from "../../../../models/app-state";
import {LoadTeamsAction} from "../../../../actions/teams.actions";
import {LoadCountriesAction} from "../../../../actions/countries.actions";
import {LoadGroundsAction} from "../../../../actions/grounds.actions";
import {LoadMatchDatesAction, LoadSeriesDatesAction} from "../../../../actions/dates.actions";
import {DateTime} from "luxon";
import {SortOrder} from "../../../../models/sortorder.model";
import {FormHelperService} from "../../../../services/form-helper.service";
import {FindRecords} from "../../../../models/find-records.model";
import {MatchSubTypeModel} from "../../../../models/match-sub-type.model";
import {LoadMatchSubTypesAction} from "../../../../actions/match-sub-types.actions";
import {SaveRecordsFormAction} from "../../../../actions/form-state.actions";

@Component({
  selector: 'app-get-bowling-records',
  templateUrl: './get-bowling-records.component.html',
  styleUrls: ['./get-bowling-records.component.css']
})
export class GetBowlingRecordsComponent  {
}
