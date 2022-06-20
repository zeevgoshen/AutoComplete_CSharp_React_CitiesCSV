import React, { Component, useMemo, useState } from 'react';
import axios from "axios";

const AutoComplete = ({loadIssues,fullData}) => {

    const [filterText, setFilter] = useState('');
    const [cities, setCities] = useState([]);

    const getFilteredCities = async () => {

        //const formData = new FormData();

        //let mockData = [];
        //mockData.push({ "text": filterText });

        //formData.append('model', JSON.stringify(mockData));
        const options = {
            headers: { "content-type": "application/json" }
        }

        console.log(filterText);
        const data = await axios
            .post('suggestions', { "text": filterText }, options  )
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };

     


    let filteredCities = useMemo(() => {
        // no search text in the search textbox
        // show all issues
        if (filterText.length !== 0) {

            getFilteredCities(filterText);
        }

        console.log(filterText);

        return (
            <div>aaaa</div>
        )
     
    }, [filterText]);


    return <div style={{ border: "1px solid red" }}>
        <label>enter a city</label>
        <input type="text" onChange={(e) => {
            setFilter(e.target.value);
        }}/>
    </div>
}

export default AutoComplete;

