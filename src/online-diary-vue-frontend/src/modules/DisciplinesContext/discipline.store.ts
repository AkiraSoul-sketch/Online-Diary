import { defineStore } from "pinia";
import { ref, type Ref } from "vue";
import type { Discipline } from "./discipline.models";
import { generateDisciplines } from "./discipline.logic";

const useDisciplinesStore = defineStore("disciplines", () => {
  const disciplines: Ref<Array<Discipline>> = ref(generateDisciplines(20));
  return { disciplines };
});

export { useDisciplinesStore };
