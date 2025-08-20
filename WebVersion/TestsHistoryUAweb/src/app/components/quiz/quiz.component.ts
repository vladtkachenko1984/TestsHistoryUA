import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { Question, Quiz } from '../../models/question.model';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quiz.component.html'
})
export class QuizComponent implements OnInit {
  questions: Question[] = [];
  currentIndex = 0;
  selectedAnswers: { [key: number]: string } = {};
  showResults = false;

  constructor(private quizService: QuizService) {}

  ngOnInit(): void {
    this.quizService.loadQuestions().subscribe((quiz: Quiz) => {
      this.questions = quiz.questions;
    });
  }

  get currentQuestion(): Question {
    return this.questions[this.currentIndex];
  }

  nextQuestion(): void {
    if (this.currentIndex < this.questions.length - 1) {
      this.currentIndex++;
    }
  }

  previousQuestion(): void {
    if (this.currentIndex > 0) {
      this.currentIndex--;
    }
  }

  submitQuiz(): void {
    this.showResults = true;
    console.log('Submitted answers:', this.selectedAnswers);
  }

  getScore(): number {
    let score = 0;
    for (let q of this.questions) {
      const answer = this.selectedAnswers[q.id];
      const correct = q.options.find(o => o.isCorrect)?.text;
      if (answer === correct) {
        score++;
      }
    }
    return score;
  }
}
