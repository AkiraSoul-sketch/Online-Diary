<script setup lang="ts">
import { onMounted, ref, type Ref } from "vue";
import { useElementSizeObservabilityV2 } from "../Common/Composables/useElementSizeObservabilityV2";
import Gradebook from "./components/Gradebook.vue";
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import InputWithIcon from "../Common/Components/InputWithIcon.vue";
import {
  ChevronDownIcon,
  Grid2X2PlusIcon,
  LockIcon,
  SaveIcon,
  SearchIcon,
} from "lucide-vue-next";
import Button from "@/components/ui/button/Button.vue";
import type { DateValue } from "reka-ui";
import { CalendarDate } from "@internationalized/date";
import Popover from "@/components/ui/popover/Popover.vue";
import PopoverTrigger from "@/components/ui/popover/PopoverTrigger.vue";
import PopoverContent from "@/components/ui/popover/PopoverContent.vue";
import Calendar from "@/components/ui/calendar/Calendar.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import type {
  ThemeInfo,
  Grade,
  StudentInfo,
} from "./components/gradebook.models";
import { useGradebookLogic } from "./components/gradebook.logic";

const date: Date = new Date(Date.now());

const studentNames: string[] = [
  "Иванов Иван Иванович",
  "Петров Петр Петрович",
  "Сидоров Сидор Сидорович",
  "Кузнецов Кузьма Кузьмич",
  "Смирнов Семён Семёнович",
  "Попов Павел Павлович",
  "Васильев Василий Васильевич",
  "Михайлов Михаил Михайлович",
  "Новиков Николай Николаевич",
  "Фёдоров Фёдор Фёдорович",
  "Григорьев Григорий Григорьевич",
  "Алексеев Алексей Алексеевич",
  "Сергеев Сергей Сергеевич",
  "Андреев Андрей Андреевич",
  "Макаров Макар Макарович",
  "Никитин Никита Никитич",
  "Григорьев Григорий Григорьевич",
  "Егоров Егор Егорович",
  "Павлов Павел Павлович",
];

function generateRandomStudents(
  count: number,
  themes: ThemeInfo[],
): StudentInfo[] {
  const students: StudentInfo[] = [];
  for (let i = 0; i < count; i++) {
    const student: StudentInfo = generateRandomStudent(i, themes);
    students.push(student);
  }
  return students;
}

const logic = useGradebookLogic();
const gradesList: string[] = ["2", "3", "4", "5", "У", "Н", "НА"];

function generateRandomStudent(
  studentId: number,
  themes: ThemeInfo[],
): StudentInfo {
  const name = generateRandomStudentName();
  const grades: Grade[] = [];
  for (const theme of themes) {
    const grade: Grade = {
      theme: theme.index,
      student: studentId,
      gradeValue: generateRandomGradeValue(),
    };
    grades.push(grade);
  }

  return {
    id: studentId,
    name,
    grades,
  };
}

function generateRandomStudentName(): string {
  const maxStudents = studentNames.length;
  const randomIndex = Math.floor(Math.random() * maxStudents);
  const name = studentNames[randomIndex];

  return name;
}

function generateRandomGradeValue(): string {
  const listLength: number = gradesList.length;
  const randomNumber: number = Math.random();
  const randomIndex: number = Math.floor(randomNumber * listLength);
  const randomGrade: string = gradesList[randomIndex];

  return randomGrade;
}

function generateRandomThemes(count: number): ThemeInfo[] {
  const themes: ThemeInfo[] = [];
  for (let i = 0; i < count; i++) {
    const info: ThemeInfo = { date: date, index: i + 1 };
    themes.push(info);
  }
  return themes;
}

function convertDateToCalendarDate(date: Date): DateValue {
  const day: number = date.getDay();
  const month: number = date.getMonth();
  const year: number = date.getFullYear();
  const calendarDate = new CalendarDate(year, month, day);
  return calendarDate;
}

const students: Ref<StudentInfo[]> = ref([]);
const themes: Ref<ThemeInfo[]> = ref([]);
const container = useElementSizeObservabilityV2();

onMounted(() => {
  themes.value = generateRandomThemes(25);
  students.value = generateRandomStudents(20, themes.value);
});
</script>

<template>
  <section
    class="flex flex-col gap-2 flex-1 min-h-0 min-w-0 bg-zinc-200 p-2 h-full w-full"
    :ref="container.element"
  >
    <ODJournalEditBlock />
    <Card :class="'shadow-(--shadow-basic) p-2'">
      <CardContent :class="'flex gap-2 flex-row'">
        <InputWithIcon
          :class="'w-full'"
          :place-holder="'Поиск студента'"
          :icon="SearchIcon"
        />
        <Button :class="'p-1 w-auto'" :size="'icon'">
          <Grid2X2PlusIcon />
          <label :class="'hidden md:inline'">Создать колонку</label>
        </Button>
        <Button :class="'p-1 w-auto'" :size="'icon'">
          <LockIcon />
          <label :class="'hidden md:inline'">Создать колонку</label>
        </Button>
        <Button :class="'p-1 w-auto'" :size="'icon'">
          <SaveIcon />
          <label :class="'hidden md:inline'">Сохранить</label>
        </Button>
        <Popover :class="'w-auto'">
          <PopoverTrigger :as-child="true">
            <Button
              :class="'shadow-sm border-(--text) text(--text)'"
              :variant="'outline'"
            >
              <label>Выбрать дату</label>
              <ChevronDownIcon />
            </Button>
          </PopoverTrigger>
          <PopoverContent>
            <Calendar :default-value="convertDateToCalendarDate(date)" />
          </PopoverContent>
        </Popover>
      </CardContent>
    </Card>
    <Gradebook
      :students="students"
      :themes="themes"
      :containerWidth="container.width.value"
    />
  </section>
</template>
