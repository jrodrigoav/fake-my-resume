import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IEnvironmentConfig, ENV_CONFIG } from '../../interfaces/environment-config';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TechTagsService {
  private baseUrl: string;

  constructor(@Inject(ENV_CONFIG) config: IEnvironmentConfig, private http: HttpClient) {
    this.baseUrl = config.techTagsUrl.replace(/\/+$/, '');
  }

  searchTechTags(tagName: string): Observable<string[]> {
    const searchParams = { text: tagName };
    return this.http.get<string[]>(this.baseUrl, { params: searchParams });
  }
}
