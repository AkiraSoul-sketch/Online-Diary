export type TeachingGroup = {
  id: number;
  name: string;
  disciplines: TeachingDiscipline[];
};

export type TeachingDiscipline = {
  id: number;
  groupId: number;
  name: string;
};
