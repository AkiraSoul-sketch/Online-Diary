// информация о теме
export type ThemeInfo = {
  // идентификатор студента
  index: number;
  // краткое название темы
  title: string;
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
  gradeValue: string | null;
};

export type GradingStudent = {
  theme: ThemeInfo;
  student: StudentInfo;
  grade: Grade;
};

export class GradeValues {
  public static GradeOtlichno(): string {
    return "5";
  }

  public static GradeKhorosho(): string {
    return "4";
  }

  public static GradeUdovletvoritelno(): string {
    return "3";
  }

  public static GradeNeudovletvoritelno(): string {
    return "2";
  }

  public static GradeUvajitelno(): string {
    return "У";
  }

  public static GradeNeYavilos(): string {
    return "Н";
  }

  public static GradeNeAttestovan(): string {
    return "НА";
  }
}
