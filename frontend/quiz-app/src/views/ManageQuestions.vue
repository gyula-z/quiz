<template>
  <div id="manage-questions">
    <div class="questions">
      <div v-for="question in questions" :key="question.id" class="question">
        <div>{{ question.text }}</div>
        <button @click="deleteQuestion(question.id)">Delete</button>
      </div>
    </div>
    <form @submit.prevent="submitQuestion">
      <input v-model="newQuestion.text" placeholder="Question" />
      <div v-for="(answer, index) in newQuestion.answers" :key="index">
        <input v-model="answer.text" :placeholder="'Answer ' + (index + 1)" />
        <input
          type="radio"
          v-model="newQuestion.correctAnswerIndex"
          :value="index"
        />
      </div>
      <button type="submit">Submit</button>
      <div
        :class="{
          'submission-message': submissionMessage,
          'error-message': error,
        }"
      >
        {{ error || submissionMessage }}
      </div>
    </form>
    <router-link to="/">Go back to the game</router-link>
  </div>
</template>

<script>
import apiClient from "../apiService";

export default {
  name: "ManageQuestions",
  data() {
    return {
      questions: [],
      submissionMessage: null,
      newQuestion: {
        text: "",
        answers: [
          { text: "", is_correct: false },
          { text: "", is_correct: false },
          { text: "", is_correct: false },
          { text: "", is_correct: false },
        ],
      },
      error: null,
    };
  },
  methods: {
    fetchQuestions() {
      apiClient
        .get("/quiz/questions")
        .then((response) => {
          this.questions = response.data;
        })
        .catch((error) => {
          console.error(error);
        });
    },
    deleteQuestion(id) {
      apiClient
        .delete(`/quiz/questions/${id}`)
        .then(() => {
          this.fetchQuestions();
        })
        .catch((error) => {
          console.error(error);
        });
    },
    submitQuestion() {
      if (this.newQuestion.correctAnswerIndex !== null) {
        this.newQuestion.answers[
          this.newQuestion.correctAnswerIndex
        ].is_correct = true;
      }

      if (
        !this.newQuestion.text ||
        !this.newQuestion.answers.some((a) => a.text && a.is_correct)
      ) {
        this.error =
          "Please fill out all required fields and mark one answer as correct.";
      } else {
        const questionData = {
          question: this.newQuestion.text,
          answers: this.newQuestion.answers
            .filter((answer) => answer.text)
            .map((answer) => {
              return {
                answer: answer.text,
                is_correct: answer.is_correct ? 1 : 0,
              };
            }),
        };

        apiClient
          .post("/quiz/questions", questionData)
          .then((response) => {
            this.fetchQuestions();
            this.resetForm();
            this.submissionMessage = `Question successfully submitted with nr. ${response.data}.`;
          })
          .catch((error) => {
            console.error(error);
          });
      }
    },
    resetForm() {
      this.newQuestion = {
        text: "",
        correctAnswerIndex: null,
        answers: [
          { text: "", is_correct: false },
          { text: "", is_correct: false },
          { text: "", is_correct: false },
          { text: "", is_correct: false },
        ],
      };
      this.error = null;
    },
  },
  created() {
    this.fetchQuestions();
  },
};
</script>

<style scoped>
#manage-questions {
  display: flex;
  flex-direction: column;
  align-items: center;
  background: aliceblue;
  padding: 50px;
  border-radius: 10px;
  width: 400px;
  margin: auto;
}

.questions {
  margin-bottom: 20px;
  color: black;
}

.question {
  background: white;
  padding: 20px;
  border-radius: 10px;
  width: 100%;
  margin-bottom: 10px;
  display: flex;
  justify-content: space-between;
}

.submission-message {
  color: green;
  font-size: 1rem;
  font-weight: bold;
}

.error-message {
  color: red;
  font-size: 1rem;
  font-weight: bold;
}

form {
  background: white;
  padding: 20px;
  border-radius: 10px;
  width: 100%;
  margin-bottom: 20px;
}

input {
  margin-bottom: 10px;
  padding: 0.7rem;
  background-color: lightblue;
  color: black;
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
</style>
