import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';


@Injectable()
export class AclCategoryService {
  query(pageIndex: number = 1, pageSize: number = 10, key: string): Observable<any> {
    let params = new HttpParams().
      append('pageindex', `${pageIndex}`)
      .append('pagesize', `${pageSize}`);
    if (key) { params = params.append('key', `${key}`); }
    return this.http.get(webApiUrls.aclCategorys.query, { params });
  }

  update(aclCategory: any): Observable<any> {
    return this.http.put(format(webApiUrls.aclCategorys.update, { id: aclCategory.id }), aclCategory);
  }

  add(aclCategory: any): Observable<any> {
    return this.http.post(webApiUrls.aclCategorys.add, aclCategory);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(format(webApiUrls.aclCategorys.delete, { id: id }));
  }


  constructor(private http: HttpClient) { }

}
