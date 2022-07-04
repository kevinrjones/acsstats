export interface FindBatting {
  matchType: string;
  teamId: number;
  opponentsId: number;
  groundId: number;
  hostCountryId: number;
  homeVenue: string;
  awayVenue: string;
  neutralVenue: string;
  sortOrder: number;
  sortDirection: string;
  startDate: string;
  endDate: string;
  season: string;
  matchWon: number;
  matchLost: number;
  matchDrawn: number;
  matchTied: number;
  limit: number;
  startRow: number;
  pageSize: number;
}

