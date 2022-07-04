import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {ReactiveFormsModule} from '@angular/forms';
import {RouterModule, Routes} from '@angular/router';
import {StoreModule} from '@ngrx/store';
import {EffectsModule} from '@ngrx/effects';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import {GetBowlingRecordsComponent} from "./components/get-bowling-records/get-bowling-records.component";
import {RecordEffects} from "./effects/record.effects";
import {SharedModule} from "../shared/shared.module";
import {BowlingOverallComponent} from "./components/bowling-overall/bowling-overall.component";
import {
  loadByGroundBowlingReducer,
  loadByHostBowlingReducer,
  loadByMatchBowlingReducer,
  loadByOppositionBowlingReducer,
  loadBySeasonBowlingReducer,
  loadBySeriesBowlingReducer,
  loadByYearBowlingReducer,
  loadInnByInnBowlingReducer,
  loadOverallBowlingReducer
} from "./reducers/record.reducer";
import {BowlingInnByInnComponent} from './components/bowling-inn-by-inn/bowling-inn-by-inn.component';
import {MatchTotalsComponent} from './components/match-totals/match-totals.component';
import {SeriesAveragesComponent} from './components/series-averages/series-averages.component';
import {GroundAveragesComponent} from './components/ground-averages/ground-averages.component';
import {ByOppositionComponent} from './components/by-opposition/by-opposition.component';
import {ByHostComponent} from './components/by-host/by-host.component';
import {ByYearOfMatchStartComponent} from './components/by-year-of-match-start/by-year-of-match-start.component';
import {BySeasonComponent} from './components/by-season/by-season.component';


const routes: Routes = [
  {path: 'records/bowling', component: GetBowlingRecordsComponent},
  {path: 'records/bowling/overall', component: BowlingOverallComponent},
  {path: 'records/bowling/inningsbyinnings', component: BowlingInnByInnComponent},
  {path: 'records/bowling/matchtotals', component: MatchTotalsComponent},
  {path: 'records/bowling/seriesaverages', component: SeriesAveragesComponent},
  {path: 'records/bowling/groundaverages', component: GroundAveragesComponent},
  {path: 'records/bowling/bycountry', component: ByHostComponent},
  {path: 'records/bowling/byopposition', component: ByOppositionComponent},
  {path: 'records/bowling/byyearofmatchstart', component: ByYearOfMatchStartComponent},
  {path: 'records/bowling/byseason', component: BySeasonComponent},
];


@NgModule({
  imports: [
    SharedModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature('bowlingrecords', {
      overall: loadOverallBowlingReducer,
      inningsByInnings: loadInnByInnBowlingReducer,
      byMatch: loadByMatchBowlingReducer,
      bySeries: loadBySeriesBowlingReducer,
      byGround: loadByGroundBowlingReducer,
      byHost: loadByHostBowlingReducer,
      byOpposition: loadByOppositionBowlingReducer,
      byYear: loadByYearBowlingReducer,
      bySeason: loadBySeasonBowlingReducer,
    }),
    EffectsModule.forFeature([RecordEffects]),
    FontAwesomeModule,
  ],
  exports: [RouterModule,
    GetBowlingRecordsComponent,
    BowlingOverallComponent
  ],
  declarations: [
    GetBowlingRecordsComponent,
    BowlingOverallComponent,
    BowlingInnByInnComponent,
    MatchTotalsComponent,
    SeriesAveragesComponent,
    GroundAveragesComponent,
    ByOppositionComponent,
    ByHostComponent,
    ByYearOfMatchStartComponent,
    BySeasonComponent,
  ],
})
export class BowlingRecordsModule {
}
