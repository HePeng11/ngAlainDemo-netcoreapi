import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';
import { http_null_param } from './consts';


@Injectable()
export class UserService {
  query(pageIndex: number = 1, pageSize: number = 10, key: string, gender: string): Observable<any> {
    let params = new HttpParams().
      append('pageindex', `${pageIndex}`)
      .append('pagesize', `${pageSize}`);
    if (key) { params = params.append('key', `${key}`); }
    if (gender && gender !== http_null_param) { params = params.append('gender', `${gender}`); }
    return this.http.get(webApiUrls.users.query, { params });
  }

  update(user: any): Observable<any> {
    return this.http.put(format(webApiUrls.users.update, { id: user.id }), user);
  }

  add(user: any): Observable<any> {
    return this.http.post(webApiUrls.users.add, user);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(format(webApiUrls.users.delete, { id: id }));
  }

  updatePassword(id: number, password: string): Observable<any> {
    return this.http.put(format(webApiUrls.users.updatePassword, { id: id }), '"' + password + '"',
      { headers: new HttpHeaders().append('Content-Type', 'application/json') });
  }

  constructor(private http: HttpClient) { }



}
