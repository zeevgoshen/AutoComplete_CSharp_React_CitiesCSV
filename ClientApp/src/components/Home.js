import React, { useEffect, useState } from 'react';
import AutoComplete from './AutoComplete'
import './AutoComplete.css'
import axios from "axios";

export default function Home() {

    const [setCities] = useState([]);

    const getAllCities = async () => {

        await axios
            .get('cities')
            .then((response) => response.data)
            .catch((err) => console.log(err));
    };

    useEffect(() => {
        getAllCities();
    }, []);
     

    return (
        <div className="main_content">

            <label className="mainTitle">SailPoint Cities Autocomplete</label>
            <label className="secondaryTitle">The search is not case sensitive.</label>
            <AutoComplete />
        </div>
    );
     
}
