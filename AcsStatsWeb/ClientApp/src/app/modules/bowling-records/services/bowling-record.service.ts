import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Envelope} from "../../../models/envelope";
import {BowlingCareerRecordDto} from "../models/bowling-overall.model";
import {IndividualBowlingDetailsDto} from "../models/individual-bowling-details.dto";
import {FindRecords} from "../../../models/find-records.model";

@Injectable({providedIn: 'root'})
export class BowlingRecordService {

  constructor(private httpClient: HttpClient) {
  }

  getBowlingOverall(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/overall/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingInningsByInnings(findBowling: FindRecords): Observable<Envelope<IndividualBowlingDetailsDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<IndividualBowlingDetailsDto[]>>(`/api/bowlingrecords/inningsbyinnings/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByMatch(findBowling: FindRecords): Observable<Envelope<IndividualBowlingDetailsDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<IndividualBowlingDetailsDto[]>>(`/api/bowlingrecords/match/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingBySeries(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/series/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByGround(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/grounds/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByHostCountry(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/host/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByOpposition(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/opposition/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByYear(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/year/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingBySeason(findBowling: FindRecords): Observable<Envelope<BowlingCareerRecordDto[]>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = findBowling ?
      {

        params: new HttpParams()
          .set('groundId', findBowling.groundId)
          .set('hostCountryId', findBowling.hostCountryId)
          .set('homeVenue', findBowling.homeVenue)
          .set('awayVenue', findBowling.awayVenue)
          .set('neutralVenue', findBowling.neutralVenue)
          .set('limit', findBowling.limit)
          .set('matchResult', matchResult)
          .set('startDate', findBowling.startDate ?? 0)
          .set('endDate', findBowling.endDate ?? 999999999)
          .set('season', findBowling.season)
          .set('sortOrder', findBowling.sortOrder)
          .set('sortDirection', findBowling.sortDirection)
          .set('startRow', findBowling.startRow)
          .set('pageSize', findBowling.pageSize)
      } : {};

    return this.httpClient.get<Envelope<BowlingCareerRecordDto[]>>(`/api/bowlingrecords/season/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }
}
