import { Component, OnInit } from '@angular/core';
import { BacklogService } from './backlog.service';

@Component({
  selector: 'app-backlog',
  templateUrl: './backlog.component.html',
  styleUrls: ['./backlog.component.less'],
  providers: [BacklogService]
})
export class BacklogComponent implements OnInit {

  public pbis: object[] = [];

  constructor(private backlogService: BacklogService) { }

  ngOnInit() {
    this.backlogService.FetchOpenPbis().then(data => this.pbis = data);
  }

}
