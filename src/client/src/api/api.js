import axios from 'axios';

const url = 'https://localhost:7197';

export const GetAll = (search, type) => {
    let params = {};
    if (search) {
        params.search = search;
    }
    if (type) {
        params.type = type;
    }
    return axios.get(`${url}/api/passwords`,{
        params,
    })
}
export const Update = (request) => {
    return axios.put(`${url}/api/passwords`, request,
        {
            headers: {
                'Content-Type': "application/x-www-form-urlencoded"
            }
        }
    );
}
export const Create = (request) => {
    return axios.post(`${url}/api/passwords`, request,
        {
            headers: {
                'Content-Type': "application/x-www-form-urlencoded"
            }
        }
    );
}
export const Delete = (id) => {
    return axios.delete(`${url}/api/passwords`,{
        params: {
            id
        }
    });
}