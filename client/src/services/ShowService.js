import axios from 'axios';

const USER_API_BASE_URL = "http://localhost:9080/api/v1/shows";

class ShowService {

    getShows(){
        return axios.get(USER_API_BASE_URL);
    }

    createShow(show){
        return axios.post(USER_API_BASE_URL, show);
    }

    getShowById(showId){
        return axios.get(USER_API_BASE_URL + '/' + showId);
    }

    updateShow(show, showId){
        return axios.put(USER_API_BASE_URL + '/' + showId, show);
    }

    deleteShow(showId){
        return axios.delete(USER_API_BASE_URL + '/' + showId);
    }
}

export default new ShowService()