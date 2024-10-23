import { TestBed } from '@angular/core/testing';

import { RevisoesService } from './revisoes.service';

describe('RevisoesCriarService', () => {
  let service: RevisoesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RevisoesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
