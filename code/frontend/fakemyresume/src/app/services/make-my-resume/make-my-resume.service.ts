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
    this.baseUrl = config.apiUrl;
  }

  private resourceUrl(resource: string) {
    return `${this.baseUrl}/${resource}`;
  }

  public getResumes() {
    return this.httpClient.get<ResumeDTO[]>(this.resourceUrl("resume"));
  }

  public getResume(id: string) {
    return this.httpClient.get<ResumeDTO>(this.resourceUrl(`resume/${id}`));
  }

  public getResumePdF(resumeId: number) {
    return this.httpClient.get<Blob>(this.resourceUrl(`resume/${resumeId}/pdf`), { responseType: 'blob' as 'json' });
  }

  public saveResume(resume: ResumeDTO) {
    return this.httpClient.post<ResumeDTO>(this.resourceUrl('resume'), resume);
  }
  
  public updateResume(id: number, resume: ResumeDTO) {
    return this.httpClient.put(this.resourceUrl(`${id}`), resume, { observe: "response" });
  }
}
