import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ReactiveFormsModule} from '@angular/forms';
import {PlayerDetailsComponent} from './components/playerdetails/playerdetails.component';
import {PlayerSearchComponent} from './components/playerSearch/playersearch.component';
import {HttpClientModule} from '@angular/common/http';
import {StoreModule} from '@ngrx/store';
import {
  playerBattingOverallSuccessReducer,
  playerBiographySuccessReducer, playerBowlingOverallSuccessReducer,
  playerOverallSuccessReducer,
  playersSuccessReducer
} from './reducers/players.reducer';
import {EffectsModule} from '@ngrx/effects';
import {PlayerEffects} from './effects/players.effects';
import {BowlingDetailsComponent} from './components/playerdetails/bowling-details/bowling-details.component';
import {BattingDetailsComponent} from './components/playerdetails/batting-details/batting-details.component';
import {OverallDetailsComponent} from './components/playerdetails/overall-details/overall-details.component';
import {PlayerBiographyComponent} from './components/playerdetails/player-biography/player-biography.component';
import {GetPlayersComponent} from './components/getplayers/get-players.component';
import {PlayersListComponent} from './components/getplayers/players-list/players-list.component';


const routes: Routes = [
  {path: 'search/player/:path', component: PlayerSearchComponent},
  {path: 'playerlist', component: GetPlayersComponent},
  {path: 'player/:id', component: PlayerDetailsComponent},
];

@NgModule({
  imports: [
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    StoreModule.forFeature('players', {
      players: playersSuccessReducer
    }),
    StoreModule.forFeature('player', {
      playerBiography: playerBiographySuccessReducer,
      playerOverall: playerOverallSuccessReducer,
      playerBattingOverall: playerBattingOverallSuccessReducer,
      playerBowlingOverall: playerBowlingOverallSuccessReducer,
    }),
    EffectsModule.forFeature([PlayerEffects]),
  ],
  exports: [RouterModule, PlayerDetailsComponent, PlayerSearchComponent, GetPlayersComponent],
  declarations: [GetPlayersComponent, PlayerDetailsComponent, PlayerSearchComponent, BowlingDetailsComponent,
    BattingDetailsComponent, OverallDetailsComponent, PlayerBiographyComponent, PlayersListComponent]
})
export class PlayerModule {
}
