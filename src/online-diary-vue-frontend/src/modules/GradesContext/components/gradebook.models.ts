// информация о теме
export type ThemeInfo = {
  // идентификатор студента
  index: number;
  // дата проставления
  date: Date;
};

// информация о студенте
export type StudentInfo = {
  // идентификатор студента
  id: number;
  // имя студента
  name: string;
  // список оценок студента
  grades: Grade[];
};

// оценка
export type Grade = {
  // тема оценки
  theme: number;
  // ид студента
  student: number;
  // значение оценки
  gradeValue: string;
};
