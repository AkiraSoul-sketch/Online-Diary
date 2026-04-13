<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import {
  Drawer,
  DrawerContent,
  DrawerHeader,
  DrawerTitle,
} from "@/components/ui/drawer";
import CloseButton from "@/modules/Common/Components/CloseButton.vue";
import { ChevronLeftIcon, ChevronRightIcon, ListIcon } from "lucide-vue-next";
import { computed, ref } from "vue";
import type { Grade, GradingStudent, StudentInfo, ThemeInfo } from "./gradebook.models";
import ChangeStudentGradeDrawer from "./GradeStudentForm/ChangeStudentGradeDrawer.vue";
import GradebookThemesList from "./Gradebook/GradebookThemesList.vue";
import GradebookTools from "./Gradebook/GradebookTools.vue";
import { useGradebookLogic } from "./gradebook.logic";
import { useGradebookStore } from "./gradebook.store";
import { toast, ToastContainer } from "vue-toastflow";

type MobileStudentGrade = {
  student: StudentInfo;
  grade: Grade | null;
};

const store = useGradebookStore();
const { formatThemeDate, resolveGradebookColor } = useGradebookLogic();

const controlsDrawerOpen = ref(false);
const themesDrawerOpen = ref(false);

const activeTheme = computed<ThemeInfo | null>(() => {
  const currentIndex = store.activeThemeIndex;
  if (currentIndex === null) {
    return store.themes[0] ?? null;
  }

  return store.themes.find((theme) => theme.index === currentIndex) ?? null;
});

const activeThemePosition = computed<number>(() => {
  if (!activeTheme.value) return -1;
  return store.themes.findIndex((theme) => theme.index === activeTheme.value?.index);
});

const canSelectPreviousTheme = computed<boolean>(() => activeThemePosition.value > 0);
const canSelectNextTheme = computed<boolean>(
  () => activeThemePosition.value !== -1 && activeThemePosition.value < store.themes.length - 1,
);

const studentsForTheme = computed<MobileStudentGrade[]>(() => {
  const currentTheme = activeTheme.value;
  if (!currentTheme) return [];

  return store.students.map((student) => ({
    student,
    grade: student.grades.find((grade) => grade.theme === currentTheme.index) ?? null,
  }));
});

function openControlsDrawer(): void {
  controlsDrawerOpen.value = true;
}

function closeControlsDrawer(): void {
  controlsDrawerOpen.value = false;
}

function openThemesDrawer(): void {
  themesDrawerOpen.value = true;
}

function closeThemesDrawer(): void {
  themesDrawerOpen.value = false;
}

function selectPreviousTheme(): void {
  if (!canSelectPreviousTheme.value) return;
  const nextTheme = store.themes[activeThemePosition.value - 1];
  if (!nextTheme) return;
  store.selectTheme(nextTheme.index);
}

function selectNextTheme(): void {
  if (!canSelectNextTheme.value) return;
  const nextTheme = store.themes[activeThemePosition.value + 1];
  if (!nextTheme) return;
  store.selectTheme(nextTheme.index);
}

function pickStudentGrade(grade: Grade | null): void {
  if (!grade) return;
  store.pickGradingStudent(grade);
}

function mobileStudentName(student: StudentInfo): string {
  const parts = student.name.split(" ");
  if (parts.length < 3) return student.name;
  return `${parts[0]} ${parts[1][0]}. ${parts[2][0]}.`;
}

function gradeBadgeText(grade: Grade | null): string {
  return grade?.gradeValue ?? "—";
}

function gradeBadgeStyle(grade: Grade | null): { backgroundColor: string } {
  return {
    backgroundColor: grade ? resolveGradebookColor(grade) : "var(--panel-bg)",
  };
}

function invokeStudentGradedToast(gradingStudent: GradingStudent): void {
  const name: string = gradingStudent.student.name;
  const theme: string = `Тема ${gradingStudent.theme.index}`;
  const gradeValue: string = gradingStudent.grade.gradeValue ?? "Нет оценки";
  const description: string = `Оценка студента ${name} по теме ${theme} обновлена на ${gradeValue}`;
  toast.success({
    title: "Оценка обновлена",
    description,
  });
}
</script>

<template>
  <section class="gradebook-mobile">
    <header class="gradebook-mobile__summary card-primary shadow-none!">
      <div class="gradebook-mobile__summary-copy">
        <span class="gradebook-mobile__eyebrow">Журнал</span>
        <div class="gradebook-mobile__summary-chips">
          <span class="gradebook-mobile__summary-chip">Группа</span>
          <span class="gradebook-mobile__summary-chip">Дисциплина</span>
        </div>
      </div>

      <Button class="gradebook-mobile__summary-button" :variant="'secondary'" @click="openControlsDrawer">
        Параметры
      </Button>
    </header>

    <section class="gradebook-mobile__theme-nav card-primary shadow-none!">
      <Button :variant="'outline'" :size="'icon'" class="gradebook-mobile__theme-arrow"
        :disabled="!canSelectPreviousTheme" @click="selectPreviousTheme">
        <ChevronLeftIcon />
      </Button>

      <button class="gradebook-mobile__theme-card" type="button" :disabled="!activeTheme" @click="openThemesDrawer">
        <span class="gradebook-mobile__eyebrow">Текущая тема</span>
        <strong class="gradebook-mobile__theme-title">
          {{ activeTheme ? `Тема ${activeTheme.index}` : "Темы не загружены" }}
        </strong>
        <span class="gradebook-mobile__theme-meta">
          {{ activeTheme ? `${formatThemeDate(activeTheme.date)} · ${activeTheme.title}` : "Выберите тему" }}
        </span>
      </button>

      <Button :variant="'outline'" :size="'icon'" class="gradebook-mobile__theme-arrow" :disabled="!canSelectNextTheme"
        @click="selectNextTheme">
        <ChevronRightIcon />
      </Button>
    </section>

    <section class="gradebook-mobile__students card-primary shadow-none!">
      <div class="gradebook-mobile__students-header">
        <div class="gradebook-mobile__students-copy">
          <span class="gradebook-mobile__students-title">Оценивание</span>
          <span class="gradebook-mobile__students-caption">Нажмите на студента, чтобы изменить оценку.</span>
        </div>

        <Button class="gradebook-mobile__students-button" :variant="'tetriary'" @click="openThemesDrawer">
          <ListIcon />
          <span>Темы</span>
        </Button>
      </div>

      <div class="gradebook-mobile__students-list">
        <button v-for="item in studentsForTheme" :key="item.student.id" class="gradebook-mobile__student-row"
          type="button" @click="pickStudentGrade(item.grade)">
          <div class="gradebook-mobile__student-copy" :title="item.student.name">
            <strong class="gradebook-mobile__student-name">{{ mobileStudentName(item.student) }}</strong>
            <span class="gradebook-mobile__student-caption">Нажмите, чтобы изменить оценку</span>
          </div>

          <span class="gradebook-mobile__student-grade" :style="gradeBadgeStyle(item.grade)">
            {{ gradeBadgeText(item.grade) }}
          </span>
        </button>
      </div>
    </section>

    <Drawer :open="controlsDrawerOpen" :direction="'bottom'" :dismissible="false"
      @update:open="controlsDrawerOpen = $event">
      <DrawerContent data-vaul-no-drag class="gradebook-mobile__drawer card-primary"
        @interact-outside="closeControlsDrawer">
        <DrawerHeader class="gradebook-mobile__drawer-header">
          <DrawerTitle class="gradebook-mobile__drawer-title">Параметры журнала</DrawerTitle>
          <div class="gradebook-mobile__drawer-close">
            <CloseButton :onClick="closeControlsDrawer" />
          </div>
        </DrawerHeader>

        <div class="gradebook-mobile__drawer-body">
          <GradebookTools :embedded="true" />
        </div>
      </DrawerContent>
    </Drawer>

    <Drawer :open="themesDrawerOpen" :direction="'bottom'" :dismissible="false"
      @update:open="themesDrawerOpen = $event">
      <DrawerContent data-vaul-no-drag class="gradebook-mobile__drawer gradebook-mobile__drawer--themes card-primary"
        @interact-outside="closeThemesDrawer">
        <DrawerHeader class="gradebook-mobile__drawer-header">
          <DrawerTitle class="gradebook-mobile__drawer-title">Темы журнала</DrawerTitle>
          <div class="gradebook-mobile__drawer-close">
            <CloseButton :onClick="closeThemesDrawer" />
          </div>
        </DrawerHeader>

        <div class="gradebook-mobile__drawer-body">
          <GradebookThemesList :embedded="true" :drawer-mode="true" @theme-picked="closeThemesDrawer" />
        </div>
      </DrawerContent>
    </Drawer>

    <ChangeStudentGradeDrawer @student-graded="invokeStudentGradedToast" />
    <ToastContainer />
  </section>
</template>

<style scoped lang="css">
.gradebook-mobile {
  height: 100%;
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-rows: auto auto minmax(0, 1fr);
  gap: 0.75rem;
  padding: 0.85rem;
}

.gradebook-mobile__summary,
.gradebook-mobile__theme-nav,
.gradebook-mobile__students {
  border: 1px solid var(--panel-border-color);
  border-radius: 1.15rem;
}

.gradebook-mobile__summary {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.75rem;
  padding: 0.85rem;
}

.gradebook-mobile__summary-copy {
  display: grid;
  gap: 0.4rem;
  min-width: 0;
}

.gradebook-mobile__eyebrow {
  font-size: 0.72rem;
  line-height: 1;
  font-weight: 700;
  color: var(--panel-text-color);
  opacity: 0.62;
  text-transform: uppercase;
  letter-spacing: 0.08em;
}

.gradebook-mobile__summary-chips {
  display: flex;
  flex-wrap: wrap;
  gap: 0.45rem;
}

.gradebook-mobile__summary-chip {
  display: inline-flex;
  align-items: center;
  min-height: 1.9rem;
  border: 1px solid var(--panel-border-color);
  border-radius: 999px;
  background: var(--panel-bg);
  padding-inline: 0.65rem;
  font-size: 0.82rem;
  color: var(--panel-text-color);
  opacity: 0.82;
}

.gradebook-mobile__summary-button {
  min-width: fit-content;
  height: 2.55rem;
  padding-inline: 0.9rem;
}

.gradebook-mobile__theme-nav {
  display: grid;
  grid-template-columns: auto minmax(0, 1fr) auto;
  align-items: center;
  gap: 0.6rem;
  padding: 0.7rem;
}

.gradebook-mobile__theme-arrow {
  flex-shrink: 0;
}

.gradebook-mobile__theme-card {
  display: grid;
  gap: 0.22rem;
  min-width: 0;
  border: 1px solid var(--panel-border-color);
  border-radius: 0.95rem;
  background: var(--panel-bg);
  padding: 0.7rem 0.85rem;
  text-align: left;
}

.gradebook-mobile__theme-title {
  font-size: 0.95rem;
  line-height: 1.15;
}

.gradebook-mobile__theme-meta {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.8rem;
  line-height: 1.2;
  color: var(--panel-text-color);
  opacity: 0.7;
}

.gradebook-mobile__students {
  min-height: 0;
  display: grid;
  grid-template-rows: auto minmax(0, 1fr);
  overflow: hidden;
}

.gradebook-mobile__students-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.75rem;
  padding: 0.85rem 0.85rem 0.65rem;
}

.gradebook-mobile__students-copy {
  display: grid;
  gap: 0.18rem;
}

.gradebook-mobile__students-title {
  font-size: 0.95rem;
  line-height: 1.1;
  font-weight: 700;
}

.gradebook-mobile__students-caption {
  font-size: 0.78rem;
  line-height: 1.25;
  color: var(--panel-text-color);
  opacity: 0.68;
}

.gradebook-mobile__students-button {
  flex-shrink: 0;
  min-height: 2.4rem;
  padding-inline: 0.85rem;
}

.gradebook-mobile__students-list {
  min-height: 0;
  overflow-y: auto;
  display: grid;
  align-content: start;
  gap: 0.55rem;
  padding: 0 0.85rem 0.85rem;
}

.gradebook-mobile__student-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.75rem;
  min-height: 4.1rem;
  border: 1px solid var(--panel-border-color);
  border-radius: 1rem;
  background: var(--panel-bg-gradient), var(--panel-bg);
  padding: 0.8rem 0.9rem;
  text-align: left;
}

.gradebook-mobile__student-copy {
  display: grid;
  gap: 0.18rem;
  min-width: 0;
}

.gradebook-mobile__student-name {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.94rem;
  line-height: 1.2;
}

.gradebook-mobile__student-caption {
  font-size: 0.76rem;
  line-height: 1.2;
  color: var(--panel-text-color);
  opacity: 0.7;
}

.gradebook-mobile__student-grade {
  display: inline-grid;
  min-width: 3rem;
  min-height: 3rem;
  place-items: center;
  border-radius: 0.85rem;
  padding: 0.4rem;
  font-size: 1rem;
  font-weight: 700;
  color: var(--fg-primary);
}

.gradebook-mobile__drawer {
  display: flex;
  max-height: 90svh;
  flex-direction: column;
  gap: 0.75rem;
  border-top-left-radius: 1.35rem;
  border-top-right-radius: 1.35rem;
  padding: 1.35rem 1rem 1rem;
}

.gradebook-mobile__drawer--themes {
  height: 92svh;
  max-height: 92svh;
}

.gradebook-mobile__drawer-header {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 0.35rem 2.75rem 0.45rem;
}

.gradebook-mobile__drawer-title {
  text-align: center;
  font-size: 1rem;
  line-height: 1.1;
}

.gradebook-mobile__drawer-close {
  position: absolute;
  top: 0.25rem;
  right: 0.25rem;
}

.gradebook-mobile__drawer-body {
  display: grid;
  min-height: 0;
  flex: 1;
  overflow: hidden;
  padding-top: 0.2rem;
}

@media (max-width: 420px) {
  .gradebook-mobile {
    padding: 0.75rem;
  }

  .gradebook-mobile__summary {
    align-items: flex-start;
    flex-direction: column;
  }

  .gradebook-mobile__summary-button {
    width: 100%;
  }

  .gradebook-mobile__students-header {
    align-items: flex-start;
    flex-direction: column;
  }

  .gradebook-mobile__students-button {
    width: 100%;
  }
}
</style>
