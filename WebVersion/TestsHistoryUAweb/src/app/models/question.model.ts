// question.model.ts

export interface Option {
  text: string;
  isCorrect: boolean;
}

export interface Question {
  id: number;
  question: string;
  options: Option[];
  explanation: string;
}

export interface Quiz {
  questions: Question[];
}
