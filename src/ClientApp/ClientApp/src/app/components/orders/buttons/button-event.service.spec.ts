import { TestBed } from '@angular/core/testing';

import { ButtonEventService } from './button-event.service';

describe('ButtonEventService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ButtonEventService = TestBed.get(ButtonEventService);
    expect(service).toBeTruthy();
  });
});
