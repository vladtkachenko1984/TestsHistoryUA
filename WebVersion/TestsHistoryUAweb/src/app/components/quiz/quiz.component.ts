import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { QuizService } from '../../services/quiz.service';
import { Question, Quiz } from '../../models/question.model';

@Component({
  selector: 'app-quiz',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './quiz.component.html',
})
export class QuizComponent implements OnInit {
  questions: Question[] = [];
  selectedAnswers: { [id: number]: string } = {};
  showResults = false;

  constructor(private quizService: QuizService) {}

  ngOnInit(): void {
    this.quizService.loadQuestions().subscribe(data => {
      this.questions = data.questions;
    });
  }

  submitQuiz() {
    this.showResults = true;
  }

  getScore(): number {
    return this.questions.filter(q =>
      q.options.find(opt => opt.isCorrect)?.text === this.selectedAnswers[q.id]
    ).length;
  }
}
