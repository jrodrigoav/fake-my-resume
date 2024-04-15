import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ENV_CONFIG, IEnvironmentConfig } from '../../interfaces/environment-config';
import { ResumeDTO } from '../../DTOs/ResumeDTO';

@Injectable({
  providedIn: 'root'
})
export class MakeMyResumeService {
  baseUrl: string;

  constructor(@Inject(ENV_CONFIG) config: IEnvironmentConfig, private httpClient: HttpClient) {
    this.baseUrl = config.makeMyResumeApiUrl;
  }

  private resourceUrl(resource: string) {
    return `${this.baseUrl}/${resource}`;
  }

  public getResume(id: string) {
    return this.httpClient.get(this.resourceUrl(id));
  }

  public saveResume(resume: ResumeDTO) {
    return this.httpClient.post(this.resourceUrl(''), resume);
  }
}
