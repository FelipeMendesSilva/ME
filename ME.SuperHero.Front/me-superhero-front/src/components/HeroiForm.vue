<template>
  <form @submit.prevent="salvar">
    <input v-model="heroiLocal.nome" placeholder="Nome real" />
    <input v-model="heroiLocal.nomeHeroi" placeholder="Nome de herói" />
    <input v-model="heroiLocal.altura" type="number" step="0.01" placeholder="Altura" />
    <input v-model="heroiLocal.peso" type="number" step="0.1" placeholder="Peso" />
    <button type="submit">Salvar</button>
  </form>
</template>

<script>
import api from "../services/api";

export default {
  props: ["heroi"],
  data() {
    return {
      heroiLocal: { ...this.heroi } // cria uma cópia
    };
  },
  methods: {
    async salvar() {
      if (this.heroiLocal.id) {
        await api.put(`/${this.heroiLocal.id}`, this.heroiLocal);
      } else {
        await api.post("/", this.heroiLocal);
      }
      this.$emit("salvar");
    }
  }
};
</script>
