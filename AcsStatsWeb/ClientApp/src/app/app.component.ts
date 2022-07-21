import {Component, OnInit} from '@angular/core';
import {createSelector, Store} from "@ngrx/store";
import {AppState} from "./models/app-state";
import {ErrorDetails} from "./models/error.model";
import {RaiseErrorAction} from "./actions/error.actions";
import {Observable} from "rxjs";
import {ToastrService} from "ngx-toastr";
import {ErrorLookupService} from "./services/error-lookup.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';

  private errorState$: Observable<ErrorDetails>;

  constructor(private appStore: Store<AppState>, private toastr: ToastrService, private errorLookupService: ErrorLookupService) {

    this.resetErrorState()

    this.errorState$ = this.appStore.select(s => s.errorState);
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
  }

  resetErrorState() {
    this.appStore.dispatch(RaiseErrorAction({payload: {id: 0, message: null}}))
  }
}
