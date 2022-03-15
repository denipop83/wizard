import {TestBed} from '@angular/core/testing';

import {BackendClient} from './backend.service';

describe('DataService', () => {
  let service: BackendClient;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BackendClient);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
