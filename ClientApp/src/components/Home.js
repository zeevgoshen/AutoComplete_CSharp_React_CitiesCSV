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
            <label className="secondaryTitle">1. The search is not case sensitive.</label>
            <label className="secondaryTitle">2. The search currently is using
                de-bounce with a time interval of such as 300ms to reduce API calls.</label>
            <label className="secondaryTitle">3. See the github readme for more details.</label>
            <AutoComplete />
        </div>
    );
     
}
