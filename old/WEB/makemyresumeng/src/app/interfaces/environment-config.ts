import { InjectionToken } from "@angular/core";

// environment-config.ts
export interface IEnvironmentConfig {
    production: boolean,
    typicodeUrl: string,
    unicornRewardsApiUrl: string,
    makeMyResumeApiUrl: string,
    techTagsUrl: string
}

export const ENV_CONFIG = new InjectionToken<IEnvironmentConfig>('EnvironmentConfig');