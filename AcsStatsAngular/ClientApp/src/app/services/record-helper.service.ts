import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { Envelope } from 'src/app/models/envelope';
import {Team} from "../models/team.model";
import {MatchDate} from "../models/date.model";
import {RecordsSummaryModel} from "../models/records-summary.model";

@Injectable({providedIn: 'root'})
export class RecordHelperService {

  constructor(private httpClient: HttpClient) {
  }

  getSummary(matchType:string, teamId: number, opponentsId: number, groundId: number, hostCouuntryId: number): Observable<Envelope<RecordsSummaryModel>> {
    return this.httpClient.get<Envelope<RecordsSummaryModel>>(`/api/records/summary/${matchType}/${teamId}/${opponentsId}/${groundId}/${hostCouuntryId}`)
  }

}
