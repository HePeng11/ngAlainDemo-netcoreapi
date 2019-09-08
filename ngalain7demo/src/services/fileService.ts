import { Injectable } from '@angular/core';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { webApiUrls } from './webApiUrls';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { format } from '@shared';


@Injectable()
export class FileService {

  saveRelation(fileId: number, businessId: number): Observable<any> {
    return this.http.post(format(webApiUrls.files.saveRelation, { fileId, businessId }), null);
  }

  constructor(private http: HttpClient) { }



}
