import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';


@Injectable()
export class ObjectAclService {

  query(objectId: number = 0, objectType: number = 1): Observable<any> {
    const params = new HttpParams().
      append('objectId', `${objectId}`)
      .append('objectType', `${objectType}`);
    return this.http.get(webApiUrls.objectAcls.query, { params });
  }

  queryAclNames(): Observable<any> {
    return this.http.get(webApiUrls.objectAcls.queryAclNames);
  }

  update(acl: any): Observable<any> {
    return this.http.put(webApiUrls.objectAcls.update, acl);
  }

  constructor(private http: HttpClient) { }

}
