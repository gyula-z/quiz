# Quiz application

## Highlights

- The user is able to take a quiz
  - Getting different questions, one at a time
  - After selecting an answer, the correct answer is shown
  - New question displays after the previous one is answered
- The user is able to add new questions by providing:
  - a question
  - 2-4 possible answers
  - marking the right answer
- The user is able to delete questions by ID

## Frontend

The application consists of two screens.

### Game

- It displays the question
- It has 4 buttons with the answers
- It marks the right or wrong answer by clicking in it
- It gets a new random question
- It shows current score
  - First time it's 0, and every right answer increments it by one.
- It has a link `manage questions`

### Manage questions

- It shows all the questions
- All questions has a delete link
  - Clicking on it deletes the question
- It has input fields below the questions:
  - Question
  - Answer 1
  - Answer 2
  - Answer 3
  - Answer 4
- The answers have radio buttons for marking the right answer
- And a submit button for sending the new question to the database
- It won't send the answers that are left empty
- It displays an error message if the new question is submitted before the
  required fields have been filled and an answer has been marked as the right
  one
- It has a link `go back to the game`

## Endpoints

### GET `/`

It renders a static HTML, as a game page.

### GET `/questions`

It renders a static HTML, as a manage questions page.

### GET `/api/quiz/game`

This endpoint returns a random question with its answers.

_Example:_

```json
{
  "id": 2,
  "question": "When did the Titanic sink?",
  "answers": [
    {
      "question_id": 2,
      "id": 5,
      "answer": "1912",
      "is_correct": 1
    },
    {
      "question_id": 2,
      "id": 6,
      "answer": "1914",
      "is_correct": 0
    },
    {
      "question_id": 2,
      "id": 7,
      "answer": "1919",
      "is_correct": 0
    },
    {
      "question_id": 2,
      "id": 8,
      "answer": "1899",
      "is_correct": 0
    }
  ]
}
```

### GET `/api/quiz/questions`

This endpoint returns all the questions.

_Example:_

```json
[
    {
        "id": 1,
        "question": "Who played Neo in The Matrix?"
    },
    {
        "id": 2,
        "question": "When did the Titanic sink?"
    },
    {
        "id": 3,
        "question": "Who is from the House Targaryen, First of Her Name, the Unburnt, Queen of the Andals and the First Men, Khaleesi of the Great Grass Sea, Breaker of Chains, and Mother of Dragon?"
    },
    ...
]
```

### POST `/api/quiz/questions`

- If you fill the form and click on the submit button, it adds a new
  question and its answers
- It accepts the new question only if:
  - A question is provided
  - At least 2, at most 4 answers are provided
  - None of the provided answers is empty
  - Exactly 1 answer is marked as the correct one

_Example:_

```json
{
    "question": "What is the lowest male voice?"
    "answers": [
        {
            "answer": "Bariton",
            "is_correct": 0
        },
        {
            "answer": "Bass",
            "is_correct": 1
        },
        {
            "answer": "Tenor",
            "is_correct": 0
        },
        {
            "answer": "Alt",
            "is_correct": 0
        }
    ]
}
```

### DELETE `/api/quiz/questions/:id`

- If you click on the delete link (which is next to the question)
- It deletes the question and its answers by id
