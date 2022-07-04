import {BowlingCareerRecordDto} from "./bowling-overall.model";
import {IndividualBowlingDetailsDto} from "./individual-bowling-details.dto";

export interface BowlingOverallUiModel {
  data: BowlingCareerRecordDto[],
  sortOrder: number,
  sortDirection: string
}

export interface InningsByInningsUiModel {
  data: IndividualBowlingDetailsDto[],
  sortOrder: number,
  sortDirection: string
}
