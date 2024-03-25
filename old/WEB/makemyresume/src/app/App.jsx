import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import { NavigationComponent } from "./components/Navigation/NavigationComponent";
import { AboutPage } from "./pages/About/AboutPage";
import { HomePage } from "./pages/Home/HomePage";
import { msalConfig } from "./msal/msalConfig";
import { PublicClientApplication } from "@azure/msal-browser";
import { MsalProvider } from "@azure/msal-react";
import { AuthenticatedTemplate, UnauthenticatedTemplate, useMsal } from "@azure/msal-react";
import React from "react";

const msalInstance = new PublicClientApplication(msalConfig);

export function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<HomePage />} />
                    <Route path="about" element={<AboutPage />} />
                    <Route path="authtest" element={
                        <React.Fragment>
                            <AuthenticatedTemplate>
                                <h5 className="card-title">Authenticated</h5>
                            </AuthenticatedTemplate>
                            <UnauthenticatedTemplate>
                                <h5 className="card-title">Please sign-in to see your profile information.</h5>
                            </UnauthenticatedTemplate>
                        </React.Fragment>
                    }/>
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

function Layout() {
    return (
        <React.StrictMode>
            <MsalProvider instance={msalInstance}>
                <div className="container-fluid">
                    <NavigationComponent />
                    <Outlet />
                </div>
            </MsalProvider>
        </React.StrictMode>
    )
}