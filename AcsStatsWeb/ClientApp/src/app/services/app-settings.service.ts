import {Injectable} from "@angular/core";

@Injectable({providedIn: 'root'})
export class AppSettingsService {

   private readonly defaultMatchType = "itt"

  getDefaultMatchType() {
    return this.defaultMatchType;
  }
}
