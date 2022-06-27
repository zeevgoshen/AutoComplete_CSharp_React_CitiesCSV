import { useState, useEffect } from "react";
import axios from 'axios'

export function useSuggestions(filterText) {


    const [cities, setCities] = useState([]);

    const getFilteredCities = async () => {

        const options = {
            headers: { "content-type": "application/json" }
        }

        await axios
            .post('suggestions', { "text": filterText }, options)
            .then((response) => setCities(response.data))
            .catch((err) => console.log(err));
    };

    useEffect(() => {
         
        getFilteredCities().then((response) => {
            setCities(response.data);
        }).catch((e) => {
            //console.log(e.message);
        });
    }, [filterText]);

    return [cities, setCities];
};


