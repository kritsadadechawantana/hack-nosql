import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PbuycoinPage } from './pbuycoin.page';

describe('PbuycoinPage', () => {
  let component: PbuycoinPage;
  let fixture: ComponentFixture<PbuycoinPage>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PbuycoinPage ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PbuycoinPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
