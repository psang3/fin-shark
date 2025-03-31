import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import { LoginRequest, RegisterRequest, UserProfileToken } from "../Models/User";

const api = "http://localhost:5097/";

export const loginAPI = async (username: string, password: string) => {
  try {
    const request: LoginRequest = {
      userName: username,
      password: password
    };
    const response = await axios.post<UserProfileToken>(api + "api/account/login", request);
    return response;
  } catch (error) {
    handleError(error);
    throw error;
  }
};

export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const request: RegisterRequest = {
      email: email,
      userName: username,
      password: password
    };
    const response = await axios.post<UserProfileToken>(api + "api/account/register", request);
    return response;
  } catch (error) {
    handleError(error);
    throw error;
  }
};