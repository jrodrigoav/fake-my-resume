// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
    production: false,
    unicornRewardsApiUrl: "https://localhost:7127/api",
    makeMyResumeApiUrl: "https://localhost:7127/api/resume",
    techTagsUrl: "https://localhost:7127/api/aditional-skills",
    msalAuth:{
        clientId: "4186cd96-2142-430d-bfd3-2ff7b64a2c05",
        authority: "https://login.microsoftonline.com/eedd1340-df1a-4db2-8a03-b4cfb1fa3e9d",
        redirectUri: '/',
        postLogoutRedirectUri: '/'
    }
};

/*
* For easier debugging in development mode, you can import the following file
* to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
*
* This import should be commented out in production mode because it will have a negative impact
* on performance if an error is thrown.
*/
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
  