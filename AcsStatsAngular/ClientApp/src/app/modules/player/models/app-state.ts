import {Player} from './player';
import {
  PlayerBattingDetails,
  PlayerBiography,
  PlayerBowlingDetails,
  PlayerOverall
} from '../playerbiography.model';


export interface PlayerState {
  players: { players: Player[] },
  player: {
    playerBiography: PlayerBiography,
    playerOverall: PlayerOverall[],
    playerBattingOverall: { [matchType: string]: PlayerBattingDetails[] },
    playerBowlingOverall: { [matchType: string]: PlayerBowlingDetails[] }
  },
}

