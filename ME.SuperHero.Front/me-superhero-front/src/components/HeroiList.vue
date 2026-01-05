<template>
  <div>
    <!-- Lista só aparece se não estiver editando -->
    <div v-if="!heroiSelecionado">
      <h2>Lista de Heróis</h2>
       <button @click="novoHeroi" class="btn-adicionar">
            ➕ Adicionar Herói
       </button>
       <div v-if="mostrarPopup" class="popup">
            <p>{{ popupMensagem }}</p>
            <button @click="mostrarPopup = false">Fechar</button>
        </div>
      <ul class="herois-list">
        <li v-for="heroi in herois" :key="heroi.id" class="item-herois-list">
          <div class="heroi-info">
            <h3>{{ heroi.nomeHeroi }}</h3>
            <p><strong>Nome real:</strong> {{ heroi.nome }}</p>
            <p><strong>Data de nascimento:</strong> {{ formatarData(heroi.dataNascimento) }}</p>
            <p><strong>Altura:</strong> {{ heroi.altura }} m</p>
            <p><strong>Peso:</strong> {{ heroi.peso }} kg</p>
          </div>

          <div class="superpoderes">
            <h4>Superpoderes:</h4>
            <ul class="superpoderes-list">
              <li v-for="poder in heroi.superpoderes" :key="poder.id" class="poder-item">
                <span class="poder-nome">
                  {{ poder.superpoder }}
                  <span class="descricao">{{ poder.descricao }}</span>
                </span>
              </li>
            </ul>
          </div>

          <div class="acoes">
            <button class="btn editar" @click="editarHeroi(heroi)">Editar</button>
            <button class="btn excluir" @click="deletarHeroi(heroi.id)">Excluir</button>
          </div>
        </li>
      </ul>
    </div>

    <!-- Form só aparece se estiver editando -->
    <HeroiForm 
      v-else 
      :heroi="heroiSelecionado" 
      @salvar="atualizarHeroiNaLista" 
      @cancelar="cancelarEdicao"  
    />
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
      heroiSelecionado: null,
      mostrarPopup: false,
      popupMensagem: ""
    };
  },
  async mounted() {
    await this.carregarHerois();
  },
  methods: {
    async carregarHerois() {
        try {
           const response = await api.get("/");

           if (!response.data || response.data.length === 0) {
              // dispara popup de lista vazia
             this.mostrarPopup = true;
             this.popupMensagem = "Lista vazia";
           } 
           else {
                this.herois = response.data;
                this.heroiSelecionado = null;
            }
        } 
        catch (error) {
          if (error.response && error.response.status === 404) {
            this.mostrarPopup = true;
            this.popupMensagem = "Lista vazia";
          }          
          else {
            console.error("Erro ao carregar heróis", error);
          }
        }
    },
    editarHeroi(heroi) {
      this.heroiSelecionado = { ...heroi };
    },
    novoHeroi() { 
        this.heroiSelecionado = { id: 0, nome: '', altura: '', peso: '', superpoderes: []} 
    },
    async atualizarHeroiNaLista(heroiAtualizado) { 
        const index = this.herois.findIndex(h => h.id === heroiAtualizado.id); 
        if (index !== -1) { this.herois.splice(index, 1, heroiAtualizado);} 
        else { this.herois.push(heroiAtualizado);}
        await this.carregarHerois();
        this.heroiSelecionado = null; // fecha edição 
    },
    cancelarEdicao() 
    { 
        this.heroiSelecionado = null; // volta para lista 
    },
    formatarData(data) {
    if (!data) return ""; // ou "N/A"
        const d = new Date(data);
        return isNaN(d.getTime()) ? "" : d.toLocaleDateString("pt-BR");
    },
    async deletarHeroi(id) {
      await api.delete(`/${id}`);
      this.herois = this.herois.filter(h => h.id !== id); 
      this.heroiSelecionado = null;
      await this.carregarHerois();
    }
  }
};
</script>

<style>
.herois-list {
  list-style: none; /* remove os pontos */
  padding: 0;
  margin: 1rem 0;
  display: flex;
  flex-direction: column;
  gap: 0.8rem;
}

.herois-list li {
  background: rgba(240, 244, 255, 0.6); /* cor translúcida */
  border: 1px solid #d0d8f0;
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  padding: 6px 10px;       /* reduz espaçamento interno */
  margin-bottom: 4px;      /* espaço menor entre itens */
  border-radius: 6px;      /* cantos arredondados */
  transition: transform 0.2s, box-shadow 0.2s;
}

.herois-list li:hover {
  transform: translateY(-2px);
  box-shadow: 0 2px 6px rgba(0,0,0,0.15);
  background: rgba(240, 244, 255, 0.8); /* aumenta opacidade no hover */
}


.herois-list li:hover {
  transform: translateY(-2px);
  box-shadow: 0 3px 8px rgba(0,0,0,0.1);
}

.superpoderes-list {
  list-style: none;
  padding: 0 0 20px 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.item-herois-list{
  display: flex; 
}
.item-herois-list > div:first-child {
  width: 300px;       /* largura fixa */
  flex-shrink: 0;     /* impede que diminua quando faltar espaço */
  text-align: left;   /* alinha o conteúdo dentro da div */  
}

.item-herois-list> div:nth-child(2),
.item-herois-list> div:nth-child(3) {
  flex: 1;            /* ocupam o restante do espaço igualmente */
  text-align: center; /* centraliza o texto */
}

.poder-item {   
  background: #eef3ff;
  border: 1px solid #d0d8f0;
  padding: 6px 6px;
  font-size: 0.95rem;
  color: #333;
  transition: transform 0.2s, box-shadow 0.2s;
}

.poder-item:hover {
  transform: translateY(-2px);
  /*box-shadow: 0 3px 8px rgba(0,0,0,0.1);*/
}

.poder-nome {
  position: relative;
  cursor: pointer;
  font-weight: 300;
  color: #4a90e2;
}

.poder-nome .descricao {
  visibility: hidden;
  opacity: 0;
  position: absolute;
  top: 120%; /* aparece abaixo do nome */
  left: 120%;
  background: #333;
  color: #fff;
  padding: 0.5rem;
  width: 220px;
  font-size: 0.85rem;
  transition: opacity 0.3s;
  z-index: 1;
}

.poder-item:hover .descricao {
  visibility: visible;
  opacity: 1;
}

.heroi-info {
  text-align: left;
  font-size: 1rem;
  color: #333;
  line-height: 0;   
  margin: 0;          
  padding: 2px 0;     
}


.heroi-info strong {
  color: #346194ff;
}

.heroi-info >h3 {
    padding-bottom:30px;     
  color: #373737ff;
}

.nome-real {
  color: #666;
  margin-left: 6px;
}

.acoes {
  padding-left:40px;
  padding-top: 20px;
  display: flex;
  gap: 0.5rem;
}

.btn {
  padding: 0.4rem 0.8rem;
  border: none;
  font-size: 0.85rem;
  cursor: pointer;
  transition: background 0.2s;
}

.btn.editar {
  background: #4a90e2;
  color: #fff;
}
.btn-adicionar{
  background: #23a234ff;
  color: #fff;
}

.btn.editar:hover {
  background: #357ab8;
}

.btn.excluir {
  background: #e94e4e;
  color: #fff;
}

.btn.excluir:hover {
  background: #c43c3c;
}

/* Fundo escurecido atrás do popup */
.backdrop {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.45);
  backdrop-filter: blur(2px);
  z-index: 999;
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