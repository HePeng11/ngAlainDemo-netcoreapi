import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';


@Injectable()
export class AclService {

  update(acl: any): Observable<any> {
    return this.http.put(format(webApiUrls.acls.update, { id: acl.id }), acl);
  }

  add(acl: any): Observable<any> {
    return this.http.post(webApiUrls.acls.add, acl);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(format(webApiUrls.acls.delete, { id: id }));
  }

  constructor(private http: HttpClient) { }

}
