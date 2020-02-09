const APIUrl = "https://localhost:44315/api";

// Auth

const loginUrl = `${APIUrl}/user/auth`;
const registerUrl = `${APIUrl}/user/register`;
const eventsCreated = `${APIUrl}/user/eventcreated`;

// User

const getUserDataUrl = `${APIUrl}/user/name`;

// Events

const eventUrl = `${APIUrl}/event`;


export { loginUrl, registerUrl, eventUrl, getUserDataUrl, eventsCreated};