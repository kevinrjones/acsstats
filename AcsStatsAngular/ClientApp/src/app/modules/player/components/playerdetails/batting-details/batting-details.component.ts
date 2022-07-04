import {ChangeDetectionStrategy, Component, Input, OnInit} from '@angular/core';
import {PlayerBattingDetails} from '../../../playerbiography.model';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-batting-details',
  templateUrl: './batting-details.component.html',
  styleUrls: ['./batting-details.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BattingDetailsComponent implements OnInit {

  @Input() playerBattingOverall$!: Observable<{ [matchType: string]: PlayerBattingDetails[] }>;
  @Input() listOfSortedMatchType!: string[];

  previousMatchId: number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  getStringValueOrEmpty(rowId: number, name: string) {
    return this.previousMatchId == rowId ? '' : name
  }

  setPreviousMatchId(id: number) {
    this.previousMatchId = id
  }

  getRowClass(id: number, matchType: string) {
    return this.previousMatchId != id && (matchType == 'f' ||  matchType == 'wf' ||  matchType ==  't' ||  matchType ==  'wt') ? 'class=matchRow' : ''
  }

  buildCaptainOrWk(row: PlayerBattingDetails) {
    let score = '';
    if (row.captain != 0) {
      score += '*';
    }
    if (row.wicketKeeper != 0) {
      score += '+';
    }
    return score
  }

  getScore(row: PlayerBattingDetails) {
    if (row.dismissalType != 11 && row.dismissalType != 14) {
      return `${row.score}<span class='position'>${row.position}</span><span class='cwk'>${this.buildCaptainOrWk(row)}</span>`
    } else {
      return '-'
    }
  }

  getBalls(row: PlayerBattingDetails) {
    return row.balls == null ? '-' : row.balls
  }
  getMinutes(row: PlayerBattingDetails) {
    return row.minutes == null ? '-' : row.minutes
  }
  getFours(row: PlayerBattingDetails) {
    return row.fours == null ? '-' : row.fours
  }
  getSixes(row: PlayerBattingDetails) {
    return row.sixes == null ? '-' : row.sixes
  }

}
