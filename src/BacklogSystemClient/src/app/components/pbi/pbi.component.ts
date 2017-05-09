import { Component, OnInit, Input } from '@angular/core';
import { ProductBacklogItem } from './../../model/productBacklogItem';

@Component({
  selector: 'app-pbi',
  templateUrl: './pbi.component.html',
  styleUrls: ['./pbi.component.less']
})
export class PbiComponent implements OnInit {

  @Input() pbi: ProductBacklogItem;
  
  constructor() { }

  ngOnInit() {
  }

}
