import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResumeDTO } from 'src/app/DTOs/ResumeDTO';
import { ENV_CONFIG, IEnvironmentConfig } from '../../interfaces/environment-config';

@Injectable({
  providedIn: 'root'
})

export class MakeMyResumeService {

  baseUrl: string;

  constructor(@Inject(ENV_CONFIG) private config: IEnvironmentConfig, private httpClient: HttpClient) {
    this.baseUrl = config.makeMyResumeApiUrl;
  }

  private resourceUrl(resource: string) {
    return `${this.baseUrl}/${resource}`;
  }

  public dummyResume() {
    return this.httpClient.get(this.resourceUrl('dummy'), {
      observe: 'response',
      responseType: 'blob'
    });
  }

  public downloadResume(id: string) {
    return this.httpClient.get(this.resourceUrl('download/' + id), {
      observe: 'response',
      responseType: 'blob'
    });
  }

  public getResume(id: string) {
    return this.httpClient.get(this.resourceUrl(id));
  }

  public saveResume(resume: ResumeDTO) {
    return this.httpClient.post(this.resourceUrl(''), resume);
  }

}
