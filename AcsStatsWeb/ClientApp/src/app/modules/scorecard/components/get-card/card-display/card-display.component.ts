import {Component, Input, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Store} from '@ngrx/store';
import {Observable} from 'rxjs';
import {BattlingLine, Inning, Scorecard, VictoryType} from '../../../models/scorecard.model';

@Component({
  selector: 'app-card-display',
  templateUrl: './card-display.component.html',
  styleUrls: ['./card-display.component.css']
})
export class CardDisplayComponent implements OnInit {

  @Input() scorecard$!: Observable<Scorecard>;

  constructor() {
  }

  ngOnInit(): void {
  }

  isVictory(scorecard: Scorecard, teamKey: number) {
    return (scorecard.header.result.whoWon != null
      && scorecard.header.result.victoryType != VictoryType.Drawn
      && scorecard.header.result.victoryType != VictoryType.NoResult
      && scorecard.header.result.victoryType != VictoryType.Abandoned
      && scorecard.header.result.whoWon.key == teamKey)
  }

  getInningsDesc(inningsNumber: number) {
    return inningsNumber === 1 ? '1st' : '2nd'
  }

  getDismisal(battingLine: BattlingLine) {

    if (battingLine.dismissal.dismissalType == 0) {
      if (battingLine.dismissal.bowler.key == battingLine.dismissal.fielder.key) {
        return `<text>c & b</text>
        <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
      } else {
        return `<text>c</text>
          <a [routerLink]='['/player/${battingLine.dismissal.fielder.key}]'>${battingLine.dismissal.fielder.name}</a>
          <text> b</text>
          <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
      }
    } else if (battingLine.dismissal.dismissalType == 1) {
      return `<text>b</text+
        <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
    } else if (battingLine.dismissal.dismissalType == 2) {
      return `<text>lbw b</text>
        <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
    } else if (battingLine.dismissal.dismissalType == 3) {
      return `<text>st</text>
        <a href='/player/${battingLine.dismissal.fielder.key}'>${battingLine.dismissal.fielder.name}</a>
        <text> b</text>
        <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
    } else if (battingLine.dismissal.dismissalType == 3) {
      return `<text>Hit wicket b</text>
        <a [routerLink]='['/player/${battingLine.dismissal.bowler.key}]'>${battingLine.dismissal.bowler.name}</a>`
    } else {
      return battingLine.dismissal.dismissal
    }
  }

  getFow(innings: Inning) {


    let fows: Array<string> = []

    innings.fallOfWickets.forEach(fow => {
      let score:string = fow.score != null ? fow.score.toString(10) : '?'
      let fowText = `${fow.wicket} - ${score} <a [routerLink]='/player/${fow.player.key}'>(${fow.player.name}</a>`
      if(fow.overs != null && fow.overs.length > 0) {
        fowText += `, ${fow.overs} ov`
      }
      fowText += ')'
      fows.push(fowText)
    })

    return fows.join(', ')
  }
}
