import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:7184/api/Herois" // ajuste para a porta da sua API
});

export default api;
