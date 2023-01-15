# csit327-project-group-6 **QUIZ API**

## Entity Relationship Diagram

![ERD Flow](https://github.com/TpenXsinoy/quiz-api/blob/master/Document/Quiz%20System%20UML.png)

## Summary

- This project provides features that make it possible to efficiently manage takers, quizzes, topics, questions, taker's answers, and quiz results in a quiz system.

## API Endpoints:

### Takers

- POST /api/Takers -- Creates Taker
- POST /api/Takers/takeQuiz?takerId=3&quizId=2 -- Lets Taker taker Quiz
- GET api/Takers -- Gets all Takers
- GET api/Takers?quizId=1 -- Gets all Takers assigned to a Quiz
- GET /api/Takers/{id} -- Gets Taker
- GET /api/Takers/{id}/quizResults -- Gets Taker with Quiz Results
- GET /api/Takers/{id}/answers -- Gets Taker with answers
- PUT /api/Takers -- Updates a Taker
- DELETE api/Takers/{id} -- Deletes a Taker

### Quizzes

- POST /api/Quizzes -- Creates Quiz
- GET api/Quizzes -- Gets all Quizzes
- GET api/Quizzes?TopicId=2 -- Gets all Quizzes that a topic is assigned to
- GET api/Quizzes?TakerId=2 -- Gets all Quizzes that a taker has taken
- GET api/Quizzes/{id} -- Gets Quiz by Id
- GET /api/Quizzes/{id}/takers -- Gets Quiz with takers
- GET /api/Quizzes/{id}/quizResults -- Gets Quiz with Quiz Results
- PUT /api/Quizzes -- Updates Quiz
- DELETE api/Quizzes/{id} -- Deletes Quiz

### Topics

- POST /api/Topics -- Creates Topic
- GET api/Topics -- Gets all Topics
- GET api/Topics?quizId=1 -- Gets all topics assigned to a quiz
- GET api/Topics/{id} -- Gets Topic
- PUT /api/Topic -- Updates Topic
- DELETE api/Topic/{id} -- Deletes Topic

### Questions

- POST /api/questions -- Creates Question
- GET /api/Questions -- Gets all Questions
- GET api/Questions?topicId=2 -- Gets all Questions by Topic Id
- GET /api/Questions/{id} -- Gets Question
- PUT /api/Questions/{id} -- Updates Question
- DELETE api/Takers/{id} -- Deletes Question

### Quiz Results

- POST /api/QuizResults -- Creates Quiz Result
- GET api/QuizResults -- Gets all Quiz Result
- GET /api/QuizResults/{id} -- Gets Quiz Result
- PUT /api/QuizResults/{id} -- Updates Quiz Result
- DELETE api/QuizResults/{id} -- Deletes Quiz Result

## Authors

Acojedo, Jhonray V.

Carillo, Alys Anthea V.

Sinoy, Stephine N.

Terdes, Jose Felipe C.

## Instructor

Mr. Jhon Christian Ambrad
