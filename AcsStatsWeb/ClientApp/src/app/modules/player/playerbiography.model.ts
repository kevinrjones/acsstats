export class NameDetail {
  constructor(public fullName: string, public usedFrom: number) {
  }
}

export class PlayerBiography {
  constructor(public nameDetails: Array<NameDetail>) {
  }
}

export class PlayerOverall {
  constructor(
    public team: string, public matchType: string, public teamId: number, public matches: number,
    public runs: number, public innings: number, public notouts: number, public balls: number,
    public fours: number, public sixes: number, public hundreds: number, public fifties: number,
    public bowlingBalls: number, public bowlingRuns: number, public maidens: number, public wickets: number,
    public bowlingFours: number, public bowlingSixes: number, public wides: number, public noBalls: number
  ) {
  }
}

export class PlayerBattingDetails {
  constructor(public id: number, public matchType: string, public team: string, public opponents: string, public ground: string, public matchStartDate: string,
              public inningsNumber: number, public dismissal: string, public dismissalType: number, public fielderId: number, public fielderName: number, public bowlerId: number,
              public bowlerName: string, public score: number, public position: number, public notOut: number, public balls: number, public minutes: number, public fours: number, public sixes: number,
              public captain: number, public wicketKeeper: number) {
  }
}

export class PlayerBowlingDetails {
  constructor(public id: number, public matchType: string, public team: string, public opponents: string, public ground: string,
              public matchStartDate: string, public inningsNumber: number, public balls: number, public runs: number,
              public maidens: number, public wickets: number, public dots: number, public fours: number, public sixes: number,
              public wides: number, public noBalls: number, public captain: number, public ballsPerOver: number) {
  }
}

