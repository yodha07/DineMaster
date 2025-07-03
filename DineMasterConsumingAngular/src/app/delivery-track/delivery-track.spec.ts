import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryTrack } from './delivery-track';

describe('DeliveryTrack', () => {
  let component: DeliveryTrack;
  let fixture: ComponentFixture<DeliveryTrack>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeliveryTrack]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeliveryTrack);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
