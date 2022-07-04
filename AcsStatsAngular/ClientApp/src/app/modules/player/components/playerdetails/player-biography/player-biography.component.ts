import {ChangeDetectionStrategy, Component, Input, OnInit} from '@angular/core';
import {Observable} from 'rxjs';
import {PlayerBiography, PlayerBowlingDetails, PlayerOverall} from '../../../playerbiography.model';

@Component({
  selector: 'app-player-biography',
  templateUrl: './player-biography.component.html',
  styleUrls: ['./player-biography.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PlayerBiographyComponent implements OnInit {

  @Input() playerBiography$!: Observable<PlayerBiography>;

  constructor() {
  }

  ngOnInit(): void {
  }

}
