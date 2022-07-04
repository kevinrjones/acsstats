import {createReducer, on} from '@ngrx/store';
import {BattingCareerRecordDto} from "../models/batting-overall.model";
import {
  LoadByMatchBattingRecordsSuccessAction,
  LoadInnByInnBattingRecordsSuccessAction,
  LoadOverallBattingRecordsSuccessAction,
  SortBattingRecordsInningsByInningsAction,
  SortBattingRecordsByMatchAction,
  SortBattingRecordsOverallAction,
  LoadBySeriesBattingRecordsSuccessAction,
  SortBattingRecordsBySeriesAction,
  LoadByGroundBattingRecordsSuccessAction,
  SortBattingRecordsByGroundAction,
  LoadByYearBattingRecordsSuccessAction,
  SortBattingRecordsByHostAction,
  LoadByHostBattingRecordsSuccessAction,
  LoadByOppositionBattingRecordsSuccessAction,
  SortBattingRecordsByOppositionAction,
  SortBattingRecordsByYearAction,
  LoadBySeasonBattingRecordsSuccessAction,
  SortBattingRecordsBySeasonAction
} from "../actions/records.actions";
import {IndividualBattingDetailsDto} from "../models/individual-batting-details.dto";
import {DateTime} from "luxon";
import { SortOrder } from 'src/app/models/sortorder.model';


export const initialBattingOverallRecordState = {
  data: Array<BattingCareerRecordDto>(),
  sortOrder: 4,
  sortDirection: "desc"
};
export const loadOverallBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadOverallBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsOverallAction, (state, records) => {
    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);

export const initialBattingInnByInnRecordState = {
  data: Array<IndividualBattingDetailsDto>(),
  sortOrder: 4,
  sortDirection: "desc"
};
export const loadInnByInnBattingReducer = createReducer(
  initialBattingInnByInnRecordState,
  on(LoadInnByInnBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsInningsByInningsAction, (state, records) => {
    let sortedData = state.data.slice().sort(sort)

    function sort(a: IndividualBattingDetailsDto, b: IndividualBattingDetailsDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.runs:
          if (a.playerScore < b.playerScore) res = -1
          if (a.playerScore > b.playerScore) res = 1
          if (a.playerScore == b.playerScore) {
            if (a.notOut) return 1
            else if (b.notOut) return -1
          }
          break;
        case SortOrder.minutes:
          if (a.minutes < b.minutes) res = -1
          if (a.minutes > b.minutes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.ground:
          if (a.ground < b.ground) res = -1
          if (a.ground > b.ground) res = 1
          break;
        case SortOrder.matchStartDate:
          let matchDateA = DateTime.fromFormat(a.matchDate, "d LLLL yyyy")
          let matchDateB = DateTime.fromFormat(b.matchDate, "d LLLL yyyy")
          if (matchDateA < matchDateB) res = -1
          if (matchDateA > matchDateB) res = 1
          break;
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
)

export const loadByMatchBattingReducer = createReducer(
  initialBattingInnByInnRecordState,
  on(LoadByMatchBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsByMatchAction, (state, records) => {
    let sortedData = state.data.slice().sort(sort)

    function sort(a: IndividualBattingDetailsDto, b: IndividualBattingDetailsDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.runs:
          if (a.playerScore < b.playerScore) res = -1
          if (a.playerScore > b.playerScore) res = 1
          if (a.playerScore == b.playerScore) {
            if (a.notOut) return 1
            else if (b.notOut) return -1
          }
          break;
        case SortOrder.minutes:
          if (a.minutes < b.minutes) res = -1
          if (a.minutes > b.minutes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.ground:
          if (a.ground < b.ground) res = -1
          if (a.ground > b.ground) res = 1
          break;
        case SortOrder.matchStartDate:
          let matchDateA = DateTime.fromFormat(a.matchDate, "d LLLL yyyy")
          let matchDateB = DateTime.fromFormat(b.matchDate, "d LLLL yyyy")
          if (matchDateA < matchDateB) res = -1
          if (matchDateA > matchDateB) res = 1
          break;
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
)

export const loadBySeriesBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeriesBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsBySeriesAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);

export const loadByGroundBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByGroundBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsByGroundAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);


export const loadByHostBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByHostBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsByHostAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);

export const loadByOppositionBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByOppositionBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsByOppositionAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);

export const loadByYearBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadByYearBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsByYearAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.matchStartYear:
          if (a.year < b.year) res = -1
          if (a.year > b.year) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);

export const loadBySeasonBattingReducer = createReducer(
  initialBattingOverallRecordState,
  on(LoadBySeasonBattingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBattingRecordsBySeasonAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BattingCareerRecordDto, b: BattingCareerRecordDto) {
      let res = 0
      switch (records.payload.sortOrder) {
        case SortOrder.name:
          if (a.sortNamePart.toUpperCase() < b.sortNamePart.toUpperCase()) res = -1
          if (a.sortNamePart.toUpperCase() > b.sortNamePart.toUpperCase()) res = 1
          break;
        case SortOrder.matches:
          if (a.matches < b.matches) res = -1
          if (a.matches > b.matches) res = 1
          break;
        case SortOrder.team:
          if (a.team < b.team) res = -1
          if (a.team > b.team) res = 1
          break;
        case SortOrder.opponents:
          if (a.opponents < b.opponents) res = -1
          if (a.opponents > b.opponents) res = 1
          break;
        case SortOrder.innings:
          if (a.innings < b.innings) res = -1
          if (a.innings > b.innings) res = 1
          break;
        case SortOrder.notOuts:
          if (a.notOuts < b.notOuts) res = -1
          if (a.notOuts > b.notOuts) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.hs:
          if (a.highestScore < b.highestScore) res = -1
          if (a.highestScore > b.highestScore) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.hundreds:
          if (a.hundreds < b.hundreds) res = -1
          if (a.hundreds > b.hundreds) res = 1
          break;
        case SortOrder.fifties:
          if (a.fifties < b.fifties) res = -1
          if (a.fifties > b.fifties) res = 1
          break;
        case SortOrder.ducks:
          if (a.ducks < b.ducks) res = -1
          if (a.ducks > b.ducks) res = 1
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.matchStartYear:
          if (a.year < b.year) res = -1
          if (a.year > b.year) res = 1
          break;
        case SortOrder.strikeRate: {
          let aballs = (a.balls == 0) ? 99999 : a.balls
          let bballs = (b.balls == 0) ? 99999 : b.balls
          if (a.runs / aballs < b.runs / bballs) res = -1
          if (a.runs / aballs > b.runs / bballs) res = 1
          break;
        }
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
);
