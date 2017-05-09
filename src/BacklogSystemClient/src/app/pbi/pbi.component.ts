import { Component, OnInit, Input } from '@angular/core';


@Component({
  selector: 'app-pbi',
  templateUrl: './pbi.component.html',
  styleUrls: ['./pbi.component.less']
})
export class PbiComponent implements OnInit {

  @Input() pbi: object;
  
  constructor() { }

  ngOnInit() {
  }

}

export interface Pbi {
  id: string;
  summary : string;
}
