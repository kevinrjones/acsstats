import {ChangeDetectionStrategy, Component, Input, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {PlayerBowlingDetails} from '../../../playerbiography.model';

@Component({
  selector: 'app-bowling-details',
  templateUrl: './bowling-details.component.html',
  styleUrls: ['./bowling-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BowlingDetailsComponent implements OnInit {

  @Input() playerBowlingOverall$!: Observable<{ [matchType: string]: PlayerBowlingDetails[] }>;
  @Input() listOfSortedMatchType!: string[];

  previousMatchId: number = 0;

  constructor() {
  }

  ngOnInit(): void {
  }

  setPreviousMatchId(id: number) {
    this.previousMatchId = id
  }

  getStringValueOrEmpty(rowId: number, name: string) {
    return this.previousMatchId == rowId ? '' : name
  }

  getRowClass(id: number, matchType: string) {
    return this.previousMatchId != id && (matchType == 'f' || matchType == 'wf' || matchType == 't' || matchType == 'wt') ? 'class=matchRow' : ''
  }

  caluclateOvers(balls: number, ballsPerOver: number) {
    let overs = Math.floor(balls / ballsPerOver)
    let ballsLeft = balls % ballsPerOver

    return ballsLeft > 0 ? `${overs}.${ballsLeft}` : overs

  }

  getDots(dots: number) {
    return dots == null ? '' : `(${dots})`
  }

}
