import {Component, Input} from '@angular/core';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-by-decade-list',
  templateUrl: './by-decade-list.component.html',
  styleUrls: ['./by-decade-list.component.css']
})
export class ScorecardByDecadeListComponent {
  @Input() byYearList$!: Observable<{ [decade: number]: string[] }>;

  constructor() {
  }

  ngOnInit() {
  }

  encodeDate(year: string) {
    return encodeURIComponent(year);
  }

}
