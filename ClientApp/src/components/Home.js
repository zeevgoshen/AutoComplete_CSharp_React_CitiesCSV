import React, { Component, useEffect, useState } from 'react';
import AutoComplete from './AutoComplete'
import axios from "axios";

export default function Home() {

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
     

    return (
        <div>
            
            <h1 id="tabelLabel">SailPoint Cities Autocomplete</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {/*{contents}*/}
            <AutoComplete />
        </div>
    );
     
}
