import {BattingCareerRecordDto} from "./batting-overall.model";
import {IndividualBattingDetailsDto} from "./individual-batting-details.dto";

export interface BattingOverallUiModel {
  data: BattingCareerRecordDto[],
  sortOrder: number,
  sortDirection: string
}

export interface InningsByInningsUiModel {
  data: IndividualBattingDetailsDto[],
  sortOrder: number,
  sortDirection: string
}

