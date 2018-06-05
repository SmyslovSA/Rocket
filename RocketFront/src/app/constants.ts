const apiUrl = 'http://localhost:63613/';
const api = "api";

const urls = {
    signalRUrl: `${apiUrl}/`,
    getSomething: `${apiUrl}something/`,  //для примера, получится 'http://localhost:63613/something/'
    getSomethingWithApi: `${apiUrl}${api}something/`,  //для примера, получится 'http://localhost:63613/api/something/'
};

export {
    urls as Urls,
};