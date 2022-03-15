import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginInfoComponent } from './login.component';

describe('LoginComponentComponent', () => {
  let component: LoginInfoComponent;
  let fixture: ComponentFixture<LoginInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LoginInfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
