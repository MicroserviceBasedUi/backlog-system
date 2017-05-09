import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Pbi } from './../pbi/pbi.component';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class BacklogService {

  constructor(private http: Http) { }

  public FetchOpenPbis(): Promise<Pbi[]> {
    return this.http.get("http://localhost:4200/api/backlog/remaining")
                          .toPromise()
                          .then(response => response.json().issues.map(item => {
                              return {
                                id: item.key,
                                summary: item.fields.summary
                              };
                            }));
  }
}
