import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import { Envelope } from 'src/app/models/envelope';
import {PlayerBattingDetails, PlayerBiography, PlayerBowlingDetails, PlayerOverall} from '../playerbiography.model';
import {Player} from '../models/player';
import {FindPlayer} from '../models/find-player.model';

@Injectable({providedIn: 'root'})
export class PlayerSearchService {

  constructor(private httpClient: HttpClient) {
  }

  findPlayers(findPlayer: FindPlayer): Observable<Envelope<Player[]>> {

    const options = findPlayer ?
      {params: new HttpParams().set('name', findPlayer.name)
          .set('team', findPlayer.team)
          .set('exactNameMatch', findPlayer.exactMatch)
          .set('debutDate', findPlayer.startDate)
          .set('activeUntil', findPlayer.endDate)
      } : {};

    return this.httpClient.get<Envelope<Player[]>>(`/api/player/findplayers`, options)
  }

  getPlayerBiography(id: number) : Observable<Envelope<PlayerBiography>>{
    return this.httpClient.get<Envelope<PlayerBiography>>(`/api/player/biography/${id}`)
  }

  getPlayerOverall(id: number) : Observable<Envelope<PlayerOverall[]>>{
    return this.httpClient.get<Envelope<PlayerOverall[]>>(`/api/player/overall/${id}`)
  }

  getPlayerBattingOverall(id: number) : Observable<Envelope<{ [matchType: string]: PlayerBattingDetails[] }>>{
    return this.httpClient.get<Envelope<{ [matchType: string]: PlayerBattingDetails[] }>>(`/api/player/battingdetails/${id}`)
  }

  getPlayerBowlingOverall(id: number) : Observable<Envelope<{ [matchType: string]: PlayerBowlingDetails[] }>>{
    return this.httpClient.get<Envelope<{ [matchType: string]: PlayerBowlingDetails[] }>>(`/api/player/bowlingdetails/${id}`)
  }
}
