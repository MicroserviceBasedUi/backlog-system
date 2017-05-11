import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { ProductBacklogItem } from './../../model/productBacklogItem';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class BacklogService {

  constructor(private http: Http) { }

  public FetchOpenPbis(): Promise<ProductBacklogItem[]> {
    return this.http.get("http://localhost:4200/api/backlog/remaining")
                          .toPromise()
                          .then(response => response.json().issues.map(item => {
                              let pbi: ProductBacklogItem = {
                                id: item.key,
                                summary: item.fields.summary,
                                status: {
                                  name: item.fields.status.statusCategory.name
                                }
                              };

                              return pbi;
                            }));
  }
}
