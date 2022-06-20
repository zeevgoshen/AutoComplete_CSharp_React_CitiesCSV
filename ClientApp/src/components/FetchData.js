import React, { Component, useEffect, useState } from 'react';
import AutoComplete from './AutoComplete'
import axios from "axios";

export default function FetchData() {

    const [cities, setCities] = useState([]);

    const getAllCities = async () => {

        const data = await axios
            .get('cities')
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };

    const citiesData = useEffect(() => {
        getAllCities();
    }, []);
    

    const renderForecastsTable = (forecasts) => {

        console.log("renderForecastsTable");
        
        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Date</th>
                            <th>Temp. (C)</th>
                            <th>Temp. (F)</th>
                            <th>Summary</th>
                        </tr>
                    </thead>
                    <tbody>

                        {forecasts && forecasts.map(forecast =>
                            <tr key={forecast.id}>
                                <td>{forecast.cityName}</td>
                                <td>{forecast.id}</td>
                                {/*<td>{forecast.temperatureF}</td>*/}
                                {/*<td>{forecast.summary}</td>*/}
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    //let contents = renderForecastsTable();
    

    return (
        <div>
            
            <h1 id="tabelLabel">Cities Autocomplete</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {/*{contents}*/}
            <AutoComplete loadIssues={setCities} fullData={cities}/>
        </div>
    );
     
}
