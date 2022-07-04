import {Component, Input, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {Player} from '../../../models/player';
import {DateTime} from 'luxon';

@Component({
  selector: 'app-players-list',
  templateUrl: './players-list.component.html',
  styleUrls: ['./players-list.component.css']
})
export class PlayersListComponent implements OnInit {

  @Input() players$!: Observable<Array<Player>>;
  @Input() name: string = ''
  @Input() team = ''
  @Input() exactMatch = false
  @Input() debutDate = ''
  @Input() activeUntilDate = ''

  constructor() { }

  ngOnInit(): void {
  }

  getDate(date: string, replacementDate: string) : string {
    if(date == null || date == '') date = replacementDate

    const dt = DateTime.fromISO(date)
    return dt.toLocaleString(DateTime.DATE_FULL)
  }

}
