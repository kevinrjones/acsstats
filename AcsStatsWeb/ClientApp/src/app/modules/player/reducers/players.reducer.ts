import {Player} from '../models/player';
import {createReducer, on} from '@ngrx/store';
import {
  LoadPlayerBattingDetailsSuccessAction,
  LoadPlayerBiographySuccessAction, LoadPlayerBowlingDetailsSuccessAction,
  LoadPlayerOverallSuccessAction,
  LoadPlayersSuccessAction
} from '../actions/players.actions';
import {
  NameDetail,
  PlayerBattingDetails,
  PlayerBiography,
  PlayerBowlingDetails,
  PlayerOverall
} from '../playerbiography.model';


export const initialPlayersState: ReadonlyArray<Player> = [];
export const playersSuccessReducer = createReducer(
  initialPlayersState,
  on(LoadPlayersSuccessAction, (state, players) => {
    return players.payload
  })
);

export const initialPlayerBiographyState = new PlayerBiography(new Array<NameDetail>());

export const playerBiographySuccessReducer = createReducer(
  initialPlayerBiographyState,
  on(LoadPlayerBiographySuccessAction, (state, players) => {
    return players.payload
  })
);

export const initialPlayerOverallState: ReadonlyArray<PlayerOverall> = [];

export const playerOverallSuccessReducer = createReducer(
  initialPlayerOverallState,
  on(LoadPlayerOverallSuccessAction, (state, players) => {
    return players.payload
  })
);

export const initialPlayerBattingOverallState: { [matchType: string]: PlayerBattingDetails[] } = {};

export const playerBattingOverallSuccessReducer = createReducer(
  initialPlayerBattingOverallState,
  on(LoadPlayerBattingDetailsSuccessAction, (state, players) => {
    return players.payload
  })
);

export const initialPlayerBowlingOverallState: { [matchType: string]: PlayerBowlingDetails[] } = {};

export const playerBowlingOverallSuccessReducer = createReducer(
  initialPlayerBowlingOverallState,
  on(LoadPlayerBowlingDetailsSuccessAction, (state, players) => {
    return players.payload
  })
);
