import { InjectionToken } from "@angular/core";
import { environment } from "../../environments/environment";

// environment-config.ts
export interface IEnvironmentConfig {
    production: boolean;
    unicornRewardsApiUrl: string;
    makeMyResumeApiUrl: string;
    techTagsUrl: string;
}

export const ENV_CONFIG = new InjectionToken<IEnvironmentConfig>('EnvironmentConfig');
export const provideEnvironment = () => ({ provide: ENV_CONFIG, useValue: environment });