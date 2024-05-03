import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ENV_CONFIG, IEnvironmentConfig } from '../../interfaces/environment-config';

@Injectable({
  providedIn: 'root'
})
export class TextService {
  baseUrl: string;

  constructor(@Inject(ENV_CONFIG) config: IEnvironmentConfig, private httpClient: HttpClient) {
    this.baseUrl = config.apiUrl;
  }

  private resourceUrl(resource: string) {
    return `${this.baseUrl}/text/${resource}`;
  }

  checkFormatAndSuggestions(value: string) {
    return this.httpClient.post<string>(this.resourceUrl('format'), { value });
  }
}

