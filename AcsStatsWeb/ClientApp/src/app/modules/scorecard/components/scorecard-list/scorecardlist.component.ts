import {Component, Input} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {FindScorecard} from '../../models/scorecard-search.model';
import {Store} from '@ngrx/store';
import {ScorecardState} from '../../models/app-state';
import {Observable} from 'rxjs';
import {LoadScorecardListAction} from '../../actions/scorecard.actions';
import {ScorecardListItem} from '../../models/scorecard-list-item.model';

@Component({
  selector: 'app-scorecard-list',
  templateUrl: './scorecardlist.component.html'
})
export class ScorecardListComponent {
  @Input() matchList$!: Observable<Array<ScorecardListItem>> ;

  constructor(private route: ActivatedRoute,
              private store: Store<ScorecardState>) {

    this.matchList$ = this.store.select(s => s.scorecards.scorecards)

  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      let fs = params as FindScorecard
      this.store.dispatch(LoadScorecardListAction({payload: fs}))
    });

  }

  getEncodedValue(key: string) {
    return encodeURIComponent(key);
  }
}
