import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ENV_CONFIG, IEnvironmentConfig } from '../../interfaces/environment-config';
import { User } from '../../DTOs/User';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseUrl: string;
  private readonly baseUrlUs: string;
  private _currentUser: Subject<User | null> = new Subject<User | null>();
  currentUserChanges: Observable<User | null>;
  currentUser: User | null = null;

  constructor(@Inject(ENV_CONFIG) config: IEnvironmentConfig, private httpClient: HttpClient) {
    this.baseUrl = `${config.apiUrl}/user`;
    this.baseUrlUs = `${config.apiUrl}`;
    this.currentUserChanges = this._currentUser.asObservable();
    this._currentUser.subscribe(user => {
      this.currentUser = user;
    })
  }

  private resourceUrl(resource: string) {
    return `${this.baseUrl}/${resource}`;
  }

  public loadCurrentUser() {
    this.httpClient.get<User>(`${this.baseUrlUs}/resume/user`, { observe: 'response' }).subscribe((response) => {
      if(response.status >= 200 && response.status <= 299) {
        this._currentUser.next(response.body);
      } else {
        this._currentUser.next(null);
      }
    });
  }
}
