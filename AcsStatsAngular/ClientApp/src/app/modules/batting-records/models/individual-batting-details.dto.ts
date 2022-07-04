export interface IndividualBattingDetailsDto {
  fullName: string
  sortNamePart: string
  team: string
  opponents: string
  inningsNumber: number
  ground: string
  matchDate: string
  playerScore: number
  bat1: number
  bat2: number
  notOut: boolean
  position: number
  balls: number
  fours: number
  sixes: number
  minutes: number
}
