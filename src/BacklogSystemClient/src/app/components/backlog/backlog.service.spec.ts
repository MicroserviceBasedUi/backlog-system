import { TestBed, inject } from '@angular/core/testing';

import { BacklogService } from './backlog.service';

describe('BacklogService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BacklogService]
    });
  });

  it('should ...', inject([BacklogService], (service: BacklogService) => {
    expect(service).toBeTruthy();
  }));
});
