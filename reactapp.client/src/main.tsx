
import ReactDOM from 'react-dom/client';
import { BrowserRouter as BrowserRouter } from 'react-router-dom';
import App from './App.tsx';
import { PageTitleProvider } from './components/PageTitleContext/PageTitleContext.tsx';
import './index.css';

ReactDOM.createRoot(document.getElementById('root')!).render(
      <PageTitleProvider>
            <BrowserRouter>
                <App />
            </BrowserRouter>
        </PageTitleProvider>
);
