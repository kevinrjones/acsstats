import {Component, OnInit} from '@angular/core';
import {Store} from "@ngrx/store";
import {AppState} from "./models/app-state";
import {ErrorDetails} from "./models/error.model";
import {RaiseErrorAction} from "./actions/error.actions";
import {Observable} from "rxjs";
import {ToastrService} from "ngx-toastr";
import {ErrorLookupService} from "./services/error-lookup.service";
import {LoadingService} from "./services/loading.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ACS Cricket Records';
  loading: boolean = false;

  private errorState$: Observable<ErrorDetails>;
  private loadingState$: Observable<boolean>;

  constructor(private appStore: Store<AppState>,
              private toastr: ToastrService,
              private errorLookupService: ErrorLookupService,
              private _loading: LoadingService) {

    this.resetErrorState()

    this.errorState$ = this.appStore.select(s => s.errorState);
    this.loadingState$ = this.appStore.select(s => s.loading)
  }

  ngOnInit(): void {
    this.errorState$.subscribe(s => {
        var message: string
        if (s.id != 0) {
          if (s.message != null)
            message = this.errorLookupService.getErrorForCode(s.id) + '<br />' + s.message
          else
            message = this.errorLookupService.getErrorForCode(s.id)
          this.toastr.error(message, "Error")
          this.resetErrorState()
        }
      }
    );


    this.loadingState$.subscribe(state => {
      this.loading = state;
    });
  }

  resetErrorState() {
    this.appStore.dispatch(RaiseErrorAction({payload: {id: 0, message: null}}))
  }
}
