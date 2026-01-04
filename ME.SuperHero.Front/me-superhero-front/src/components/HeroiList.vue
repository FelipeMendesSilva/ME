<template>
  <div>
    <h2>Lista de Heróis</h2>
    <ul>
      <li v-for="heroi in herois" :key="heroi.id">
        {{ heroi.nomeHeroi }} ({{ heroi.nome }})
        <button @click="editarHeroi(heroi)">Editar</button>
        <button @click="deletarHeroi(heroi.id)">Excluir</button>
      </li>
    </ul>
    <HeroiForm v-if="heroiSelecionado" :heroi="heroiSelecionado" @salvar="carregarHerois" />
  </div>
</template>

<script>
import api from "../services/api";
import HeroiForm from "./HeroiForm.vue";

export default {
  components: { HeroiForm },
  data() {
    return {
      herois: [],
      heroiSelecionado: null
    };
  },
  async mounted() {
    await this.carregarHerois();
  },
  methods: {
    async carregarHerois() {
      const response = await api.get("/");
      this.herois = response.data;
      this.heroiSelecionado = null;
    },
    editarHeroi(heroi) {
      this.heroiSelecionado = { ...heroi };
    },
    async deletarHeroi(id) {
      await api.delete(`/${id}`);
      await this.carregarHerois();
    }
  }
};
</script>
