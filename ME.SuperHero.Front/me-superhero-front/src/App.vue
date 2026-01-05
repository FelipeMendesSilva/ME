<template>
  <div>
    <h1>Projeto SUPERHERO</h1>
    <HeroiList />
  </div>
</template>

<script>
import HeroiList from "./components/HeroiList.vue";

export default {
  components: { HeroiList }
};
</script>

<style>
body {
  display: flex;
  justify-content: center; /* centraliza horizontalmente */
  min-height: 100vh;       /* ocupa toda a altura da tela */
  margin: 0;               /* remove margens padrão */
}


#app {
  font-family: Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
