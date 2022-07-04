var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
function setupWomensHomePage() {
    new HomePageMatches();
}
function setupScorecardHomePage() {
    new Scorecard();
}
var HomePage = /** @class */ (function () {
    function HomePage() {
        var _this = this;
        this.loadState = function () {
            _this.formState = JSON.parse(localStorage.getItem('pageState'));
            if (_this.formState != null) {
                _this.team.selectedIndex = _this.formState.team;
                _this.opponents.selectedIndex = _this.formState.opponents;
                _this.hostcountry.selectedIndex = _this.formState.host;
                _this.ground.selectedIndex = _this.formState.ground;
                _this.startDate.value = _this.formState.startDate;
                _this.endDate.value = _this.formState.endDate;
            }
        };
        this.getTeamsForMatchTypes = function (team, opponents, matchType, loadTeams) {
            $.get("/api/Teams/".concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                loadTeams(data);
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        this.getStartAndEndDateForMatchTypes = function (start, end, matchType) {
            $.get("/api/Matches/Dates/".concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                _this.formState = JSON.parse(localStorage.getItem('pageState'));
                _this.formState.startDate = data.result[0].date;
                _this.formState.endDate = data.result[1].date;
                localStorage.setItem('pageState', JSON.stringify(_this.formState));
                _this.loadState();
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        this.getGroundsForMatchTypes = function (ground, matchType) {
            $.get("/api/Grounds/".concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                var selectItems = data.result.map(function (ground) {
                    var sd = new SelectData({ id: ground.id, name: "".concat(ground.code, ": ").concat(ground.knownAs), matchType: ground.matchType });
                    return sd;
                });
                _this.buildSelectListWithFirstEntry('all grounds', ground, selectItems, '0');
                _this.loadState();
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        this.getCountriesForMatchTypes = function (hostCountry, matchType) {
            $.get("/api/Countries/".concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                var selectItems = data.result.map(function (country) {
                    var sd = new SelectData({ id: country.id, name: "".concat(country.name), matchType: country.matchType });
                    return sd;
                });
                _this.buildSelectListWithFirstEntry('all countries', hostCountry, selectItems, '0');
                _this.loadState();
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        this.getSeasonsForMatchTypes = function (season, matchType) {
            $.get("/api/Matches/seriesdates/".concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                var selectData = data.result.map(function (it) { return new SelectData({
                    id: it,
                    name: it,
                    matchType: matchType.value
                }); });
                _this.buildSelectListWithFirstEntry('all seasons', season, selectData, '0');
                _this.loadState();
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        this.buildTeamsSelectList = function (team, opponents, data) {
            _this.buildSelectListWithFirstEntry('all teams', team, data.result, '0');
            _this.buildSelectListWithFirstEntry('all teams', opponents, data.result, '0');
        };
        this.buildSelectListWithFirstEntry = function (firstRow, selectList, data, initialValue) {
            selectList.options.length = 0;
            _this.addOption(selectList, firstRow, initialValue);
            _this.buildSelectList(selectList, data);
        };
        this.buildSelectList = function (selectList, data) {
            data.map(function (d) {
                _this.addOption(selectList, d.name, d.id.toString());
            });
        };
        this.addOption = function (team, name, value) {
            var newElement = new Option();
            newElement.label = name;
            newElement.value = value;
            team.add(newElement);
        };
    }
    return HomePage;
}());
var Scorecard = /** @class */ (function (_super) {
    __extends(Scorecard, _super);
    function Scorecard() {
        var _this = _super.call(this) || this;
        _this.getScorecard = function (evt) {
            evt.preventDefault();
            var url = "/scorecard/".concat(encodeURIComponent(_this.team.selectedOptions[0].label), "-v-").concat(encodeURIComponent(_this.opponents.selectedOptions[0].label), "-").concat(encodeURIComponent(_this.dates.selectedOptions[0].value));
            window.location.href = url;
        };
        _this.teamOnChange = function (evt) {
            _this.getMatchDatesForTeamsAndMatchType(_this.team, _this.opponents, _this.matchType, function (data) {
                _this.dates.options.length = 0;
                data.result.map(function (d) {
                    _this.addOption(_this.dates, d, d);
                });
            });
        };
        _this.getMatchDatesForTeamsAndMatchType = function (team, opponents, matchType, loadDates) {
            $.get("/api/Matches/matchdates/".concat(team.selectedOptions[0].value, "/").concat(opponents.selectedOptions[0].value, "/").concat(matchType.selectedOptions[0].value))
                .done(function (data) {
                loadDates(data);
            })
                .fail(function () {
                alert('unable to connect to the server');
            });
        };
        _this.matchTypeOnChange = function (evt) {
            _this.getTeamsForMatchTypes(_this.team, _this.opponents, _this.matchType, function (data) {
                _this.team.options.length = 0;
                _this.opponents.options.length = 0;
                _this.buildSelectList(_this.team, data.result);
                _this.buildSelectList(_this.opponents, data.result);
            });
            _this.getMatchDatesForTeamsAndMatchType(_this.team, _this.opponents, _this.matchType, function (data) {
                _this.dates.options.length = 0;
                data.result.map(function (d) {
                    _this.addOption(_this.dates, d, d);
                });
            });
        };
        _this.matchType = document.getElementById('matchType');
        _this.matchType.onchange = _this.matchTypeOnChange;
        _this.team = document.getElementById('teamid');
        _this.team.onchange = _this.teamOnChange;
        _this.opponents = document.getElementById('opponentsid');
        _this.opponents.onchange = _this.teamOnChange;
        _this.dates = document.getElementById('datesid');
        _this.getCard = document.getElementById('getcardid');
        _this.getTeamsForMatchTypes(_this.team, _this.opponents, _this.matchType, function (data) {
            _this.buildSelectList(_this.team, data.result);
            _this.buildSelectList(_this.opponents, data.result);
        });
        _this.getCard.onclick = _this.getScorecard;
        return _this;
    }
    return Scorecard;
}(HomePage));
var HomePageMatches = /** @class */ (function (_super) {
    __extends(HomePageMatches, _super);
    function HomePageMatches() {
        var _this = _super.call(this) || this;
        _this.matchTypeOnChange = function (evt) {
            _this.getTeamsForMatchTypes(_this.team, _this.opponents, _this.matchType, function (data) {
                _this.buildTeamsSelectList(_this.team, _this.opponents, data);
                _this.loadState();
            });
            _this.getGroundsForMatchTypes(_this.ground, _this.matchType);
            _this.getStartAndEndDateForMatchTypes(_this.startDate, _this.endDate, _this.matchType);
        };
        _this.setLimit = function (evt) {
            if (_this.teamlimit != null) {
                _this.setTeamLimits();
            }
            else if (_this.battinglimit != null) {
                _this.setBattingLimit();
            }
        };
        _this.saveState = function (evt) {
            var state = new FormState();
            state.team = _this.team.selectedIndex;
            state.opponents = _this.opponents.selectedIndex;
            state.ground = _this.ground.selectedIndex;
            state.host = _this.hostcountry.selectedIndex;
            state.season = _this.season.selectedIndex;
            state.startDate = _this.startDate.value;
            state.endDate = _this.endDate.value;
            localStorage.setItem('pageState', JSON.stringify(state));
        };
        _this.resetForm = function (evt) {
            _this.team.selectedIndex = 0;
            _this.opponents.selectedIndex = 0;
            _this.hostcountry.selectedIndex = 0;
            _this.ground.selectedIndex = 0;
            _this.season.selectedIndex = 0;
            _this.startDate.value = '';
            _this.endDate.value = '';
            localStorage.setItem('pageState', JSON.stringify(new FormState()));
            _this.getStartAndEndDateForMatchTypes(_this.startDate, _this.endDate, _this.matchType);
        };
        _this.matchType = document.getElementById('matchType');
        _this.team = document.getElementById('teamid');
        _this.opponents = document.getElementById('opponentsid');
        _this.hostcountry = document.getElementById('hostcountryid');
        _this.ground = document.getElementById('groundid');
        _this.season = document.getElementById('seasonid');
        _this.startDate = document.getElementById('startdateid');
        _this.endDate = document.getElementById('enddateid');
        _this.extrasByInnings = document.getElementById('extrasByInnings');
        _this.InningsByInnings = document.getElementById('InningsByInnings');
        _this.seriesAverage = document.getElementById('SeriesAverages');
        _this.groundAverages = document.getElementById('Groundaverages');
        _this.byHostCountry = document.getElementById('byHostCountry');
        _this.byOppositionTeam = document.getElementById('byOppositionTeam');
        _this.byYearOfMatchStart = document.getElementById('byYearOfMatchStart');
        _this.bySeason = document.getElementById('bySeason');
        _this.matchTotals = document.getElementById('matchTotals');
        _this.teamlimit = document.getElementById('teamlimit');
        _this.battinglimit = document.getElementById('battinglimit');
        _this.bowlinglimit = document.getElementById('bowlinglimit');
        _this.partnershipLimit = document.getElementById('partnershiplimit');
        _this.overallFigures = document.getElementById('overallFigures');
        _this.submit = document.getElementById('submit');
        _this.reset = document.getElementById('reset');
        _this.matchType.onchange = _this.matchTypeOnChange;
        if (_this.overallFigures != null)
            _this.overallFigures.onchange = _this.setLimit;
        if (_this.InningsByInnings != null)
            _this.InningsByInnings.onchange = _this.setLimit;
        if (_this.matchTotals != null)
            _this.matchTotals.onchange = _this.setLimit;
        if (_this.seriesAverage != null)
            _this.seriesAverage.onchange = _this.setLimit;
        if (_this.groundAverages != null)
            _this.groundAverages.onchange = _this.setLimit;
        if (_this.byHostCountry != null)
            _this.byHostCountry.onchange = _this.setLimit;
        if (_this.byOppositionTeam != null)
            _this.byOppositionTeam.onchange = _this.setLimit;
        if (_this.byYearOfMatchStart != null)
            _this.byYearOfMatchStart.onchange = _this.setLimit;
        if (_this.bySeason != null)
            _this.bySeason.onchange = _this.setLimit;
        if (_this.extrasByInnings != null)
            _this.extrasByInnings.onchange = _this.setLimit;
        _this.getTeamsForMatchTypes(_this.team, _this.opponents, _this.matchType, function (data) {
            _this.buildTeamsSelectList(_this.team, _this.opponents, data);
            _this.loadState();
        });
        _this.getGroundsForMatchTypes(_this.ground, _this.matchType);
        _this.getCountriesForMatchTypes(_this.hostcountry, _this.matchType);
        _this.getSeasonsForMatchTypes(_this.season, _this.matchType);
        _this.getStartAndEndDateForMatchTypes(_this.startDate, _this.endDate, _this.matchType);
        _this.submit.onclick = _this.saveState;
        _this.reset.onclick = _this.resetForm;
        return _this;
    }
    HomePageMatches.prototype.setTeamLimits = function () {
        var _a, _b, _c, _d, _e, _f, _g, _h, _j, _k;
        this.teamlimit.disabled = false;
        if ((_a = this.overallFigures) === null || _a === void 0 ? void 0 : _a.checked)
            this.teamlimit.value = '200';
        else if ((_b = this.extrasByInnings) === null || _b === void 0 ? void 0 : _b.checked)
            this.teamlimit.value = '50';
        else if ((_c = this.InningsByInnings) === null || _c === void 0 ? void 0 : _c.checked)
            this.teamlimit.value = '350';
        else if ((_d = this.matchTotals) === null || _d === void 0 ? void 0 : _d.checked)
            this.teamlimit.value = '800';
        else if (((_e = this.seriesAverage) === null || _e === void 0 ? void 0 : _e.checked) || ((_f = this.groundAverages) === null || _f === void 0 ? void 0 : _f.checked)
            || ((_g = this.byHostCountry) === null || _g === void 0 ? void 0 : _g.checked) || ((_h = this.byOppositionTeam) === null || _h === void 0 ? void 0 : _h.checked) || ((_j = this.byYearOfMatchStart) === null || _j === void 0 ? void 0 : _j.checked)
            || ((_k = this.bySeason) === null || _k === void 0 ? void 0 : _k.checked)) {
            this.teamlimit.value = '';
            this.teamlimit.disabled = true;
        }
    };
    HomePageMatches.prototype.setBattingLimit = function () {
        var _a, _b, _c, _d, _e, _f, _g;
        if (this.overallFigures.checked)
            this.battinglimit.value = '5000';
        else if (this.InningsByInnings.checked)
            this.battinglimit.value = '200';
        else if ((_a = this.matchTotals) === null || _a === void 0 ? void 0 : _a.checked)
            this.battinglimit.value = '300';
        else if (((_b = this.seriesAverage) === null || _b === void 0 ? void 0 : _b.checked) || ((_c = this.groundAverages) === null || _c === void 0 ? void 0 : _c.checked)
            || ((_d = this.byHostCountry) === null || _d === void 0 ? void 0 : _d.checked) || ((_e = this.byOppositionTeam) === null || _e === void 0 ? void 0 : _e.checked) || ((_f = this.byYearOfMatchStart) === null || _f === void 0 ? void 0 : _f.checked)
            || ((_g = this.bySeason) === null || _g === void 0 ? void 0 : _g.checked))
            this.battinglimit.value = '700';
    };
    HomePageMatches.prototype.setBowlingLimit = function () {
        var _a, _b, _c, _d, _e, _f;
        if (this.overallFigures != null && this.overallFigures.checked)
            this.bowlinglimit.value = '200';
        else if (this.InningsByInnings != null && this.InningsByInnings.checked)
            this.bowlinglimit.value = '7';
        else if (this.matchTotals != null && this.matchTotals.checked)
            this.bowlinglimit.value = '10';
        else if (((_a = this.seriesAverage) === null || _a === void 0 ? void 0 : _a.checked) || ((_b = this.groundAverages) === null || _b === void 0 ? void 0 : _b.checked)
            || ((_c = this.byHostCountry) === null || _c === void 0 ? void 0 : _c.checked) || ((_d = this.byOppositionTeam) === null || _d === void 0 ? void 0 : _d.checked) || ((_e = this.byYearOfMatchStart) === null || _e === void 0 ? void 0 : _e.checked)
            || ((_f = this.bySeason) === null || _f === void 0 ? void 0 : _f.checked))
            this.bowlinglimit.value = '30';
    };
    return HomePageMatches;
}(HomePage));
var Envelope = /** @class */ (function () {
    function Envelope() {
    }
    return Envelope;
}());
var Team = /** @class */ (function () {
    function Team() {
    }
    return Team;
}());
var Overall = /** @class */ (function () {
    function Overall() {
    }
    return Overall;
}());
var TeamEnvelope = /** @class */ (function (_super) {
    __extends(TeamEnvelope, _super);
    function TeamEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return TeamEnvelope;
}(Envelope));
var OverallEnvelope = /** @class */ (function (_super) {
    __extends(OverallEnvelope, _super);
    function OverallEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return OverallEnvelope;
}(Envelope));
var DatesEnvelope = /** @class */ (function (_super) {
    __extends(DatesEnvelope, _super);
    function DatesEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return DatesEnvelope;
}(Envelope));
var Country = /** @class */ (function () {
    function Country() {
    }
    return Country;
}());
var CountryEnvelope = /** @class */ (function (_super) {
    __extends(CountryEnvelope, _super);
    function CountryEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return CountryEnvelope;
}(Envelope));
var Ground = /** @class */ (function () {
    function Ground() {
    }
    return Ground;
}());
var GroundEnvelope = /** @class */ (function (_super) {
    __extends(GroundEnvelope, _super);
    function GroundEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return GroundEnvelope;
}(Envelope));
var StartEndDates = /** @class */ (function () {
    function StartEndDates() {
    }
    return StartEndDates;
}());
var StartEndDatesEnvelope = /** @class */ (function (_super) {
    __extends(StartEndDatesEnvelope, _super);
    function StartEndDatesEnvelope() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return StartEndDatesEnvelope;
}(Envelope));
var SelectData = /** @class */ (function () {
    function SelectData(_a) {
        var id = _a.id, matchType = _a.matchType, name = _a.name;
        this.name = name;
        this.id = id;
        this.matchType = matchType;
    }
    return SelectData;
}());
var FormState = /** @class */ (function () {
    function FormState() {
    }
    return FormState;
}());
//# sourceMappingURL=App.js.map