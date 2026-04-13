<script setup lang="ts">
import type { GradingStudent } from "./gradebook.models";
import GradebookStudents from "./Gradebook/GradebookStudents.vue";
import GradebookTheme from "./Gradebook/GradebookTheme.vue";
import GradebookStudentGrades from "./Gradebook/GradebookStudentGrades.vue";
import { computed, nextTick, onBeforeUnmount, ref, watch } from "vue";
import ChangeStudentGradeDrawer from "./GradeStudentForm/ChangeStudentGradeDrawer.vue";
import { useGradebookStore } from "./gradebook.store";
import { toast, ToastContainer } from "vue-toastflow";

const store = useGradebookStore();
const students = computed(() => store.students);
const themes = computed(() => store.themes);
const studentsScroller = ref<HTMLElement | null>(null);
const gradesScroller = ref<HTMLElement | null>(null);
const pendingStudentsScrollTop = ref(0);
let studentsScrollFrame: number | null = null;

const gradebookStyle = computed(() => ({
  gridTemplateColumns: `repeat(${themes.value.length}, var(--gradebook-grade-column))`,
  gridTemplateRows: `var(--gradebook-header-height) repeat(${students.value.length}, var(--gradebook-row-height))`,
}));

const studentsTrackStyle = computed(() => ({
  gridTemplateRows: `var(--gradebook-header-height) repeat(${students.value.length}, var(--gradebook-row-height))`,
}));

watch(
  () => store.activeThemeIndex,
  async (themeIndex) => {
    if (!themeIndex) return;
    await nextTick();

    const target = gradesScroller.value?.querySelector<HTMLElement>(
      `[data-theme-index="${themeIndex}"]`,
    );

    target?.scrollIntoView({
      behavior: "smooth",
      block: "nearest",
      inline: "center",
    });
  },
  { flush: "post" },
);

function invokeStudentGradedToast(gradingStudent: GradingStudent): void {
  const name: string = gradingStudent.student.name;
  const theme: string =
    "Тема " + gradingStudent.theme.date.toLocaleDateString();
  const gradeValue: string = gradingStudent.grade.gradeValue ?? "Нет оценки";
  const description: string = `Оценка студента ${name} по теме ${theme} обновлена на ${gradeValue}`;
  toast.success({
    title: "Оценка обновлена",
    description: description,
  });
}

function selectTheme(themeIndex: number): void {
  store.selectTheme(themeIndex);
}

function syncVerticalScroll(): void {
  const sourceEl = gradesScroller.value;
  const targetEl = studentsScroller.value;

  if (!sourceEl || !targetEl) return;

  pendingStudentsScrollTop.value = sourceEl.scrollTop;

  if (studentsScrollFrame !== null) {
    return;
  }

  studentsScrollFrame = requestAnimationFrame(() => {
    studentsScrollFrame = null;

    if (!studentsScroller.value) return;
    if (studentsScroller.value.scrollTop === pendingStudentsScrollTop.value) return;

    studentsScroller.value.scrollTop = pendingStudentsScrollTop.value;
  });
}

function preventStudentsWheel(event: WheelEvent): void {
  event.preventDefault();
}

onBeforeUnmount(() => {
  if (studentsScrollFrame !== null) {
    cancelAnimationFrame(studentsScrollFrame);
  }
});
</script>

<template>
  <section class="gradebook-shell">
    <div class="gradebook-content card-primary shadow-none!">
      <div class="gradebook-layout">
        <div ref="studentsScroller" class="gradebook-students-scroller" @wheel.prevent="preventStudentsWheel">
          <div class="gradebook-students-track" :style="studentsTrackStyle">
            <div class="gradebook-students-spacer" aria-hidden="true"></div>
            <GradebookStudents v-for="student in students" :key="'student-' + student.id" :student="student" />
          </div>
        </div>

        <div ref="gradesScroller" class="gradebook-grades-scroller" @scroll="syncVerticalScroll">
          <div class="gradebook-centerer">
            <div class="gradebook-grid" :style="gradebookStyle">
              <GradebookTheme v-for="theme in themes" :key="'header-' + theme.index" :theme="theme"
                :active="theme.index === store.activeThemeIndex" @select="selectTheme" />

              <template v-for="student in students" :key="student.id">
                <GradebookStudentGrades :student="student" />
              </template>
            </div>
          </div>
        </div>
      </div>
    </div>

    <ChangeStudentGradeDrawer @student-graded="invokeStudentGradedToast" />
    <ToastContainer />
  </section>
</template>

<style scoped lang="css">
.gradebook-shell {
  --gradebook-student-column: clamp(11.3rem, 15vw, 14.2rem);
  --gradebook-grade-column: clamp(4rem, 4.5vw, 4.6rem);
  --gradebook-row-height: clamp(2.8rem, 3.5vw, 3.35rem);
  --gradebook-header-height: 4.95rem;
  height: 100%;
  min-height: 0;
  min-width: 0;
  overflow: hidden;
  display: grid;
  grid-template-rows: minmax(0, 1fr);
}

.gradebook-content {
  position: relative;
  min-width: 0;
  min-height: 0;
  height: 100%;
  overflow: hidden;
  border: 1px solid hsl(0 0% 100% / 0.04);
  border-radius: 1.25rem;
  padding: 0.25rem;
}

.gradebook-layout {
  height: 100%;
  min-width: 0;
  min-height: 0;
  display: grid;
  grid-template-columns: var(--gradebook-student-column) minmax(0, 1fr);
  gap: 0.4rem;
}

.gradebook-students-scroller,
.gradebook-grades-scroller {
  height: 100%;
  min-width: 0;
  min-height: 0;
  overscroll-behavior: contain;
}

.gradebook-students-scroller {
  overflow: hidden;
  scrollbar-width: none;
  -ms-overflow-style: none;
}

.gradebook-students-scroller::-webkit-scrollbar {
  display: none;
}

.gradebook-grades-scroller {
  overflow: auto;
  scrollbar-gutter: stable both-edges;
}

.gradebook-students-track {
  display: grid;
  width: var(--gradebook-student-column);
  gap: 0.4rem;
  padding: 0.25rem;
  box-sizing: border-box;
}

.gradebook-students-spacer {
  position: sticky;
  top: 0;
  z-index: 22;
  min-height: 100%;
  border-radius: 0.9rem;
}

.gradebook-centerer {
  width: max-content;
  min-width: 100%;
  margin-inline: auto;
  box-sizing: border-box;
  padding: 0.25rem;
}

.gradebook-grid {
  display: grid;
  gap: 0.4rem;
  width: max-content;
  align-content: start;
}

@media (max-width: 1100px) {
  .gradebook-shell {
    --gradebook-student-column: 10.6rem;
    --gradebook-grade-column: 3.9rem;
    --gradebook-header-height: 4.75rem;
  }
}

@media (max-width: 720px) {
  .gradebook-shell {
    --gradebook-student-column: 10rem;
    --gradebook-grade-column: 3.75rem;
    --gradebook-header-height: 4.5rem;
  }

  .gradebook-content {
    padding: 0.75rem;
  }

  .gradebook-layout {
    gap: 0.35rem;
  }
}
</style>
