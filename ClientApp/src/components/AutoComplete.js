import React, { Component, useMemo, useState, useEffect } from 'react';
import axios from 'axios';
import './AutoComplete.css'
//import getFilteredCities from '../services/data.service'

const AutoComplete = () => {

    const [filterText, setFilter] = useState('');
    const [cities, setCities] = useState([]);


    const getFilteredCities = async () => {


        const options = {
            headers: { "content-type": "application/json" }
        }

        console.log("getFilteredCities1111");
        const data = await axios
            .post('suggestions', { "text": filterText }, options)
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };


    const citiesData = useEffect(() => {


        console.log("citiesData = useEffect" + cities);


    }, []);


    let filteredCities = useMemo(() => {
        // no search text in the search textbox
        // show all issues
        if (filterText.length !== 0) {
            getFilteredCities();
        }
    }, [filterText]);

    const selectText =(text) =>{
        console.log(text);
    }

    return <div className="suggestionList">
        <label>enter a city</label>
        <input type="text" onChange={(e) => {
            setFilter(e.target.value);
        }} />
        <ul>
            {cities && cities.map((city) => <li key={city.id} onClick={(e) => e.target.value}>{city.cityName}</li>)}
        </ul>

    </div>
}

export default AutoComplete;

