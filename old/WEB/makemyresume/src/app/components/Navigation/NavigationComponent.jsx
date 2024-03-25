import { useMsal } from "@azure/msal-react";
import { Link } from "react-router-dom";
import { loginRequest } from "../../msal/msalConfig";

export function NavigationComponent() {
  const { instance } = useMsal();

  function handleLogin(event) {
    instance.loginPopup(loginRequest).catch(e => {
      console.log(e);
    });
  }
  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
      <div className="container-fluid">
        <Link className="navbar-brand" to="/">Make My Resume</Link>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navBarMain" aria-controls="navBarMain" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navBarMain">
          <ul className="navbar-nav me-auto mb-2 mb-lg-0">
            <li className="nav-item">
              <Link className="nav-link" aria-current="page" to="/">Home</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/about">About</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/authtest">Auth Test</Link>
            </li>
          </ul>
          <form className="d-flex" role="login">
            <button className="btn btn-outline-success" type="button" onClick={handleLogin}>Login</button>
          </form>
        </div>
      </div>
    </nav>
  );
}