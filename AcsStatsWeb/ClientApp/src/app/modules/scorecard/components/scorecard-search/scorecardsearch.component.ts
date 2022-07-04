import {Component} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AbstractControl, FormGroup, FormBuilder, Validators} from '@angular/forms';

@Component({
  selector: 'app-scorecard-search',
  templateUrl: './scorecardsearch.component.html',
  styleUrls: ['./scorecardsearch.component.css']
})
export class ScorecardSearchComponent {
  private type: string = '';
  playerClass: string = 'tab-primary';
  cardClass: string = 'tab-secondary';

  cardForm!: FormGroup;
  homeTeamMessage: String = '';
  awayTeamMessage: String = '';

  private validationMessages = {
    required: 'Please enter a name',
    minlength: 'The name must be at least 3 characters'
  }

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private router: Router) {
    this.cardClass = 'tab-primary'
    this.playerClass = 'tab-secondary'
  }

  ngOnInit() {

    this.cardForm = this.fb.group({
      homeTeam: ['', [Validators.required, Validators.minLength(3)]],
      homeTeamExactMatch: false,
      awayTeam: ['', [Validators.required, Validators.minLength(3)]],
      awayTeamExactMatch: false,
      venue: '0',
      startDate: '',
      endDate: '',
      matchType: 'wt',
      matchResult: '0',
    });

    this.route.params.subscribe(p => {
      this.type = p['type']
    });

    const homeTeam = this.cardForm.get('homeTeam')

    homeTeam?.valueChanges.subscribe(
      _ => this.setHomeTeamMessage(homeTeam)
    )

    const awayTeam = this.cardForm.get('awayTeam')

    awayTeam?.valueChanges.subscribe(
      _ => this.setAwayTeamMessage(awayTeam)
    )

  }

  find() {

    this.router.navigate(['/scorecardlist'], {
      queryParams: {
        homeTeam: this.cardForm.get('homeTeam')?.value
        , homeTeamExactMatch: this.cardForm.get('homeTeamExactMatch')?.value
        , awayTeam: this.cardForm.get('awayTeam')?.value
        , awayTeamExactMatch: this.cardForm.get('awayTeamExactMatch')?.value
        , venue: this.cardForm.get('venue')?.value
        , startDate: this.cardForm.get('startDate')?.value
        , endDate: this.cardForm.get('endDate')?.value
        , matchType: this.cardForm.get('matchType')?.value
        , matchResult: this.cardForm.get('matchResult')?.value
      }
    })
  }


  private setHomeTeamMessage(nameControl: AbstractControl): void {
    this.homeTeamMessage = ''

    if ((nameControl.touched || nameControl.dirty) && nameControl.errors) {
      this.homeTeamMessage = Object.keys(nameControl.errors).map(
        key => (this.validationMessages as any)[key]).join(' ');
    }
  }

  private setAwayTeamMessage(nameControl: AbstractControl): void {
    this.awayTeamMessage = ''

    if ((nameControl.touched || nameControl.dirty) && nameControl.errors) {
      this.awayTeamMessage = Object.keys(nameControl.errors).map(
        key => (this.validationMessages as any)[key]).join(' ');
    }
  }
}
