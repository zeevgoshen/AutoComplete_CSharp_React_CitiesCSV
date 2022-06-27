import React, { useMemo, useState, useEffect } from 'react';
import debounce from 'lodash.debounce';
import { useSuggestions } from '../services/suggestions.service'

import './AutoComplete.css'

const AutoComplete = () => {

    const [filterText, setFilter] = useState('');
    const [inputValue, setInputValue] = useState('');
    const [cities, setCities] = useSuggestions(filterText);

    const handleChange = (e) => {
        setInputValue();
        if (e.target.value) {
            setFilter(e.target.value.replace(/[^a-z]/gi, ''));
        }
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

