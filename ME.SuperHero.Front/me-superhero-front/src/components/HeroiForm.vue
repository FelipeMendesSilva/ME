<template>
  <form @submit.prevent="salvar" class="heroi-form">
    <div class="form-group">
      <label for="nome">Nome real:</label>
      <input id="nome" v-model="heroiLocal.nome" type="text" placeholder="Ex: Bruce Wayne" />
    </div>

    <div class="form-group">
      <label for="nomeHeroi">Nome de herói:</label>
      <input id="nomeHeroi" v-model="heroiLocal.nomeHeroi" type="text" placeholder="Ex: Batman" />
    </div>

    <div class="form-group">
      <label for="altura">Altura (m):</label>
      <input id="altura" v-model.number="heroiLocal.altura" type="number" step="0.01" placeholder="Ex: 1.88" />
    </div>

    <div class="form-group">
      <label for="peso">Peso (kg):</label>
      <input id="peso" v-model.number="heroiLocal.peso" type="number" step="0.1" placeholder="Ex: 95" />
    </div>

    <!-- Superpoderes -->
    <div class="form-group">
      <label>Superpoderes:</label>
      <ul>
        <li v-for="(poder, index) in heroiLocal.superpoderes" :key="index">
          <input v-model="heroiLocal.superpoderes[index]" placeholder="Ex: Força, voo..." />
          <button type="button" @click="removerPoder(index)">Remover</button>
        </li>
      </ul>
      <button type="button" @click="adicionarPoder">Adicionar poder</button>
    </div>
   

    <button type="submit">Salvar</button>
  </form>
</template>

<script>
import api from "../services/api";

export default {
  props: ["heroi"],
  data() {
    return {
      heroiLocal: {
        ...this.heroi,
        superpoderes: this.heroi?.superpoderes || [] // garante array
      }
    };
  },
  methods: {
    adicionarPoder() {
      this.heroiLocal.superpoderes.push("");
    },
    removerPoder(index) {
      this.heroiLocal.superpoderes.splice(index, 1);
    },
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

<style scoped>
.heroi-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}
.form-group {
  display: flex;
  flex-direction: column;
}
ul {
  list-style: none;
  padding: 0;
}
li {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 0.5rem;
}
button {
  margin-top: 0.5rem;
}
</style>
