export type DisciplineSemester = {
  id: number;
  number: number;
};

export type DisciplineGroup = {
  id: number;
  name: string;
};

export type DisciplineTeacher = {
  id: number;
  name: string;
};

export type EntityLifeTime = {
  archived: boolean;
  createdAt: Date;
  updatedAt: Date | null;
};

export type Discipline = {
  id: number;
  name: string;
  lifeTime: EntityLifeTime;
  semester: DisciplineSemester | null;
  group: DisciplineGroup | null;
  teacher: DisciplineTeacher | null;
};
