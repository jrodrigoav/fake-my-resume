import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironmentConfig, ENV_CONFIG } from '../../interfaces/environment-config';
import { map, Observable } from 'rxjs';
import { TechTag } from 'src/app/DTOs/TechTag';


@Injectable({
  providedIn: 'root'
})
export class TechTagsService {

  private baseUrl: string;

  constructor(@Inject(ENV_CONFIG) private config: IEnvironmentConfig, private http: HttpClient) {
    this.baseUrl = config.techTagsUrl.replace(/\/+$/, '');
  }

  getAllTechTags(): Observable<TechTag[]> {
    return this.http.get<TechTag[]>(this.baseUrl);
  }

  searchTechTags(tagName: string): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl+'/'+tagName);
  }

}
