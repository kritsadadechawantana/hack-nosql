import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PsalecoinPage } from './psalecoin.page';

describe('PsalecoinPage', () => {
  let component: PsalecoinPage;
  let fixture: ComponentFixture<PsalecoinPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PsalecoinPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PsalecoinPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
