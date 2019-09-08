import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders } from '@angular/common/http';


@Injectable()
export class LoginService {
  constructor(
    public http: _HttpClient,
  ) {

  }

  login(loginname: string, password: string): Observable<any> {
    return this.http
      .post(webApiUrls.users.login, {
        loginname: loginname,
        password: password,
        _allow_anonymous: true
      });
  }


}
