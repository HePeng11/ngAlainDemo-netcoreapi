import { Component } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { Router } from '@angular/router';

@Component({
  selector: 'exception-trigger',
  template: `
  <div class="pt-lg">
    <nz-card>
      <button *ngFor="let t of types" (click)="go(t)" nz-button nzType="danger">触发{{t}}</button>
    </nz-card>
  </div>
  `
})
export class ExceptionTriggerComponent {
  // types = [401, 403, 404, 500];
  types = [403, 404, 500];

  constructor(private http: _HttpClient, private router: Router) { }

  go(type: number) {
    this.router.navigateByUrl(`/exception/${type}`);
  }
}
