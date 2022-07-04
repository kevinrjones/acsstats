import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Envelope} from 'src/app/models/envelope';
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {FindBatting} from "../models/find-batting-overall.model";
import {IndividualBattingDetailsDto} from "../models/individual-batting-details.dto";

@Injectable({providedIn: 'root'})
export class BattingRecordService {

  constructor(private httpClient: HttpClient) {
  }

  getBattingOverall(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/overall/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingInningsByInnings(findBatting: FindBatting): Observable<Envelope<IndividualBattingDetailsDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<IndividualBattingDetailsDto[]>>(`/api/battingrecords/inningsbyinnings/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingByMatch(findBatting: FindBatting): Observable<Envelope<IndividualBattingDetailsDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<IndividualBattingDetailsDto[]>>(`/api/battingrecords/match/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingBySeries(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/series/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingByGround(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/grounds/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingByHostCountry(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/host/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingByOpposition(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/opposition/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingByYear(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('sortDirection', findBatting.sortDirection)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/year/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

  getBattingBySeason(findBatting: FindBatting): Observable<Envelope<BattingCareerRecordDto[]>> {

    let matchResult = findBatting ? findBatting.matchWon | findBatting.matchWon | findBatting.matchWon | findBatting.matchWon : 0;

    const options = findBatting ?
      {

        params: new HttpParams()
          .set('groundId', findBatting.groundId)
          .set('hostCountryId', findBatting.hostCountryId)
          .set('homeVenue', findBatting.homeVenue)
          .set('awayVenue', findBatting.awayVenue)
          .set('neutralVenue', findBatting.neutralVenue)
          .set('limit', findBatting.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBatting.startDate ?? 0)
          .set('endDate', findBatting.endDate ?? 999999999)
          .set('season', findBatting.season)
          .set('sortOrder', findBatting.sortOrder)
          .set('startRow', findBatting.startRow)
          .set('pageSize', findBatting.pageSize)
          .set('sortDirection', findBatting.sortDirection)
      } : {};

    return this.httpClient.get<Envelope<BattingCareerRecordDto[]>>(`/api/battingrecords/season/${findBatting.matchType}/${findBatting.teamId}/${findBatting.opponentsId}`, options)
  }

}