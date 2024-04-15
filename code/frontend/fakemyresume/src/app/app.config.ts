import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { provideEnvironment } from './interfaces/environment-config';
import { provideNativeDateAdapter } from '@angular/material/core';
import { MsalBroadcastService, MsalGuard, MsalInterceptor, MsalService } from "@azure/msal-angular";
import { provideMsalInstance, provideMsalGuardConfig, provideMsalInterceptorConfig } from './msal-providers';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideAnimationsAsync(),
    provideHttpClient(withInterceptorsFromDi()),
    provideEnvironment(),
    provideNativeDateAdapter(),
    provideMsalInstance(),
    provideMsalGuardConfig(),
    provideMsalInterceptorConfig(),
    { provide: HTTP_INTERCEPTORS, useClass: MsalInterceptor, multi: true },
    MsalService,
    MsalGuard,
    MsalBroadcastService,
  ],
};
