const APIUrl = "https://localhost:44315/api";

// Auth

const loginUrl = `${APIUrl}/user/auth`;
const registerUrl = `${APIUrl}/user/register`;

// User

const getUserDataUrl = `${APIUrl}/user/name`;

// Events

const eventUrl = `${APIUrl}/event`;


export { loginUrl, registerUrl, eventUrl, getUserDataUrl};