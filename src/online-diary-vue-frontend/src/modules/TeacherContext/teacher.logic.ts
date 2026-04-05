import type { TeachingDiscipline, TeachingGroup } from "./teacher.models";

const useTeacherLogic = () => {
  return {
    generateRandomGroups,
  };
};

function generateRandomGroups(
  count: number,
  disciplinesCount: number,
): TeachingGroup[] {
  const groups: TeachingGroup[] = [];
  for (let i = 0; i < count; i++) {
    const group = generateRandomGroup();
    for (let j = 0; j < disciplinesCount; j++) {
      addRandomDiscipline(group);
    }
    groups.push(group);
  }
  return groups;
}

function addRandomDiscipline(group: TeachingGroup): void {
  const disciplineName: string = getRandomArrayValue(disciplineNames);
  const disciplineId: number = generateRandomId();
  const newDiscipline: TeachingDiscipline = {
    id: disciplineId,
    groupId: group.id,
    name: disciplineName,
  };
  group.disciplines.push(newDiscipline);
}

function generateRandomGroup(): TeachingGroup {
  const groupName: string = getRandomArrayValue(groupNames);
  const id: number = generateRandomId();
  return {
    id: id,
    disciplines: [],
    name: groupName,
  };
}

function generateRandomId(): number {
  return Math.floor(Math.random() * 1000);
}

function getRandomArrayValue<T>(arr: T[]): T {
  const randomIndex = Math.floor(Math.random() * arr.length);
  return arr[randomIndex];
}

const disciplineNames = [
  "Алгоритмы и структуры данных",
  "Программирование на Java",
  "Программирование на Python",
  "Программирование на C++",
  "Программирование на JavaScript",
  "Базы данных",
  "Веб-разработка",
  "Мобильная разработка",
  "Разработка игр",
  "Искусственный интеллект",
  "Машинное обучение",
  "Компьютерные сети",
  "Операционные системы",
];

const groupNames = ["Группа А1", "Группа Б2", "Группа В3"];

export { useTeacherLogic };
