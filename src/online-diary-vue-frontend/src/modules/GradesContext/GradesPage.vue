<script lang="ts">
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import ODGradesPagePageTitle from "./components/OD-GradesPage-PageTitle.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import ODJournalDateBlock from "./components/OD-JournalDateBlock.vue";
import ODStudentsSearch from "./components/OD-StudentsSearch.vue";
import ODGradedThemesList from "./components/GradedThemesList/OD-GradedThemesList.vue";
import { ref, type Ref } from "vue";
import GradesTable from "./components/GradesTable/GradesTable.vue";
import { Item, ItemHeader, ItemMedia, ItemTitle } from "@/components/ui/item";
import ItemContent from "@/components/ui/item/ItemContent.vue";
import Table from "@/components/ui/table/Table.vue";
import { Label } from "@/components/ui/label";
import { Components } from "../Common/ComponentsLogic/Components";

type ThemeInfo = {
  index: number;
  date: Date;
};

type StudentInfo = {
  name: string;
  grades: number[];
};

const studentNames: string[] = [
  "Иванов Иван",
  "Петров Петр",
  "Сидоров Сидор",
  "Кузнецов Кузьма",
  "Смирнов Семён",
  "Попов Павел",
  "Васильев Василий",
  "Михайлов Михаил",
  "Новиков Николай",
  "Фёдоров Фёдор",
  "Григорьев Григорий",
  "Алексеев Алексей",
  "Сергеев Сергей",
  "Андреев Андрей",
  "Макаров Макар",
  "Никитин Никита",
  "Григорьев Григорий",
  "Егоров Егор",
  "Павлов Павел",
];

function generateRandomStudents(
  count: number,
  gradesPerStudent: number,
): StudentInfo[] {
  const students: StudentInfo[] = [];
  for (let i = 0; i < count; i++) {
    const student: StudentInfo = generateRandomStudent(gradesPerStudent);
    students.push(student);
  }
  return students;
}

function generateRandomStudent(gradesCount: number): StudentInfo {
  const maxStudents = studentNames.length;
  const randomIndex = Math.floor(Math.random() * maxStudents);
  const name = studentNames[randomIndex];
  const grades: number[] = [];
  for (let i = 0; i < gradesCount; i++) {
    const randomGrade = Math.floor(Math.random() * 5) + 1;
    grades.push(randomGrade);
  }
  return { name, grades };
}

function generateRandomThemes(count: number): ThemeInfo[] {
  const themes: ThemeInfo[] = [];
  const currentDate = new Date();
  for (let i = 0; i < count; i++) {
    const info: ThemeInfo = { date: currentDate, index: i + 1 };
    themes.push(info);
  }
  return themes;
}

export default {
  components: {
    Card,
    CardContent,
    CardTitle,
    Label,
    ODGradesPagePageTitle,
    ODJournalEditBlock,
    ODJournalDateBlock,
    ODStudentsSearch,
    ODGradedThemesList,
    GradesTable,
    Item,
    ItemContent,
    Table,
    ItemHeader,
    ItemTitle,
    ItemMedia,
  },
  data() {
    const beforeTableSectionWrapper: number = 0;
    return {
      date: new Date(Date.now()),
      tableHeaderStyles: "text-start p-0 border inline-block",
      gradeTableHeaderStyles: "text-center p-0 border inline-block",
      gradeThemeHeaderLabelStyles: "text-center p-0 border inline-block",
      beforeTableSectionWrapper: beforeTableSectionWrapper,
    };
  },
  mounted() {
    this.useTableContainerElementWidthObserver();
    this.initializeBeforeTableSectionWrapper();
  },
  methods: {
    disposeTableResizeObserver(): void {
      if (this.tableWidthResizeObserver) {
        this.tableWidthResizeObserver.disconnect();
        this.tableWidthResizeObserver = undefined;
      }
    },
    generateRandomStudent,
    generateRandomStudents,
    generateRandomThemes,
    initializeBeforeTableSectionWrapper(): void {
      const elementKey: string = "before-table-wrapper";
      const element: HTMLElement | null = Components.HTMLElementByRef(
        this,
        elementKey,
      );
      if (!element) return;
      const height: number = element.clientHeight;
      this.beforeTableSectionWrapper = height;
    },
    useTableContainerElementWidthObserver(): void {
      const elementKey: string = "tableContainerElementRef";
      const element = Components.HTMLElementByRef(this, elementKey);
      if (!element) return;
      if (this.tableWidthResizeObserver) return;
      const width = element.clientWidth;
      this.tableWidthResizeObserver = new ResizeObserver((entries) => {
        for (let entry of entries) {
          if (entry.target === element) {
            this.tableWrapperWidth = width;
          }
        }
      });
      this.tableWidthResizeObserver.observe(element);
    },
  },
  unmounted() {
    Components.DisposeResizeObserver(this.tableWidthResizeObserver);
  },
  setup() {
    const defaultWidth: number = 0;
    const tableWrapperWidth = ref(defaultWidth);
    const blockCellWidth: Ref<number, number> = ref(defaultWidth);
    const studentCellWidth: Ref<number, number> = ref(defaultWidth);
    const gradeScoreCellWidth: Ref<number, number> = ref(defaultWidth);
    const gradeValueCellWidth: Ref<number, number> = ref(defaultWidth);
    const tableWidthResizeObserver = ref<ResizeObserver | undefined>(undefined);
    return {
      tableWrapperWidth,
      blockCellWidth,
      studentCellWidth,
      gradeScoreCellWidth,
      gradeValueCellWidth,
      tableWidthResizeObserver,
    };
  },
};
</script>

<template>
  <section
    class="flex flex-col gap-2 my-0 p-2 h-screen"
    :ref="'tableContainerElementRef'"
  >
    <CardContent :class="'flex-1 px-2'">
      <section :class="'grid grid-cols-2 gap-2'" :ref="'before-table-wrapper'">
        <!-- edit journal items here -->
        <div :class="'grid grid-cols-2 gap-2'">
          <ODJournalEditBlock />
          <!-- редакторы -->
          <section :class="'col-span-2'">
            <ODJournalEditorsList />
          </section>
        </div>
        <!-- темы за которые выставлены оценки -->
        <ODGradedThemesList />
      </section>
      <!-- таблица оценок -->
      <div>
        <GradesTable
          :width-props="{
            blockCellWidth: blockCellWidth,
            studentCellWidth: studentCellWidth,
            gradeScoreCellWidth: gradeScoreCellWidth,
            gradeValueCellWidth: gradeValueCellWidth,
            tableWrapperWidth: tableWrapperWidth,
          }"
          :students="
            generateRandomStudents(10, 20).map((s) => ({
              name: s.name,
              grades: s.grades,
            }))
          "
          :themes="
            generateRandomThemes(20).map((t) => ({
              date: t.date,
              number: t.index,
            }))
          "
        >
        </GradesTable>
      </div>
    </CardContent>
  </section>
</template>
