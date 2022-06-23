import React, { useMemo, useState, useEffect } from 'react';
import axios from 'axios';
import debounce from 'lodash.debounce';

import './AutoComplete.css'

const AutoComplete = () => {

    const [filterText, setFilter] = useState('');
    const [cities, setCities] = useState([]);

    const [inputValue, setInputValue] = useState('');


    const getFilteredCities = async () => {

        const options = {
            headers: { "content-type": "application/json" }
        }

        await axios
            .post('suggestions', { "text": filterText }, options)
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };

    useMemo(() => {
        // no search text in the search textbox
        // show all issues
        if (filterText.length !== 0) {
            getFilteredCities();
        }
    }, [filterText]);


    const handleChange = (e) => {
        setInputValue();
        setFilter(e.target.value);
    };

    const debouncedResults = useMemo(() => {
        return debounce(handleChange, 150);
    }, []);

    useEffect(() => {
        return () => {
            debouncedResults.cancel();
        };
    });


    const selectText = (text) => {
        setInputValue(text);
    }

    return <div className="suggestionList">
        <div className="suggestionLabels">
            <label className="suggestionLabels">Start typing a city name...</label>
            <div className="selectedValue">{filterText.length > 0 ? inputValue : ' '}</div>

            <input className="autoCompleteInput" type="text" onChange={debouncedResults} />
        </div>
        <ul>
            {filterText.length > 0 && cities && cities.map((city) => <li key={city.id} onClick={(e) => selectText(city.cityName)}>{city.cityName}</li>)}
        </ul>

    </div>
}

export default AutoComplete;

