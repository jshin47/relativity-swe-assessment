import axios from 'axios';

const SHOW_API_BASE_URL = "http://localhost:9080/api/v1/shows";

class ShowService {

    getShows(pageIndex){
        return axios.get(`${SHOW_API_BASE_URL}?pageIndex=${pageIndex}`);
    }

    createShow(show){
        return axios.post(SHOW_API_BASE_URL, show);
    }

    getShowById(showId){
        return axios.get(SHOW_API_BASE_URL + '/' + showId);
    }

    updateShow(show, showId){
        return axios.put(SHOW_API_BASE_URL + '/' + showId, show);
    }

    deleteShow(showId){
        return axios.delete(SHOW_API_BASE_URL + '/' + showId);
    }

    getShowCountries() {
        return axios.get(`${SHOW_API_BASE_URL}/countries`)
    }

    getShowRatings() {
        return axios.get(`${SHOW_API_BASE_URL}/ratings`)
    }
}

export default new ShowService()