<template>
  <div>
    <h1>CRUD de Heróis 🦸‍♂️</h1>
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
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
