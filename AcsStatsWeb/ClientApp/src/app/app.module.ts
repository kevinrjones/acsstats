import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {StoreModule} from '@ngrx/store';
import {EffectsModule} from '@ngrx/effects';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';

import {AppComponent} from './app.component';
import {HomeComponent} from './components/home/home.component';
import {environment} from '../environments/environment';
import {PageNotFoundComponent} from './components/pagenotfound/pagenotfound.component';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {countryReducer} from './reducers/countries.reducer';
import {PlayerModule} from './modules/player/player.module';
import {ScorecardModule} from './modules/scorecard/scorecard-module';
import {BattingRecordsModule} from './modules/batting-records/batting-records.module';
import {teamReducer} from "./reducers/teams.reducer";
import {TeamEffects} from "./effects/team.effects";
import {CountryEffects} from "./effects/country.effects";
import {GroundEffects} from "./effects/ground.effects";
import {groundsReducer} from "./reducers/grounds.reducer";
import {matchDatesReducer, seriesDatesReducer} from "./reducers/dates.reducer";
import {DateEffects} from "./effects/date.effects";
import {recordSummaryReducer} from "./reducers/recordsummary.reducer";
import {RecordSummaryEffects} from "./effects/recordsummary.effects";
import {FontAwesomeModule} from '@fortawesome/angular-fontawesome';
import {BowlingRecordsModule} from "./modules/bowling-records/bowling-records.module";
import {HeaderComponent} from "./components/header/header.component";
import {MatchSubTypeEffects} from "./effects/match-sub-type.effects";
import {matchSubTypeReducer} from "./reducers/match-sub-type.reducer";
import {loadSearchFormStateReducer} from "./reducers/form-state.reducer";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PageNotFoundComponent,
    HeaderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: '**', component: PageNotFoundComponent, pathMatch: 'full'},
    ]),
    StoreModule.forRoot({
      countries: countryReducer,
      teams: teamReducer,
      grounds: groundsReducer,
      seriesDates: seriesDatesReducer,
      matchDates: matchDatesReducer,
      matchSubTypes: matchSubTypeReducer,
      playerRecordSummary: recordSummaryReducer,
      formState: loadSearchFormStateReducer
    }, {}),
    EffectsModule.forRoot([TeamEffects, CountryEffects, GroundEffects, DateEffects, RecordSummaryEffects, MatchSubTypeEffects]),
    StoreDevtoolsModule.instrument({maxAge: 25, logOnly: environment.production}),
    FontAwesomeModule,
    PlayerModule,
    ScorecardModule,
    BattingRecordsModule,
    BowlingRecordsModule,
  ],
  providers: [],
  exports: [

  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
