import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-records-header',
  templateUrl: './records-header.component.html',
  styleUrls: ['./records-header.component.css']
})
export class RecordsHeaderComponent implements OnInit {

  @Input() name!: string;

  battingClass: string
  bowlingClass: string
  fieldingClass: string
  teamClass: string
  partnershipClass: string
  umpiresClass: string

  constructor() {
    this.battingClass = "inactive"
    this.bowlingClass = "inactive"
    this.fieldingClass = "inactive"
    this.teamClass = "inactive"
    this.partnershipClass = "inactive"
    this.umpiresClass = "inactive"
  }

  ngOnInit(): void {

    if (this.name == "batting") {
      this.battingClass = "active"
    } else if (this.name == "bowling") {
      this.bowlingClass = "active"
    }


  }

}
