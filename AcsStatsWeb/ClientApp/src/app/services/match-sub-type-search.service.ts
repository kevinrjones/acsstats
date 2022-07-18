import {Injectable} from "@angular/core";
import {MatchSubTypeModel} from "../models/match-sub-type.model";
import {Observable} from "rxjs";
import {of} from "rxjs";

@Injectable({providedIn: 'root'})
export class MatchSubTypeSearchService {

  matchSubTypes = new Map<string, MatchSubTypeModel[]>()

  constructor() {
    this.matchSubTypes.set("itt", [
      {key: "itt", name: "All"},
      {key: "wctt", name: "T20 World Cup"},
    ]);
    this.matchSubTypes.set("witt", [
      {key: "witt", name: "All"},
      {key: "wwctt", name: "Women's T20 World Cup"},
    ]);
    this.matchSubTypes.set("tt", [
      {key: "tt", name: "All"},
      {key: "bbl", name: "Big Bash League"},
      {key: "bpl", name: "Bangladeshi Premier League"},
      {key: "cpl", name: "Caribbean Premier League"},
      {key: "engtt", name: "England Domestic T20"},
      {key: "hund", name: "The Hundred"},
      {key: "ipl", name: "Indian Premier League"},
      {key: "lpl", name: "Lanka Premier League"},
      {key: "msl", name: "Mzansi Super League"},
      {key: "nztt", name: "New Zealand Domestic T20"},
      {key: "psl", name: "Pakistani Super League"},
      {key: "wctt", name: "T20 World Cup"},
    ]);
    this.matchSubTypes.set("wtt", [
      {key: "wtt", name: "All"},
      {key: "kia", name: "Kia Super League"},
      {key: "wbbl", name: "Women's Big Bash League"},
      {key: "whund", name: "Women's Hundred"},
      {key: "wwctt", name: "Wommen's T20 World Cup"},
    ]);
  }

  getMatchSubTypesForMatchType(matchType: string): Observable<MatchSubTypeModel[]> {
    // @ts-ignore
    return of(this.matchSubTypes.has(matchType) ? this.matchSubTypes.get(matchType) : [])
  }
}
