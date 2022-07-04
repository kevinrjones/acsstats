import {createAction, props} from '@ngrx/store'
import {Player} from '../models/player';
import {FindPlayer} from '../models/find-player.model';
import {
  PlayerBattingDetails,
  PlayerBiography,
  PlayerBowlingDetails,
  PlayerOverall
} from '../playerbiography.model';


const LOAD_PLAYERS = 'LOAD_PLAYERS;';
const LOAD_PLAYERS_SUCCESS = 'LOAD_PLAYERS_SUCCESS;';
const LOAD_PLAYERS_FAILURE = 'LOAD_PLAYERS_FAILURE;';

export const LoadPlayersAction = createAction(LOAD_PLAYERS, props<{ payload: FindPlayer }>())
export const LoadPlayersSuccessAction = createAction(LOAD_PLAYERS_SUCCESS, props<{ payload: Player[] }>())
export const LoadPlayersFailureAction = createAction(LOAD_PLAYERS_FAILURE)

const LOAD_PLAYER_BIOGRAPHY = 'LOAD_PLAYER;';
const LOAD_PLAYER_BIOGRAPHY_SUCCESS = 'LOAD_PLAYER_BIOGRAPHY_SUCCESS;';
const LOAD_PLAYER_BIOGRAPHY_FAILURE = 'LOAD_PLAYER_BIOGRAPHY_FAILURE;';

export const LoadPlayerBiographyAction = createAction(LOAD_PLAYER_BIOGRAPHY, props<{ payload: number }>())
export const LoadPlayerBiographySuccessAction = createAction(LOAD_PLAYER_BIOGRAPHY_SUCCESS, props<{ payload: PlayerBiography }>())
export const LoadPlayerBiographyFailureAction = createAction(LOAD_PLAYER_BIOGRAPHY_FAILURE)

const LOAD_PLAYER_OVERALL = 'LOAD_PLAYER_OVERAL;';
const LOAD_PLAYER_OVERALL_SUCCESS = 'LOAD_PLAYER_OVERALL_SUCCESS;';
const LOAD_PLAYER_OVERALL_FAILURE = 'LOAD_PLAYER_OVERALL_FAILURE;';

export const LoadPlayerOverallAction = createAction(LOAD_PLAYER_OVERALL, props<{ payload: number }>())
export const LoadPlayerOverallSuccessAction = createAction(LOAD_PLAYER_OVERALL_SUCCESS, props<{ payload: PlayerOverall[] }>())
export const LoadPlayerOverallFailureAction = createAction(LOAD_PLAYER_OVERALL_FAILURE)

const LOAD_PLAYER_BATTING_DETAILS = 'LOAD_PLAYER_BATTING_DETAILS;';
const LOAD_PLAYER_BATTING_DETAILS_SUCCESS = 'LOAD_PLAYER_BATTING_DETAILS_SUCCESS;';
const LOAD_PLAYER_BATTING_DETAILS_FAILURE = 'LOAD_PLAYER_BATTING_DETAILS_FAILURE;';

export const LoadPlayerBattingDetailsAction = createAction(LOAD_PLAYER_BATTING_DETAILS, props<{ payload: number }>())
export const LoadPlayerBattingDetailsSuccessAction = createAction(LOAD_PLAYER_BATTING_DETAILS_SUCCESS, props<{ payload: { [matchType: string]: PlayerBattingDetails[] } }>())
export const LoadPlayerBattingDetailsFailureAction = createAction(LOAD_PLAYER_BATTING_DETAILS_FAILURE)

const LOAD_PLAYER_BOWLING_DETAILS = 'LOAD_PLAYER_BOWLING_DETAILS;';
const LOAD_PLAYER_BOWLING_DETAILS_SUCCESS = 'LOAD_PLAYER_BOWLING_DETAILS_SUCCESS;';
const LOAD_PLAYER_BOWLING_DETAILS_FAILURE = 'LOAD_PLAYER_BOWLING_DETAILS_FAILURE;';

export const LoadPlayerBowlingDetailsAction = createAction(LOAD_PLAYER_BOWLING_DETAILS, props<{ payload: number }>())
export const LoadPlayerBowlingDetailsSuccessAction = createAction(LOAD_PLAYER_BOWLING_DETAILS_SUCCESS, props<{ payload: { [matchType: string]: PlayerBowlingDetails[] } }>())
export const LoadPlayerBowlingDetailsFailureAction = createAction(LOAD_PLAYER_BOWLING_DETAILS_FAILURE)
