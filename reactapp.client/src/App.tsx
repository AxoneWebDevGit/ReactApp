import './App.css';
import { Routes, Route, Link, useNavigate } from 'react-router-dom';
import Home from './components/Home/Home';
import ContactUs from './components/ContactUs/ContactUs';
import AboutUs from './components/AboutUs/AboutUs';

function App() {
    const navigate = useNavigate();

    // Function to update the page title based on the link
    const updatePageTitle = () => {
        console.log(`My App - ${window.location.pathname.replace('/', '')}`);
        //document.title = `My App - ${window.location.pathname.replace('/', '')}`;
    };
    return (
        <div className="app">
            <nav>
                <ul>
                    <li><Link to="/" onClick={() => { navigate('/'); updatePageTitle(); }}>Home</Link></li>
                    <li><Link to="/AboutUs" onClick={() => { navigate('/AboutUs'); updatePageTitle(); }}>About Us</Link></li>
                    <li><Link to="/ContactUs" onClick={() => { navigate('/ContactUs'); updatePageTitle(); }}>Contact Us</Link></li>
                </ul>
            </nav>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/AboutUs" element={<AboutUs />} />
                <Route path="/ContactUs" element={<ContactUs />} />
            </Routes>
        </div>
    );
}
export default App;
