<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Drawer from "@/components/ui/drawer/Drawer.vue";
import DrawerContent from "@/components/ui/drawer/DrawerContent.vue";
import { useGradebookLogic } from "../gradebook.logic";
import { GradeValues, type GradingStudent } from "../gradebook.models";
import { useGradebookStore } from "../gradebook.store";
import ChangeStudentGradeDrawerTitle from "./ChangeStudentGradeDrawerTitle.vue";
import ChangeStudentGradeDrawerFooter from "./ChangeStudentGradeDrawerFooter.vue";
import ChangeStudentGradeCurrentStatus from "./ChangeStudentGradeCurrentStatus.vue";

const store = useGradebookStore();
const { resolveGradebookColorValue } = useGradebookLogic();

const emits = defineEmits<{
  (e: "studentGraded", student: GradingStudent): void;
}>();

const numericGradeOptions = [
  { label: "2", value: GradeValues.GradeNeudovletvoritelno() },
  { label: "3", value: GradeValues.GradeUdovletvoritelno() },
  { label: "4", value: GradeValues.GradeKhorosho() },
  { label: "5", value: GradeValues.GradeOtlichno() },
];

const statusGradeOptions = [
  { label: "Не было", value: GradeValues.GradeNeYavilos() },
  { label: "Ув. причина", value: GradeValues.GradeUvajitelno() },
  { label: "Не атт.", value: GradeValues.GradeNeAttestovan() },
];

function gradeStudent(gradeValue: string): void {
  const selectedStudent: GradingStudent | null = store.gradingStudentValue;
  if (selectedStudent === null) return;
  const clone: GradingStudent = {
    ...selectedStudent,
    grade: { ...selectedStudent.grade, gradeValue },
  };
  store.gradeStudent(gradeValue);
  emits("studentGraded", clone);
}

function gradeOptionStyle(gradeValue: string): { backgroundColor: string } {
  return {
    backgroundColor: resolveGradebookColorValue(gradeValue),
  };
}
</script>

<template>
  <Drawer
    :fixed="true"
    :direction="'bottom'"
    :open="store.gradingStudentValue !== null"
  >
    <DrawerContent data-vaul-no-drag :class="'card-primary change-grade-drawer'">
      <Card
        :class="'change-grade-drawer__card border-0 shadow-none bg-transparent'"
      >
        <ChangeStudentGradeDrawerTitle />
        <CardTitle :class="'text-responsive-primary'">
          Изменение оценки
        </CardTitle>
        <ChangeStudentGradeCurrentStatus />
        <CardContent :class="'change-grade-drawer__content'">
          <div class="change-grade-drawer__grid change-grade-drawer__grid--numeric">
            <Button
              v-for="option in numericGradeOptions"
              :key="option.value"
              class="change-grade-drawer__button"
              :variant="'ghost'"
              v-on:click="gradeStudent(option.value)"
            >
              <span
                class="change-grade-drawer__button-value text-responsive-secondary"
                :style="gradeOptionStyle(option.value)"
              >
                {{ option.label }}
              </span>
            </Button>
          </div>

          <div class="change-grade-drawer__grid change-grade-drawer__grid--status">
            <Button
              v-for="option in statusGradeOptions"
              :key="option.value"
              class="change-grade-drawer__button change-grade-drawer__button--status"
              :variant="'ghost'"
              v-on:click="gradeStudent(option.value)"
            >
              <span
                class="change-grade-drawer__button-value change-grade-drawer__button-value--status text-responsive-secondary"
                :style="gradeOptionStyle(option.value)"
              >
                {{ option.label }}
              </span>
            </Button>
          </div>
        </CardContent>
        <ChangeStudentGradeDrawerFooter />
      </Card>
    </DrawerContent>
  </Drawer>
</template>

<style scoped lang="css">
.change-grade-drawer {
  max-height: 88svh;
  padding: 0.9rem 0.9rem 1rem;
}

.change-grade-drawer__card {
  display: grid;
  justify-items: center;
  gap: 0.9rem;
  padding-inline: 0.15rem;
}

.change-grade-drawer__content {
  display: grid;
  width: min(100%, 32rem);
  gap: 0.85rem;
  padding: 0;
}

.change-grade-drawer__grid {
  display: grid;
  gap: 0.65rem;
}

.change-grade-drawer__grid--numeric {
  grid-template-columns: repeat(4, minmax(0, 1fr));
}

.change-grade-drawer__grid--status {
  grid-template-columns: repeat(3, minmax(0, 1fr));
}

.change-grade-drawer__button {
  min-height: 3rem;
  width: 100%;
  border: 1px solid hsl(0 0% 100% / 0.05);
  border-radius: 0.88rem;
  background:
    linear-gradient(180deg, hsl(220 12% 25% / 0.94), hsl(220 12% 19% / 0.98)),
    var(--bg-primary-accent-2);
  padding: 0.35rem;
  box-shadow: var(--shadow-primary);
}

.change-grade-drawer__button--status {
  white-space: normal;
}

.change-grade-drawer__button-value {
  display: grid;
  min-height: 100%;
  min-width: 100%;
  place-items: center;
  border-radius: 0.62rem;
  padding: 0.55rem;
  font-weight: 600;
  line-height: 1.1;
  color: var(--fg-primary);
}

.change-grade-drawer__button-value--status {
  text-align: center;
}

@media (max-width: 640px) {
  .change-grade-drawer__grid--numeric,
  .change-grade-drawer__grid--status {
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }
}
</style>
