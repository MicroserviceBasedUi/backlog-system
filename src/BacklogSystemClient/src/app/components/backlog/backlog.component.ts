import { Component, OnInit } from '@angular/core';
import { BacklogService } from './backlog.service';
import { ProductBacklogItem } from './../../model/productBacklogItem';

@Component({
  selector: 'app-backlog',
  templateUrl: './backlog.component.html',
  styleUrls: ['./backlog.component.less'],
  providers: [BacklogService]
})
export class BacklogComponent implements OnInit {

  public pbis: ProductBacklogItem[] = [];

  constructor(private backlogService: BacklogService) { }

  ngOnInit() {
    this.backlogService.FetchOpenPbis().then(data => this.pbis = data);
  }

}
