import React, { Component, useMemo, useState, useEffect } from 'react';
import axios from 'axios';
import './AutoComplete.css'

const AutoComplete = () => {

    const [filterText, setFilter] = useState('');
    const [cities, setCities] = useState([]);

    const [inputValue, setInputValue] = useState([]);
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



    const selectText = (text) => {
        setInputValue(text);
    }

    return <div className="suggestionList">
        <div className="suggestionLabels">
            <label className="suggestionLabels">Start typing a city name...</label>
            <div className="selectedValue">{filterText.length > 0 ? inputValue : ""}</div>
            <input type="text" onChange={(e) => {
                setFilter(e.target.value);
            }} />
        </div>
        <ul>
            {filterText.length > 0 && cities && cities.map((city) => <li key={city.id} onClick={(e) => selectText(city.cityName)}>{city.cityName}</li>)}
        </ul>

    </div>
}

export default AutoComplete;

