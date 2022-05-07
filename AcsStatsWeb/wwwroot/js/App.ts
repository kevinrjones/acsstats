function setupWomensHomePage() {
    new WomensHomePageMatches()
}

function setupScorecardHomePage() {
    new WomensScorecard()
}

interface LoadTeams {
    (data: TeamEnvelope): void
}

interface LoadDates {
    (data: DatesEnvelope): void
}

class WomensHomePage {
    private formState: FormState;
    protected team: HTMLSelectElement
    protected opponents: HTMLSelectElement
    protected hostcountry: HTMLSelectElement
    protected ground: HTMLSelectElement
    protected startDate: HTMLInputElement
    protected endDate: HTMLInputElement

    protected loadState = () => {
        this.formState = JSON.parse(localStorage.getItem("pageState"))
        console.log(`loadState ${JSON.stringify(this.formState)}`);

        if (this.formState != null) {
            this.team.selectedIndex = this.formState.team
            this.opponents.selectedIndex = this.formState.opponents
            this.hostcountry.selectedIndex = this.formState.host
            this.ground.selectedIndex = this.formState.ground
            this.startDate.value = this.formState.startDate
            this.endDate.value = this.formState.endDate
        }
    }

    protected getTeamsForMatchTypes = (team: HTMLSelectElement, opponents: HTMLSelectElement, matchType: HTMLSelectElement, loadTeams: LoadTeams) => {
        $.get(`/api/Teams/${matchType.selectedOptions[0].value}`)
            .done((data: TeamEnvelope) => {
                loadTeams(data)
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }

    protected getStartAndEndDateForMatchTypes = (start: HTMLInputElement, end: HTMLInputElement, matchType: HTMLSelectElement) => {
        $.get(`/api/Matches/Dates/${matchType.selectedOptions[0].value}`)
            .done((data: StartEndDatesEnvelope) => {
                this.formState = JSON.parse(localStorage.getItem("pageState"))
                console.log("got dates: " + data.result[0].date + ", " + data.result[1].date)
                this.formState.startDate = data.result[0].date;
                this.formState.endDate = data.result[1].date;

                localStorage.setItem("pageState", JSON.stringify(this.formState))
                this.loadState()
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }

    protected getGroundsForMatchTypes = (ground: HTMLSelectElement, matchType: HTMLSelectElement) => {
        $.get(`/api/Grounds/${matchType.selectedOptions[0].value}`)
            .done((data: GroundEnvelope) => {
                var selectItems = data.result.map(ground => {
                    const sd = new SelectData(
                        {id: ground.id, name: `${ground.code}: ${ground.knownAs}`, matchType: ground.matchType})
                    return sd
                })
                this.buildSelectListWithFirstEntry("all grounds", ground, selectItems, "0")
                this.loadState()
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }

    protected getCountriesForMatchTypes = (hostCountry: HTMLSelectElement, matchType: HTMLSelectElement) => {
        $.get(`/api/Countries/${matchType.selectedOptions[0].value}`)
            .done((data: CountryEnvelope) => {
                var selectItems = data.result.map(country => {
                    const sd = new SelectData(
                        {id: country.id, name: `${country.name}`, matchType: country.matchType})
                    return sd
                })
                this.buildSelectListWithFirstEntry("all countries", hostCountry, selectItems, "0")
                this.loadState()
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }

    protected getSeasonsForMatchTypes = (season: HTMLSelectElement, matchType: HTMLSelectElement) => {
        $.get(`/api/Matches/seriesdates/${matchType.selectedOptions[0].value}`)
            .done((data) => {
                let selectData = data.result.map((it: string) => new SelectData({
                        id: it,
                        name: it,
                        matchType: matchType.value
                    })
                )
                this.buildSelectListWithFirstEntry("all seasons", season, selectData, "0")
                this.loadState()
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }
    protected buildTeamsSelectList = (team: HTMLSelectElement, opponents: HTMLSelectElement, data: TeamEnvelope) => {
        this.buildSelectListWithFirstEntry("all teams", team, data.result, "0")
        this.buildSelectListWithFirstEntry("all teams", opponents, data.result, "0")
    }


    protected buildSelectListWithFirstEntry = (firstRow: string, selectList: HTMLSelectElement, data: SelectData[], initialValue: string) => {
        selectList.options.length = 0
        this.addOption(selectList, firstRow, initialValue);

        this.buildSelectList(selectList, data)
    }

    protected buildSelectList = (selectList: HTMLSelectElement, data: SelectData[]) => {
        data.map((d: SelectData) => {
            this.addOption(selectList, d.name, d.id.toString());
        })
    }

    protected addOption = (team: HTMLSelectElement, name: string, value: string) => {
        let newElement = new Option()
        newElement.label = name
        newElement.value = value
        team.add(newElement)
    }
}

class WomensScorecard extends WomensHomePage {
    matchType: HTMLSelectElement
    dates: HTMLSelectElement
    getCard: HTMLButtonElement

    constructor() {
        super();

        this.matchType = <HTMLSelectElement>document.getElementById("matchType");
        this.matchType.onchange = this.matchTypeOnChange

        this.team = <HTMLSelectElement>document.getElementById("teamid");
        this.team.onchange = this.teamOnChange

        this.opponents = <HTMLSelectElement>document.getElementById("opponentsid");
        this.opponents.onchange = this.teamOnChange

        this.dates = <HTMLSelectElement>document.getElementById("datesid");
        this.getCard = <HTMLButtonElement>document.getElementById("getcardid");

        this.getTeamsForMatchTypes(this.team, this.opponents, this.matchType, data => {
            this.buildSelectList(this.team, data.result)
            this.buildSelectList(this.opponents, data.result)
        });

        this.getCard.onclick = this.getScorecard

    }

    protected getScorecard = (evt: Event) => {
        evt.preventDefault()

        var url = `/scorecard/${encodeURIComponent(this.team.selectedOptions[0].label)}-v-${encodeURIComponent(this.opponents.selectedOptions[0].label)}-${encodeURIComponent(this.dates.selectedOptions[0].value)}`
        console.log(`url '${url}'`)

        window.location.href = url
    }

    protected teamOnChange = (evt: Event) => {
        this.getMatchDatesForTeamsAndMatchType(this.team, this.opponents, this.matchType, data => {
            this.dates.options.length = 0
            data.result.map((d: string) => {
                this.addOption(this.dates, d, d);
            })
        });
    }

    protected getMatchDatesForTeamsAndMatchType = (team: HTMLSelectElement, opponents: HTMLSelectElement, matchType: HTMLSelectElement, loadDates: LoadDates) => {
        $.get(`/api/Matches/matchdates/${team.selectedOptions[0].value}/${opponents.selectedOptions[0].value}/${matchType.selectedOptions[0].value}`)
            .done((data: DatesEnvelope) => {
                loadDates(data);
            })
            .fail(function () {
                alert("unable to connect to the server");
            })
    }


    matchTypeOnChange = (evt: Event) => {
        this.getTeamsForMatchTypes(this.team, this.opponents, this.matchType, data => {
            this.team.options.length = 0
            this.opponents.options.length = 0
            this.buildSelectList(this.team, data.result)
            this.buildSelectList(this.opponents, data.result)
        })
        this.getMatchDatesForTeamsAndMatchType(this.team, this.opponents, this.matchType, data => {
            this.dates.options.length = 0
            data.result.map((d: string) => {
                this.addOption(this.dates, d, d);
            })
        });
    }
}


class WomensHomePageMatches extends WomensHomePage {
    season: HTMLSelectElement
    matchType: HTMLSelectElement
    extrasByInnings: HTMLInputElement
    InningsByInnings: HTMLInputElement
    matchTotals: HTMLInputElement
    teamlimit: HTMLInputElement
    battinglimit: HTMLInputElement
    bowlinglimit: HTMLInputElement
    partnershipLimit: HTMLInputElement
    overallFigures: HTMLInputElement
    seriesAverage: HTMLInputElement;
    submit: HTMLInputElement;
    reset: HTMLInputElement;
    private groundAverages: HTMLInputElement;
    private byOppositionTeam: HTMLInputElement;
    private byYearOfMatchStart: HTMLInputElement;
    private bySeason: HTMLInputElement;
    private byHostCountry: HTMLInputElement;

    constructor() {
        super();

        this.matchType = <HTMLSelectElement>document.getElementById("matchType");
        this.team = <HTMLSelectElement>document.getElementById("teamid");
        this.opponents = <HTMLSelectElement>document.getElementById("opponentsid");
        this.hostcountry = <HTMLSelectElement>document.getElementById("hostcountryid");
        this.ground = <HTMLSelectElement>document.getElementById("groundid");
        this.season = <HTMLSelectElement>document.getElementById("seasonid");
        this.startDate = <HTMLInputElement>document.getElementById("startdateid");
        this.endDate = <HTMLInputElement>document.getElementById("enddateid");
        this.extrasByInnings = <HTMLInputElement>document.getElementById("extrasByInnings");
        this.InningsByInnings = <HTMLInputElement>document.getElementById("InningsByInnings");
        this.seriesAverage = <HTMLInputElement>document.getElementById("SeriesAverages");
        this.groundAverages = <HTMLInputElement>document.getElementById("Groundaverages");
        this.byHostCountry = <HTMLInputElement>document.getElementById("byHostCountry");
        this.byOppositionTeam = <HTMLInputElement>document.getElementById("byOppositionTeam");
        this.byYearOfMatchStart = <HTMLInputElement>document.getElementById("byYearOfMatchStart");
        this.bySeason = <HTMLInputElement>document.getElementById("bySeason");
        this.matchTotals = <HTMLInputElement>document.getElementById("matchTotals");
        this.teamlimit = <HTMLInputElement>document.getElementById("teamlimit");
        this.battinglimit = <HTMLInputElement>document.getElementById("battinglimit");
        this.bowlinglimit = <HTMLInputElement>document.getElementById("bowlinglimit");
        this.partnershipLimit = <HTMLInputElement>document.getElementById("partnershiplimit");
        this.overallFigures = <HTMLInputElement>document.getElementById("overallFigures");
        this.submit = <HTMLInputElement>document.getElementById("submit");
        this.reset = <HTMLInputElement>document.getElementById("reset");


        this.matchType.onchange = this.matchTypeOnChange
        if (this.overallFigures != null)
            this.overallFigures.onchange = this.setLimit
        if (this.InningsByInnings != null)
            this.InningsByInnings.onchange = this.setLimit
        if (this.matchTotals != null)
            this.matchTotals.onchange = this.setLimit
        if (this.seriesAverage != null)
            this.seriesAverage.onchange = this.setLimit
        if (this.groundAverages != null)
            this.groundAverages.onchange = this.setLimit
        if (this.byHostCountry != null)
            this.byHostCountry.onchange = this.setLimit
        if (this.byOppositionTeam != null)
            this.byOppositionTeam.onchange = this.setLimit
        if (this.byYearOfMatchStart != null)
            this.byYearOfMatchStart.onchange = this.setLimit
        if (this.bySeason != null)
            this.bySeason.onchange = this.setLimit
        if (this.extrasByInnings != null)
            this.extrasByInnings.onchange = this.setLimit

        this.getTeamsForMatchTypes(this.team, this.opponents, this.matchType, data => {
            this.buildTeamsSelectList(this.team, this.opponents, data)
            this.loadState()
        });
        this.getGroundsForMatchTypes(this.ground, this.matchType);
        this.getCountriesForMatchTypes(this.hostcountry, this.matchType);
        this.getSeasonsForMatchTypes(this.season, this.matchType);
        this.getStartAndEndDateForMatchTypes(this.startDate, this.endDate, this.matchType)

        this.submit.onclick = this.saveState;
        this.reset.onclick = this.resetForm;


    }

    matchTypeOnChange = (evt: Event) => {
        this.getTeamsForMatchTypes(this.team, this.opponents, this.matchType, data => {
            this.buildTeamsSelectList(this.team, this.opponents, data)
            this.loadState()
        })
        this.getGroundsForMatchTypes(this.ground, this.matchType)
        this.getStartAndEndDateForMatchTypes(this.startDate, this.endDate, this.matchType)
    }

    setLimit = (evt: Event) => {

        if (this.teamlimit != null) {
            this.setTeamLimits()
        } else if (this.battinglimit != null) {
            this.setBattingLimit()
        }
    }


    private setTeamLimits() {
        this.teamlimit.disabled = false;

        if (this.overallFigures?.checked)
            this.teamlimit.value = "200";
        else if (this.extrasByInnings?.checked)
            this.teamlimit.value = "50";
        else if (this.InningsByInnings?.checked)
            this.teamlimit.value = "350";
        else if (this.matchTotals?.checked)
            this.teamlimit.value = "800";

        else if (this.seriesAverage?.checked || this.groundAverages?.checked
            || this.byHostCountry?.checked || this.byOppositionTeam?.checked || this.byYearOfMatchStart?.checked
            || this.bySeason?.checked) {
            this.teamlimit.value = "";
            this.teamlimit.disabled = true;
        }
    }

    private setBattingLimit() {
        if (this.overallFigures.checked)
            this.battinglimit.value = "5000"
        else if (this.InningsByInnings.checked)
            this.battinglimit.value = "200";
        else if (this.matchTotals?.checked)
            this.battinglimit.value = "300";
        else if (this.seriesAverage?.checked || this.groundAverages?.checked
            || this.byHostCountry?.checked || this.byOppositionTeam?.checked || this.byYearOfMatchStart?.checked
            || this.bySeason?.checked)
            this.battinglimit.value = "700";
    }

    private setBowlingLimit() {
        if (this.overallFigures != null && this.overallFigures.checked)
            this.bowlinglimit.value = "200"
        else if (this.InningsByInnings != null && this.InningsByInnings.checked)
            this.bowlinglimit.value = "7";
        else if (this.matchTotals != null && this.matchTotals.checked)
            this.bowlinglimit.value = "10";
        else if (this.seriesAverage?.checked || this.groundAverages?.checked
            || this.byHostCountry?.checked || this.byOppositionTeam?.checked || this.byYearOfMatchStart?.checked
            || this.bySeason?.checked)
            this.bowlinglimit.value = "30";
    }

    private saveState = (evt: Event) => {
        let state = new FormState()
        state.team = this.team.selectedIndex
        state.opponents = this.opponents.selectedIndex
        state.ground = this.ground.selectedIndex
        state.host = this.hostcountry.selectedIndex
        state.season = this.season.selectedIndex
        state.startDate = this.startDate.value
        state.endDate = this.endDate.value

        localStorage.setItem("pageState", JSON.stringify(state))

        console.log(`state: ${JSON.stringify(state)}`)
    }

    private resetForm = (evt: Event) => {
        this.team.selectedIndex = 0
        this.opponents.selectedIndex = 0
        this.hostcountry.selectedIndex = 0
        this.ground.selectedIndex = 0
        this.season.selectedIndex = 0
        this.startDate.value = ""
        this.endDate.value = ""

        localStorage.setItem("pageState", JSON.stringify(new FormState()))

        this.getStartAndEndDateForMatchTypes(this.startDate, this.endDate, this.matchType)
    }
}


class Envelope<T> {
    result: T[];
    errorMessage: string;
    timeGenerated: Date;
}

class Team {
    matchType: string;
    name: string;
    id: number;
}

class Overall {
    matchType: string;
    matches: number;
    runs: number
    notouts: number
    balls: number
    fours: number
    sixes: number
    hundreds: number
    fifties: number
}

class TeamEnvelope extends Envelope<Team> {

}

class OverallEnvelope extends Envelope<Array<Overall>> {

}

class DatesEnvelope extends Envelope<string> {

}


class Country {
    name: string;
    id: number;
    matchType: string;
}

class CountryEnvelope extends Envelope<Country> {

}

class Ground {
    matchType: string;
    knownAs: string;
    code: string;
    groundId: number;
    id: number;
}

class GroundEnvelope extends Envelope<Ground> {

}

class StartEndDates {
    id: number;
    date: string;
    dateOffset: number;
    matchtype: string;
}

class StartEndDatesEnvelope extends Envelope<StartEndDates> {

}

class SelectData {
    id: number | string;
    matchType: string;
    name: string;

    constructor(
        {
            id,
            matchType,
            name
        }: SelectDataParameters) {
        this.name = name
        this.id = id
        this.matchType = matchType
    }
}

interface SelectDataParameters {
    id: number | string,
    matchType: string,
    name: string
}

class FormState {
    team: number;
    opponents: number;
    host: number;
    ground: number;
    season: number;
    startDate: string;
    endDate: string;
}



