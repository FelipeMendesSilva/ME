import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7184/api/Herois" // ajuste para a porta da sua API
});

export default api;
