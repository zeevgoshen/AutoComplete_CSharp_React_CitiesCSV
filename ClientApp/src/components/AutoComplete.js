import React, { Component, useMemo, useState, useEffect } from 'react';
import axios from 'axios';
import './AutoComplete.css'

const AutoComplete = () => {

    const [filterText, setFilter] = useState('');
    const [cities, setCities] = useState([]);

    const getFilteredCities = async () => {

        const options = {
            headers: { "content-type": "application/json" }
        }

        const data = await axios
            .post('suggestions', { "text": filterText }, options)
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };

    let filteredCities = useMemo(() => {
        // no search text in the search textbox
        // show all issues
        if (filterText.length !== 0) {
            getFilteredCities();
        }
    }, [filterText]);


    let inputValue = "";
    const selectText =(text) =>{
        console.log(text);
        inputValue = text;
        //setFilter(text);
        
    }

    return <div className="suggestionList">
                <label>Start typing a city name...</label>
        <input type="text" onChange={(e) => {
                    setFilter(e.target.value);
                }} />
                <ul>
            {cities && cities.map((city) => <li key={city.id} onClick={(e) => selectText(city.cityName)}>{city.cityName}</li>)}
                </ul>

    </div>
}

export default AutoComplete;

