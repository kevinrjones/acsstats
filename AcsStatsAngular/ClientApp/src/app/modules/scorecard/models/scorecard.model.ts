export class Scorecard {
  constructor(public notes: string[], public debuts: Debut[],  public header: ScorecardHeader, public innings: Inning[]) {
  }
}

export interface Debut {
  playerId: number,
  fullName: string,
  teamId: number,
  teamName: string
}

export interface Person {
  key: number,
  name: string
}

export interface Location {
  key: number,
  name: string
}

export interface CloseOfPlay {
  day: number,
  note: string
}

export interface Dismissal {
  dismissalType: number,
  dismissal: string,
  bowler: Person,
  fielder: Person
}

export enum VictoryType {
  Awarded = 0,
  Drawn,
  Runs,
  Wickets,
  Innings,
  Tied,
  Abandoned,
  NoResult,
  RunRate,
  LosingFewerWickets,
  FasterScoringRate,
  Unknown
}

export interface ScorecardTeam {
  key: number,
  name: string
}

export interface Result {
  whoWon: ScorecardTeam,
  whoLost: ScorecardTeam,
  resultString: ScorecardTeam,
  victoryType: VictoryType
}

export interface FallOfWicket {
  wicket: number,
  score: number,
  player: Person,
  overs: string
}


export interface Inning {
  team: ScorecardTeam,
  opponents: ScorecardTeam,
  inningsNumber: number,
  inningsOrder: number,
  total: Total,
  extras: Extras,
  battingLines: BattlingLine[],
  bowlingLines: BowlingLine[],
  fallOfWickets: FallOfWicket[]
}


export interface Total {
  wickets : number,
  declared: boolean,
  overs: string,
  minutes: number,
  total: number
}

export interface Extras {
  byes: number,
  legByes: number,
  wides: number,
  noBalls: number,
  pens: number,
  total: number
}

export interface BattlingLine {
  player: Person,
  runs: number,
  balls: number,
  fours: number,
  sixes: number,
  notOut: boolean,
  minutes: number,
  position: number,
  dismissal: Dismissal,
  isCaptain: boolean,
  isWicketKeeper: boolean
}


export interface BowlingLine {
  dots :number,
  runs:number,
  balls:number,
  fours:number,
  sixes:number,
  overs: string,
  wides:number,
  player: Person,
  maidens:number,
  noBalls:number,
  wickets:number,
  isCaptain:boolean
}

export interface ScorecardHeader {
  toss: ScorecardTeam,
  where: Location,
  result: Result,
  scorers: Person[],
  umpires: Person[],
  awayTeam: ScorecardTeam,
  awayTeamScores: string[],
  dayNight: boolean,
  homeTeam: ScorecardTeam,
  homeTeamScores: string[],
  tvUmpires:Person[],
  matchDate: string,
  matchType: string,
  matchTitle: string,
  seriesDate: string,
  closeOfPlay: CloseOfPlay[],
  ballsPerOver: number,
  matchReferee: Person[],
  matchDesignator: string
}
