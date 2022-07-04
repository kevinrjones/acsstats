import {createReducer, on} from '@ngrx/store';
import {BowlingCareerRecordDto} from "../models/bowling-overall.model";
import {
  LoadByMatchBowlingRecordsSuccessAction,
  LoadInnByInnBowlingRecordsSuccessAction,
  LoadOverallBowlingRecordsSuccessAction,
  SortBowlingRecordsInningsByInningsAction,
  SortBowlingRecordsByMatchAction,
  SortBowlingRecordsOverallAction,
  LoadBySeriesBowlingRecordsSuccessAction,
  SortBowlingRecordsBySeriesAction,
  LoadByGroundBowlingRecordsSuccessAction,
  SortBowlingRecordsByGroundAction,
  LoadByYearBowlingRecordsSuccessAction,
  SortBowlingRecordsByHostAction,
  LoadByHostBowlingRecordsSuccessAction,
  LoadByOppositionBowlingRecordsSuccessAction,
  SortBowlingRecordsByOppositionAction,
  SortBowlingRecordsByYearAction,
  LoadBySeasonBowlingRecordsSuccessAction,
  SortBowlingRecordsBySeasonAction
} from "../actions/records.actions";
import {IndividualBowlingDetailsDto} from "../models/individual-bowling-details.dto";
import {DateTime} from "luxon";
import { SortOrder } from 'src/app/models/sortorder.model';


export const initialBowlingOverallRecordState = {
  data: Array<BowlingCareerRecordDto>(),
  sortOrder: SortOrder.wickets,
  sortDirection: "desc"
};
export const loadOverallBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadOverallBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsOverallAction, (state, records) => {
    console.log("sort reducer")
    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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

export const initialBowlingInnByInnRecordState = {
  data: Array<IndividualBowlingDetailsDto>(),
  sortOrder: SortOrder.wickets,
  sortDirection: "desc"
};
export const loadInnByInnBowlingReducer = createReducer(
  initialBowlingInnByInnRecordState,
  on(LoadInnByInnBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsInningsByInningsAction, (state, records) => {
    let sortedData = state.data.slice().sort(sort)

    function sort(a: IndividualBowlingDetailsDto, b: IndividualBowlingDetailsDto) {
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
        case SortOrder.balls:
          if (a.playerBalls < b.playerBalls) res = -1
          if (a.playerBalls > b.playerBalls) res = 1
          break;
        case SortOrder.ballsPerOver:
          if (a.ballsPerOver < b.ballsPerOver) res = -1
          if (a.ballsPerOver > b.ballsPerOver) res = 1
          break;
        case SortOrder.maidens:
          if (a.playerMaidens < b.playerMaidens) res = -1
          if (a.playerMaidens > b.playerMaidens) res = 1
          break;
        case SortOrder.runs:
          if (a.playerRuns < b.playerRuns) res = -1
          if (a.playerRuns > b.playerRuns) res = 1
          break;
        case SortOrder.wickets:
          if (a.playerWickets < b.playerWickets) res = -1
          if (a.playerWickets > b.playerWickets) res = 1
          break;
        case SortOrder.econ: {
          let aballs = (a.playerBalls == 0) ? 99999 : a.playerBalls
          let bballs = (b.playerBalls == 0) ? 99999 : b.playerBalls
          if (a.playerRuns / aballs < b.playerRuns / bballs) res = -1
          if (a.playerRuns / aballs > b.playerRuns / bballs) res = 1
          break;
        }
        case SortOrder.innings:
          if (a.inningsNumber < b.inningsNumber) res = -1
          if (a.inningsNumber > b.inningsNumber) res = 1
          break;
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }

    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
)

export const loadByMatchBowlingReducer = createReducer(
  initialBowlingInnByInnRecordState,
  on(LoadByMatchBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsByMatchAction, (state, records) => {
    let sortedData = state.data.slice().sort(sort)

    function sort(a: IndividualBowlingDetailsDto, b: IndividualBowlingDetailsDto) {
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
        case SortOrder.balls:
          if (a.playerBalls < b.playerBalls) res = -1
          if (a.playerBalls > b.playerBalls) res = 1
          break;
        case SortOrder.ballsPerOver:
          if (a.ballsPerOver < b.ballsPerOver) res = -1
          if (a.ballsPerOver > b.ballsPerOver) res = 1
          break;
        case SortOrder.maidens:
          if (a.playerMaidens < b.playerMaidens) res = -1
          if (a.playerMaidens > b.playerMaidens) res = 1
          break;
        case SortOrder.runs:
          if (a.playerRuns < b.playerRuns) res = -1
          if (a.playerRuns > b.playerRuns) res = 1
          break;
        case SortOrder.wickets:
          if (a.playerWickets < b.playerWickets) res = -1
          if (a.playerWickets > b.playerWickets) res = 1
          break;
        case SortOrder.econ: {
          let aballs = (a.playerBalls == 0) ? 99999 : a.playerBalls
          let bballs = (b.playerBalls == 0) ? 99999 : b.playerBalls
          if (a.playerRuns / aballs < b.playerRuns / bballs) res = -1
          if (a.playerRuns / aballs > b.playerRuns / bballs) res = 1
          break;
        }
        case SortOrder.innings:
          if (a.inningsNumber < b.inningsNumber) res = -1
          if (a.inningsNumber > b.inningsNumber) res = 1
          break;
      }
      if (records.payload.sortDirection == 'DESC')
        res = -res;
      return res
    }
    return {data: sortedData, sortOrder: records.payload.sortOrder, sortDirection: records.payload.sortDirection}
  })
)

export const loadBySeriesBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadBySeriesBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsBySeriesAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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

export const loadByGroundBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByGroundBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsByGroundAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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


export const loadByHostBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByHostBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsByHostAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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

export const loadByOppositionBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByOppositionBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsByOppositionAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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

export const loadByYearBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadByYearBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsByYearAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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

export const loadBySeasonBowlingReducer = createReducer(
  initialBowlingOverallRecordState,
  on(LoadBySeasonBowlingRecordsSuccessAction, (state, records) => {
    return {
      data: records.payload.data,
      sortOrder: records.payload.sortOrder,
      sortDirection: records.payload.sortDirection
    }
  }),
  on(SortBowlingRecordsBySeasonAction, (state, records) => {

    let sortedData = state.data.slice().sort(sort)

    function sort(a: BowlingCareerRecordDto, b: BowlingCareerRecordDto) {
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
        case SortOrder.balls:
          if (a.balls < b.balls) res = -1
          if (a.balls > b.balls) res = 1
          break;
        case SortOrder.maidens:
          if (a.maidens < b.maidens) res = -1
          if (a.maidens > b.maidens) res = 1
          break;
        case SortOrder.runs:
          if (a.runs < b.runs) res = -1
          if (a.runs > b.runs) res = 1
          break;
        case SortOrder.wickets:
          if (a.wickets < b.wickets) res = -1
          if (a.wickets > b.wickets) res = 1
          break;
        case SortOrder.avg:
          if (a.avg < b.avg) res = -1
          if (a.avg > b.avg) res = 1
          break;
        case SortOrder.bbi:
          if (a.bbiw < b.bbiw) res = -1
          if (a.bbiw > b.bbiw) res = 1
          if(a.bbiw == b.bbiw) {
            if(a.bbir < b.bbir) res = 1
            if(a.bbir > b.bbir) res = -1
          }
          break;
        case SortOrder.bbm:
          if (a.bbmw < b.bbmw) res = -1
          if (a.bbmw > b.bbmw) res = 1
          if(a.bbmw == b.bbmw) {
            if(a.bbmr < b.bbmr) res = 1
            if(a.bbmr > b.bbmr) res = -1
          }
          break;
        case SortOrder.fours:
          if (a.fours < b.fours) res = -1
          if (a.fours > b.fours) res = 1
          break;
        case SortOrder.sixes:
          if (a.sixes < b.sixes) res = -1
          if (a.sixes > b.sixes) res = 1
          break;
        case SortOrder.tenfor:
          if (a.tenFor < b.tenFor) res = -1
          if (a.tenFor > b.tenFor) res = 1
          break;
        case SortOrder.fivefor:
          if (a.fiveFor < b.fiveFor) res = -1
          if (a.fiveFor > b.fiveFor) res = 1
          break;
        case SortOrder.strikeRate: {
          let awickets = (a.wickets == 0) ? 99999 : a.wickets
          let bwickets = (b.wickets == 0) ? 99999 : b.wickets
          if (a.balls / awickets < b.balls / bwickets) res = -1
          if (a.balls / awickets > b.balls / bwickets) res = 1
          break;
        }
        case SortOrder.econ: {
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
