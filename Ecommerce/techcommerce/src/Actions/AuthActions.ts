import {LoginDTO, RegisterDTO, TokenDTO} from "../../Models/AuthDTOs";
import axios, {AxiosResponse} from 'axios';

export const login = async (user: LoginDTO) => {

    const url = 'http://localhost:5046/api/v1/Auth/Login';
    
    try {

        const response: AxiosResponse<TokenDTO> = await axios.post(url, user);
        return response.data;
    } catch (error) {
        console.error('Error during login:', error);
        throw error;
    }
}

export const register = async (user: RegisterDTO) => {
    const url = `http://localhost:5046/api/v1/Auth/Register`;
    
    try {
        const response: AxiosResponse<RegisterDTO> = await axios.post(url, user);
        return response.data;
    } catch (error) {
        console.error('Error during registration:', error);
        throw error;
    }
}