import { useEffect, useState } from 'react';
//import { BrowserRouter as Router, Route, Switch, Link } from 'react-router-dom';
//import { Route } from 'react-router-dom';
import './Home.css';

import { usePageTitle } from '../PageTitleContext/PageTitleContext';

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

const Home: React.FC = () => {


    const [forecasts, setForecasts] = useState<Forecast[]>();

    useEffect(() => {
        populateWeatherData();
    }, []);
    const { setPageTitle } = usePageTitle();

    useEffect(() => {
        // Update the page title when the component mounts
        setPageTitle('Homr Page');
    }, [setPageTitle]);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div>

            

            <table className="table table-striped" aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
            <button id="tabelLabel" onClick={populateWeatherData}>
                Refresh Weather Data
            </button>
        </div>;

    return (<div>

        <h1 id="tabelLabel">Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>

        {contents}
    </div>
    );
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }
}

export default Home;