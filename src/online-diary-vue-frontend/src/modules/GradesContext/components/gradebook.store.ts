import { defineStore } from "pinia";
import type {
  Grade,
  GradingStudent,
  StudentInfo,
  ThemeInfo,
} from "./gradebook.models";
import { computed, onMounted, ref, watch, type Ref } from "vue";

export { useGradebookStore };

const useGradebookStore = defineStore("gradebook", () => {
  const students: Ref<Array<StudentInfo>> = ref([]);
  const themes: Ref<Array<ThemeInfo>> = ref([]);
  const gradingStudent: Ref<GradingStudent | null> = ref(null);
  const activeThemeIndex: Ref<number | null> = ref(null);
  const gradingStudentValue = computed(() => gradingStudent.value);

  onMounted(() => {
    themes.value = generateRandomThemes(20);
    students.value = generateRandomStudents(20, themes.value);
    activeThemeIndex.value = themes.value[0]?.index ?? null;
  });

  watch(
    themes,
    (loadedThemes) => {
      if (loadedThemes.length === 0) {
        activeThemeIndex.value = null;
        return;
      }

      const current = activeThemeIndex.value;
      const exists = loadedThemes.some((theme) => theme.index === current);
      if (!exists) {
        activeThemeIndex.value = loadedThemes[0].index;
      }
    },
    { immediate: true },
  );

  function pickGradingStudent(grade: Grade): void {
    gradingStudent.value = getStudentForGrade(grade);
  }

  function gradeStudent(gradeValue: string): void {
    if (!gradingStudent.value) return;
    for (const student of students.value) {
      if (student.id === gradingStudent.value.student.id) {
        for (const studentGrade of student.grades) {
          if (studentGrade.theme === gradingStudent.value.theme.index) {
            updateStudentGrade(studentGrade, gradeValue);
            return;
          }
        }
      }
    }
  }

  function disbandGradingStudent(): void {
    gradingStudent.value = null;
  }

  function selectTheme(themeIndex: number): void {
    activeThemeIndex.value = themeIndex;
  }

  function updateStudentGrade(studentGrade: Grade, gradeValue: string): void {
    studentGrade.gradeValue = gradeValue;
    disbandGradingStudent();
  }

  function makeGradingStudent(
    student: StudentInfo | undefined,
    theme: ThemeInfo | undefined,
    grade: Grade | undefined,
  ): GradingStudent | null {
    if (!student || !theme || !grade) return null;
    return { grade, student, theme };
  }

  function getStudentForGrade(grade: Grade): GradingStudent | null {
    const student = students.value.find((s) => s.id === grade.student);
    const theme = themes.value.find((t) => t.index === grade.theme);
    const selectedGrade = student?.grades.find(
      (g) => g.theme === grade.theme && g.student === grade.student,
    );

    return makeGradingStudent(student, theme, selectedGrade);
  }

  return {
    students,
    themes,
    activeThemeIndex,
    gradingStudentValue,
    pickGradingStudent,
    gradeStudent,
    disbandGradingStudent,
    selectTheme,
  };
});

const date: Date = new Date(Date.now());

const studentNames: string[] = [
  "Иванов Иван Иванович",
  "Петров Петр Петрович",
  "Сидоров Сидор Сидорович",
  "Кузнецов Кузьма Кузьмич",
  "Смирнов Семён Семёнович",
  "Попов Павел Павлович",
  "Васильев Василий Васильевич",
  "Михайлов Михаил Михайлович",
  "Новиков Николай Николаевич",
  "Фёдоров Фёдор Фёдорович",
  "Григорьев Григорий Григорьевич",
  "Алексеев Алексей Алексеевич",
  "Сергеев Сергей Сергеевич",
  "Андреев Андрей Андреевич",
  "Макаров Макар Макарович",
  "Никитин Никита Никитич",
  "Григорьев Григорий Григорьевич",
  "Егоров Егор Егорович",
  "Павлов Павел Павлович",
];

function generateRandomStudents(
  count: number,
  themes: ThemeInfo[],
): StudentInfo[] {
  const students: StudentInfo[] = [];
  for (let i = 0; i < count; i++) {
    const student: StudentInfo = generateRandomStudent(i, themes);
    students.push(student);
  }

  return students;
}

function generateRandomStudent(
  studentId: number,
  themes: ThemeInfo[],
): StudentInfo {
  const name = generateRandomStudentName();
  const grades: Grade[] = [];
  for (const theme of themes) {
    const grade: Grade = {
      theme: theme.index,
      student: studentId,
      gradeValue: generateRandomGradeValue(),
    };
    grades.push(grade);
  }

  return {
    id: studentId,
    name,
    grades,
  };
}

const gradesList: string[] = ["2", "3", "4", "5", "У", "Н", "НА"];

function generateRandomStudentName(): string {
  const maxStudents = studentNames.length;
  const randomIndex = Math.floor(Math.random() * maxStudents);
  const name = studentNames[randomIndex];

  return name;
}

function generateRandomGradeValue(): string | null {
  const listLength: number = gradesList.length;
  const randomNumber: number = Math.random();
  const randomIndex: number = Math.floor(randomNumber * listLength) + 1;
  if (randomIndex >= listLength) return null;
  const randomGrade: string = gradesList[randomIndex];

  return randomGrade;
}

function generateRandomThemes(count: number): ThemeInfo[] {
  const themes: ThemeInfo[] = [];
  for (let i = 0; i < count; i++) {
    const info: ThemeInfo = {
      date: date,
      index: i + 1,
      title: `Тема ${i + 1}`,
    };
    themes.push(info);
  }

  return themes;
}
