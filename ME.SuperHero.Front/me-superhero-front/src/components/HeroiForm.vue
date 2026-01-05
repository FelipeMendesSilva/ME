<template>
  <form @submit.prevent="salvar" class="heroi-form">
        <div v-if="mostrarPopup" class="popup">
            <p>{{ popupMensagem }}</p>
            <button @click="mostrarPopup = false">Fechar</button>
        </div>
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

    <div class="form-group">
    <label for="dataNascimento">Data de Nascimento:</label>
    <input id="dataNascimento" v-model="heroiLocal.dataNascimento" type="date"  placeholder="Ex: 1990-10-20"/>
    </div>

    <!-- Superpoderes -->
    <div class="form-group">
      <label>Superpoderes:</label>
      <ul class="superpoderes-list">
        <li v-for="(poder, index) in heroiLocal.superpoderes" :key="poder.id">
            {{ poder.superpoder }}
            <span class="tooltip">❓<span class="tooltip-text">{{ poder.descricao }}</span>
            </span>
  
            <button type="button" @click="removerPoder(index)">Remover</button>
        </li>
      </ul>     
          <select v-model="selectedPoder" class="dropdown">
          <option disabled value="">Selecione um poder</option>
            <option 
              v-for="sp in superpoderesDisponiveis"     
              :key="sp.id" 
              :value="sp"
              :title="sp.descricao" >
              {{ sp.superpoder }}
            </option>
          </select>
      <button type="button" @click="adicionarPoder">Adicionar poder</button>
    </div>  
    
    <button type="submit">Salvar</button>
    <button type="button" @click="$emit('cancelar')" class="btn-cancelar">Cancelar</button>
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
        dataNascimento: this.heroi?.dataNascimento ? new Date(this.heroi.dataNascimento).toISOString().split("T")[0] : "",
        superpoderes: this.heroi?.superpoderes 
      },
      superpoderesDisponiveis: [],
      mostrarPopup: false,
      popupMensagem: ""
    };
  },
  async mounted() {
    await this.carregarSuperpoderes();
  },
  methods: {
    async carregarSuperpoderes() {
      const response = await api.get("/superpoderes");
      const poderesSelecionados = this.heroiLocal.superpoderes?.map(sp => sp.id) || []; 
      this.superpoderesDisponiveis = response.data.filter(sp => 
       !poderesSelecionados.includes(sp.id) );
    },
    adicionarPoder() {
      if(this.selectedPoder){
        this.heroiLocal.superpoderes.push(this.selectedPoder); 
        this.selectedPoder = null; 
        this.carregarSuperpoderes();
      }
    },
    removerPoder(index) {
      this.heroiLocal.superpoderes.splice(index, 1);
    },
    async salvar() {
    
    const heroiDTO = { 
        id: this.heroiLocal.id, 
        nome: this.heroiLocal.nome, 
        nomeHeroi: this.heroiLocal.nomeHeroi, 
        altura: this.heroiLocal.altura, 
        peso: this.heroiLocal.peso, 
        dataNascimento: (() => { 
            const d = new Date(this.heroiLocal.dataNascimento); 
            return isNaN(d.getTime()) ? null : this.heroiLocal.dataNascimento; })(),
        superpoderes:this.heroiLocal.superpoderes?.map(sp => sp.id) || []
        };
      if (this.heroiLocal.id) { 
        try
        {
            await api.put(`/${this.heroiLocal.id}`, heroiDTO);
            this.$emit("salvar", this.heroiLocal); 
         }
        catch(error) {
            console.log("Entrou no catch");
            if (error.response && error.response.status === 400) {
                const mensagem = error.response.data || "Erro inesperado"; 
                this.mostrarPopup = true; 
                this.popupMensagem = mensagem;
            }          
            else {
            console.error("Erro ao carregar heróis", error);
          }    
        }
      } 
      else { 
        try{
            const response = await api.post(`/`, heroiDTO); 
            this.heroiLocal.id = response.data.id; 
            this.$emit("salvar", this.heroiLocal);
        }
        catch(error) {
          if (error.response && error.response.status === 400) {
            const mensagem = error.response.data || "Erro inesperado"; 
            this.mostrarPopup = true; 
            this.popupMensagem = mensagem;
          }          
          else {
            console.error("Erro ao carregar heróis", error);
          }            
        }
      }  
    }
  }
};
</script>

<style scoped>
.heroi-form {
    width:640px;
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  padding: 1.5rem;
  background: #f9f9fb;
  box-shadow: 0 4px 12px rgba(0,0,0,0.08);
  margin: auto;
  font-family: "Segoe UI", sans-serif;
}

.form-group {
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: 600;
  margin-bottom: 0.4rem;
  color: #333;
}

.form-group input,
.form-group select {
  padding: 0.6rem 0.8rem;
  border: 1px solid #ccc;
  font-size: 0.95rem;
  transition: border-color 0.2s, box-shadow 0.2s;
}

.form-group input:focus,
.form-group select:focus {
  border-color: #4a90e2;
  box-shadow: 0 0 6px rgba(74,144,226,0.3);
  outline: none;
}

button {
  padding: 0.6rem 1rem;
  border: none;
  background: #4a90e2;
  color: #fff;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s;
}

.btn-cancelar{
    background: #e94e4e;
  color: #fff;
}

.btn-cancelar:hover {
  background: #c43c3c;
}
button:hover {
  background: #357ab8;
}
.superpoderes-list {
  list-style: none; /* remove os pontos */
  padding-bottom: 20px;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 0.6rem;
}

.superpoderes-list li {
  background: #f0f4ff;
  border: 1px solid #d0d8f0;
  padding: 0.6rem 0.8rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 0.95rem;
  color: #333;
  transition: transform 0.2s, box-shadow 0.2s;
}

.superpoderes-list li:hover {
  transform: translateY(-2px);
  box-shadow: 0 3px 8px rgba(0,0,0,0.1);
}

.superpoderes-list strong {
  color: #4a90e2;
}

.tooltip {
  position: relative;
  display: inline-block;
  cursor: pointer;
  margin-left: 8px;
  color: #4a90e2;
  font-weight: bold;
}

.tooltip .tooltip-text {
  visibility: hidden;
  width: 220px;
  background-color: #333;
  color: #fff;
  text-align: left;
  padding: 0.6rem;
  position: absolute;
  z-index: 999;
  bottom: 125%; /* aparece acima */
  left: 50%;
  transform: translateX(-50%);
  opacity: 0;
  transition: opacity 0.3s;
  font-size: 1rem;
}

.tooltip:hover .tooltip-text {
  visibility: visible;
  opacity: 1;
}

/* Container do popup */
.popup {
  position: fixed;
  top: 50%;
  left: 50%;
  width: min(400px, 90vw);
  transform: translate(-50%, -50%);
  background: #bfc1c2ff;
  border: 1px solid #e5e7eb;
  box-shadow: 0 10px 30px rgba(0,0,0,0.25);
  z-index: 1000;
  padding: 20px;
  text-align: center;
  animation: fadeInScale 0.25s ease;
}

/* Texto da mensagem */
.popup p {
  margin-bottom: 16px;
  font-size: 15px;
  color: #374151;
  line-height: 1.4;
}

/* Botão dentro do popup */
.popup button {
  background: #2563eb;
  color: #fff;
  border: none;
  padding: 10px 16px;
  font-size: 14px;
  font-weight: 600;
  cursor: pointer;
  transition: background 0.2s ease, box-shadow 0.2s ease;
}

.popup button:hover {
  background: #1e4fd6;
  box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.25);
}

/* Animação de entrada */
@keyframes fadeInScale {
  from {
    opacity: 0;
    transform: translate(-50%, -50%) scale(0.95);
  }
  to {
    opacity: 1;
    transform: translate(-50%, -50%) scale(1);
  }
}

</style>