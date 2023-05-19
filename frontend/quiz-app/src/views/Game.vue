<template>
  <div id="game">
    <div class="score-board">SCORE: {{ score }}</div>
    <div class="game-board">
      <div class="question">{{ question }}</div>
      <div class="answers">
        <button
          v-for="answer in answers"
          :key="answer.id"
          :class="{
            'answer-correct': answer.correct,
            'answer-incorrect': answer.clicked && !answer.correct,
          }"
          @click="answerQuestion(answer)"
          :disabled="buttonDisabled"
        >
          {{ answer.text }}
        </button>
      </div>
    </div>
    <div class="manage-link">
      <router-link to="/questions">Manage questions</router-link>
    </div>
  </div>
</template>

<script>
import apiClient from "../apiService";

export default {
  name: "Game",
  data() {
    return {
      score: 0,
      question: "",
      answers: [],
      buttonDisabled: false,
    };
  },
  methods: {
    fetchQuestion() {
      apiClient
        .get("/quiz/game")
        .then((response) => {
          this.question = response.data.text;
          this.answers = response.data.answers;
          this.answers.forEach((answer) => {
            answer.clicked = false;
            answer.correct = null;
          });
          this.buttonDisabled = false;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    answerQuestion(answer) {
      if (!answer.clicked) {
        answer.clicked = true;
        answer.correct = answer.isCorrect;

        if (answer.correct) {
          this.score += 1;
        }

        this.buttonDisabled = true;
        setTimeout(() => {
          this.fetchQuestion();
        }, 2000);
      }
    },
  },
  created() {
    this.fetchQuestion();
  },
};
</script>

<style scoped>
#game {
  display: flex;
  flex-direction: column;
  align-items: center;
  background: #f0f0f0;
  padding: 20px;
  border-radius: 10px;
  width: 400px;
  margin: auto;
}

.score-board {
  font-size: 1.5rem;
  margin-bottom: 20px;
  color: black;
}

.game-board {
  background: white;
  padding: 20px;
  border-radius: 10px;
  width: 100%;
  margin-bottom: 20px;
}

.question {
  color: black;
}

.answers {
  display: flex;
  flex-direction: column;
}

button {
  background: royalblue;
  color: white;
  padding: 10px;
  margin: 10px 0;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.answer-correct {
  background: green;
}

.answer-incorrect {
  background: red;
}

.manage-link {
  margin-top: 20px;
}
</style>
