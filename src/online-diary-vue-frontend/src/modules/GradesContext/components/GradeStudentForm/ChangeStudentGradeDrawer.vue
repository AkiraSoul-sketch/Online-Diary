<script setup lang="ts">
import ButtonGroup from "@/components/ui/button-group/ButtonGroup.vue";
import Button from "@/components/ui/button/Button.vue";
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Drawer from "@/components/ui/drawer/Drawer.vue";
import DrawerContent from "@/components/ui/drawer/DrawerContent.vue";
import {
  GradeValues,
  type GradingStudent,
} from "../gradebook.models";
import { useGradebookStore } from "../gradebook.store";
import ChangeStudentGradeDrawerTitle from "./ChangeStudentGradeDrawerTitle.vue";
import ChangeStudentGradeDrawerFooter from "./ChangeStudentGradeDrawerFooter.vue";
import ChangeStudentGradeCurrentStatus from "./ChangeStudentGradeCurrentStatus.vue";

const store = useGradebookStore();

const emits = defineEmits<{
  (e: "studentGraded", student: GradingStudent): void;
}>();

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
</script>

<template>
  <Drawer :fixed="true" :direction="'bottom'" :open="store.gradingStudentValue !== null">
    <DrawerContent :class="'card-primary'">
      <Card :class="'border-0 shadow-none flex items-center-safe bg-transparent'">
        <ChangeStudentGradeDrawerTitle />
        <CardTitle :class="'text-responsive-primary'"> Изменение оценки </CardTitle>
        <ChangeStudentGradeCurrentStatus />
        <CardContent :class="'flex flex-col gap-5 justify-center items-center'">
          <ButtonGroup>
            <Button :class="'text-responsive-secondary'"
              v-on:click="gradeStudent(GradeValues.GradeNeudovletvoritelno())">
              2
            </Button>
            <Button :class="'text-responsive-secondary'" v-on:click="gradeStudent(GradeValues.GradeUdovletvoritelno())">
              3
            </Button>
            <Button :class="'text-responsive-secondary'" v-on:click="gradeStudent(GradeValues.GradeKhorosho())">
              4
            </Button>
            <Button :class="'text-responsive-secondary'" v-on:click="gradeStudent(GradeValues.GradeOtlichno())">
              5
            </Button>
          </ButtonGroup>
          <ButtonGroup>
            <Button :class="'text-responsive-secondary'" v-on:click="gradeStudent(GradeValues.GradeNeYavilos())">
              Не было
            </Button>
            <Button :class="'text-responsive-secondary'" v-on:click="gradeStudent(GradeValues.GradeUvajitelno())">
              Ув. причина
            </Button>
          </ButtonGroup>
        </CardContent>
        <ChangeStudentGradeDrawerFooter />
      </Card>
    </DrawerContent>
  </Drawer>
</template>
