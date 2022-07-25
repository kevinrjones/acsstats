import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {Envelope} from "../../../models/envelope";
import {BowlingCareerRecordDto} from "../models/bowling-overall.model";
import {IndividualBowlingDetailsDto} from "../models/individual-bowling-details.dto";
import {FindRecords} from "../../../models/find-records.model";
import {SqlResultsEnvelope} from "../../../models/sqlresultsenvelope.model";

@Injectable({providedIn: 'root'})
export class BowlingRecordService {

  constructor(private httpClient: HttpClient) {
  }

  private setBowlingParams(findRecords: FindRecords, matchResult: number) {
    return findRecords ?
      {

        params: new HttpParams()
          .set('matchSubType', findRecords.matchSubType)
          .set('groundId', findRecords.groundId)
          .set('hostCountryId', findRecords.hostCountryId)
          .set('homeVenue', findRecords.homeVenue)
          .set('awayVenue', findRecords.awayVenue)
          .set('neutralVenue', findRecords.neutralVenue)
          .set('limit', findRecords.limit)
          .set('matchResult', matchResult)
          .set('startDate', findRecords.startDate ?? 0)
          .set('endDate', findRecords.endDate ?? 999999999)
          .set('season', findRecords.season)
          .set('sortOrder', findRecords.sortOrder)
          .set('sortDirection', findRecords.sortDirection)
          .set('startRow', findRecords.startRow)
          .set('pageSize', findRecords.pageSize)
      } : {};

  }


  getBowlingOverall(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/overall/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingInningsByInnings(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<IndividualBowlingDetailsDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<IndividualBowlingDetailsDto[]>>>(`/api/bowlingrecords/inningsbyinnings/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByMatch(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<IndividualBowlingDetailsDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<IndividualBowlingDetailsDto[]>>>(`/api/bowlingrecords/match/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingBySeries(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/series/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByGround(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/grounds/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByHostCountry(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/host/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByOpposition(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/opposition/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingByYear(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/year/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }

  getBowlingBySeason(findBowling: FindRecords): Observable<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>> {

    let matchResult = findBowling ? findBowling.matchWon | findBowling.matchWon | findBowling.matchWon | findBowling.matchWon : 0;

    const options = this.setBowlingParams(findBowling, matchResult)

    return this.httpClient.get<Envelope<SqlResultsEnvelope<BowlingCareerRecordDto[]>>>(`/api/bowlingrecords/season/${findBowling.matchType}/${findBowling.teamId}/${findBowling.opponentsId}`, options)
  }
}
