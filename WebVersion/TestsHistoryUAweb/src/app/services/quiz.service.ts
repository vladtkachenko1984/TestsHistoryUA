import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Quiz } from '../models/question.model';

@Injectable({
  providedIn: 'root'
})
export class QuizService {
  constructor(private http: HttpClient) {}

  loadQuestions(): Observable<Quiz> {
    return this.http.get<Quiz>('assets/questions.json');
  }
}