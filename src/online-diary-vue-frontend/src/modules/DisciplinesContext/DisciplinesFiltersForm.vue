<script setup lang="ts">
import { SearchIcon } from "lucide-vue-next";
import InputWithIcon from "../Common/Components/InputWithIcon.vue";
import Button from "@/components/ui/button/Button.vue";
import Label from "@/components/ui/label/Label.vue";
import Field from "@/components/ui/field/Field.vue";
import Checkbox from "@/components/ui/checkbox/Checkbox.vue";
import FieldContent from "@/components/ui/field/FieldContent.vue";
import FieldLabel from "@/components/ui/field/FieldLabel.vue";
import { useDisciplinesStore } from "./discipline.store";
import type {
  Discipline,
  DisciplineGroup,
  DisciplineSemester,
  DisciplineTeacher,
} from "./discipline.models";
import { onMounted } from "vue";
import AccentSelect from "../Common/Components/AccentSelect.vue";

// форма для фильтрации списка дисциплин

const store = useDisciplinesStore();
const teachers: DisciplineTeacher[] = [];
const semesters: DisciplineSemester[] = [];
const groups: DisciplineGroup[] = [];

onMounted(() => {
  const disciplines: Discipline[] = store.disciplines;

  disciplines
    .map((d) => d.teacher)
    .filter((t) => t !== null)
    .forEach((teacher) => {
      if (!teachers.some((t) => t.name === teacher?.name)) {
        teachers.push(teacher);
      }
    });

  disciplines
    .map((d) => d.semester)
    .filter((s) => s !== null)
    .forEach((sem) => {
      if (!semesters.some((s) => s.number === sem.number)) {
        semesters.push(sem);
      }
    });

  disciplines
    .map((d) => d.group)
    .filter((g) => g !== null)
    .forEach((grp) => {
      if (!groups.some((g) => g.name === grp.name)) {
        groups.push(grp);
      }
    });
});
</script>

<template>
  <div class="filters-container card-primary rounded-md">
    <div class="text-responsive-primary font-semibold p-4">
      Фильтрация
    </div>
    <div class="filters-layout">
      <div class="filters-body">

        <InputWithIcon :class="'filters-item text-responsive-tertiary'" :label="'Дисциплина'" :place-holder="'название'"
          :icon="SearchIcon" />

        <AccentSelect :class="'filters-item'" :placeholder="'выбрать преподавателя'" :label="'Преподаватели'"
          :items="teachers" :item-key="(t) => t.id" :item-display-text="(t) => t.name" />

        <AccentSelect :class="'filters-item'" :placeholder="'выбрать группу'" :label="'Группы'" :items="groups"
          :item-key="(g) => g.id" :item-display-text="(g) => g.name" />

        <AccentSelect :class="'filters-item'" :placeholder="'выбрать семестр'" :label="'Семестр'" :items="semesters"
          :item-key="(s) => s.id" :item-display-text="(s) => s.number" />
      </div>
      <div :class="'checkbox-row-container'">
        <Field :orientation="'horizontal'">
          <Checkbox :id="'archived-filter'" :default-value="false" />
          <FieldContent>
            <FieldLabel :class="'text-responsive-tertiary'" for="archived-filter">
              Архивные дисциплины
            </FieldLabel>
          </FieldContent>
        </Field>
      </div>
      <div :class="'filters-button-container'">
        <Button :variant="'secondary'" :class="'mx-auto filters-button'">
          <Label :class="'text-responsive-tertiary font-prussian-blue'">Сбросить</Label>
        </Button>
      </div>
    </div>
  </div>
</template>

<style lang="css" scoped>
.filters-container {
  min-width: 0;
  display: flex;
  flex-direction: column;
  gap: 1em;
}

.filters-layout {
  display: flex;
  min-width: 0;
  flex-direction: column;
  align-items: center;
}

.filters-body {
  display: grid;
  grid-template-columns: 1 / 1;
  gap: 0.5em;
}

.filters-item {
  min-width: 0;
  display: flex;
  padding: 0 1em;
  gap: 1em;
}

.filters-item>* {
  min-width: 0;
}

.checkbox-row-container {
  display: flex;
  margin-top: 1em;
  justify-content: center;
}

.checkbox-row-container>* {
  width: fit-content;
}

.filters-button-container {
  width: 100%;
  display: flex;
  justify-content: center;
  padding: 0.5em;
}

.filters-button {
  padding: 0.5em;
  min-width: 75%;
  margin-bottom: 0.5em;
  height: auto !important;
}
</style>
