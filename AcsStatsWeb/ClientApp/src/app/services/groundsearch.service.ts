import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { Envelope } from 'src/app/models/envelope';
import {Team} from "../models/team.model";
import { Ground } from '../models/ground.model';

@Injectable({providedIn: 'root'})
export class GroundSearchService {

  constructor(private httpClient: HttpClient) {
  }

  findGroundsForMatchType(matchType: string): Observable<Envelope<Ground[]>> {
    return this.httpClient.get<Envelope<Ground[]>>(`/api/grounds/${matchType}`)
  }


}
