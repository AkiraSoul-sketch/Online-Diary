import type {
  Discipline,
  DisciplineGroup,
  DisciplineSemester,
  DisciplineTeacher,
  EntityLifeTime,
} from "./discipline.models";

const SEMESTER_NUMBERS = [1, 2, 3, 4, 5, 6, 7, 8];

const DISCIPLINE_NAMES = [
  "Математика",
  "Физика",
  "Программирование",
  "Алгоритмы",
  "Базы данных",
  "Операционные системы",
  "Дискретная математика",
  "Геометрия",
  "История",
  "Иностранный язык",
];

const TEACHERS = [
  "Иванов И.И.",
  "Петров П.П.",
  "Сидоров С.С.",
  "Кузнецова А.А.",
  "Смирнов В.В.",
  "Коваленко Н.Н.",
  "Морозова Е.Е.",
  "Новиков К.К.",
  "Федоров М.М.",
  "Ершова О.О.",
];

const GROUPS = ["Группа 1", "Группа 2", "Группа 3"];

function getRandomArrayValue<T>(array: T[]): T {
  const randomNumber: number = Math.random() * array.length;
  const randomIndex: number = Math.floor(randomNumber);
  return array[randomIndex];
}

function getRandomId(): number {
  const randomId: number = Math.floor(Math.random() * 1000);
  return randomId;
}

function generateEntityLifeTime(): EntityLifeTime {
  const createdAt: Date = new Date(Date.now());
  const updatedAt: Date | null = null;
  const archived: boolean = false;
  return { archived, createdAt, updatedAt };
}

function generateTeacher(): DisciplineTeacher {
  const randomId: number = getRandomId();
  const randomTeacherName: string = getRandomArrayValue(TEACHERS);
  return { id: randomId, name: randomTeacherName };
}

function generateSemester(): DisciplineSemester {
  const randomId: number = getRandomId();
  const randomSemesterNumber: number = getRandomArrayValue(SEMESTER_NUMBERS);
  return { id: randomId, number: randomSemesterNumber };
}

function generateRandomGroup(): DisciplineGroup {
  const randomId: number = getRandomId();
  const randomGroupName: string = getRandomArrayValue(GROUPS);
  return { id: randomId, name: randomGroupName };
}

function generateDiscipline(): Discipline {
  const randomId: number = getRandomId();
  const name: string = getRandomArrayValue(DISCIPLINE_NAMES);
  const semester: DisciplineSemester = generateSemester();
  const teacher: DisciplineTeacher = generateTeacher();
  const group: DisciplineGroup = generateRandomGroup();
  const lifeTime: EntityLifeTime = generateEntityLifeTime();
  return {
    id: randomId,
    name,
    semester,
    teacher,
    group,
    lifeTime,
  };
}

function generateDisciplines(count: number): Discipline[] {
  const disciplines: Discipline[] = [];
  for (let i = 0; i < count; i++) {
    const discipline: Discipline = generateDiscipline();
    disciplines.push(discipline);
  }
  return disciplines;
}

export { generateDisciplines };
