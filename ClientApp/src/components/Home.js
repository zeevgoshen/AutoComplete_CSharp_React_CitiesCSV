import React, { Component, useEffect, useState } from 'react';
import AutoComplete from './AutoComplete'
import './AutoComplete.css'
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
        <div className="main_content">

            <label className="mainTitle">SailPoint Cities Autocomplete</label>
            <label className="secondaryTitle">The search is Case Sensitive.</label>
            {/*{contents}*/}
            <AutoComplete />
        </div>
    );
     
}
