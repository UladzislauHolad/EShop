import { TestBed } from '@angular/core/testing';

import { LogginEventService } from './loggin-event.service';

describe('LogginEventService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: LogginEventService = TestBed.get(LogginEventService);
    expect(service).toBeTruthy();
  });
});
