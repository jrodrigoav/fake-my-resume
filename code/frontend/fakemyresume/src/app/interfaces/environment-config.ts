import { InjectionToken } from "@angular/core";
import { environment } from "../../environments/environment";

// environment-config.ts
export interface IEnvironmentConfig {
    production: boolean;
    apiUrl: string;
    msalAuth:{
        clientId: string;
        authority: string;
        redirectUri: string;
        postLogoutRedirectUri: string;
    }
}

export const ENV_CONFIG = new InjectionToken<IEnvironmentConfig>('EnvironmentConfig');
export const provideEnvironment = () => ({ provide: ENV_CONFIG, useValue: environment });