import {Component} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {AbstractControl, FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-player-search',
  templateUrl: './playersearch.component.html',
  styleUrls: ['./playersearch.component.css']
})
export class PlayerSearchComponent {
  private type: string = '';
  playerClass: string = 'tab-primary';
  cardClass: string = 'tab-secondary';

  playerForm!: FormGroup;
  nameMessage: String = '';

  private validationMessages = {
    required: 'Please enter a name',
    minlength: 'The name must be at least 3 characters'
  }

  constructor(private route: ActivatedRoute, private fb: FormBuilder, private router: Router) {
  }

  ngOnInit() {

    this.playerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      exactMatch: false,
      team: '',
      startDate: '',
      endDate: '',
    });

    this.route.params.subscribe(p => {
      this.type = p['type']
    });

    const nameControl = this.playerForm.get('name')

    nameControl?.valueChanges.subscribe(
      _ => this.setMessage(nameControl)
    )

  }

  find() {

    this.router.navigate(['/playerlist'], {
      queryParams: {
        name: this.playerForm.get('name')?.value
        , exactMatch: this.playerForm.get('exactMatch')?.value
        , team: this.playerForm.get('team')?.value
        , startDate: this.playerForm.get('startDate')?.value
        , endDate: this.playerForm.get('endDate')?.value
      }
    })
  }


  private setMessage(nameControl: AbstractControl): void {
    this.nameMessage = ''

    if ((nameControl.touched || nameControl.dirty) && nameControl.errors) {
      this.nameMessage = Object.keys(nameControl.errors).map(
        key => (this.validationMessages as any)[key]).join(' ');
    }
  }
}
