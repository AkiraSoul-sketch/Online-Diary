import { defineStore } from "pinia";
import { ref, type Ref } from "vue";
import type { TeachingGroup } from "./teacher.models";

const useTeacherStore = defineStore("teacher", () => {
  const groups: Ref<Array<TeachingGroup>> = ref([]);

  return {
    groups,
  };
});

export { useTeacherStore };
