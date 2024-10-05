import axios from 'axios';

const SHOWCATEGORY_API_BASE_URI = "http://localhost:9080/api/v1/showcategorys";

class ShowCategoryService {
    getShowCategories(){
        return axios.get(SHOWCATEGORY_API_BASE_URI);
    }
}

export default new ShowCategoryService();
