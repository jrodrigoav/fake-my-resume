import { createRoot } from 'react-dom/client';
import { App } from "./app/App";
const container = document.getElementById('make-my-resume-app');
const root = createRoot(container); // createRoot(container!) if you use TypeScript
root.render(<App/>);