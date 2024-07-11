import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResumeDTO } from '../../DTOs/ResumeDTO';
import { Observable } from 'rxjs/internal/Observable';
import { INote } from '../../interfaces/inote';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MakeMyResumeService {
  baseUrl: string;

  constructor(private httpClient: HttpClient) {
    this.baseUrl = environment.apiUrl;
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
    return this.httpClient.put(this.resourceUrl(`resume/${id}`), resume, { observe: "response" });
  }

  public fetchNotes(): Observable<INote[]> {
    return this.httpClient.get<INote[]>(this.resourceUrl("notes"));
  }

  public createNote(note: INote): Observable<INote> {
    return this.httpClient.post<INote>(this.resourceUrl("notes"), note);
  }

  public updateNote(note: INote) {
    return this.httpClient.put(this.resourceUrl("notes"), note);
  }

  public deleteNote(note: INote) {
    return this.httpClient.delete(this.resourceUrl("notes"), { body: note });
  }

  searchTechTags(tagName: string): Observable<string[]> {
    const searchParams = { text: tagName };
    return this.httpClient.get<string[]>(this.resourceUrl("tag"), { params: searchParams });//TODO change this to tag
  }

  checkFormatAndSuggestions(value: string) {
    return this.httpClient.post<string>(this.resourceUrl('text/format'), { value });
  }
}
