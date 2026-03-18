<script lang="ts">
import { Label } from "@/components/ui/label";
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import ODGradesPagePageTitle from "./components/OD-GradesPage-PageTitle.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import ODJournalDateBlock from "./components/OD-JournalDateBlock.vue";
import ODStudentsSearch from "./components/OD-StudentsSearch.vue";
import ODGradedThemesList from "./components/GradedThemesList/OD-GradedThemesList.vue";
import { ref, type Ref } from "vue";
import GradesTable from "./components/GradesTable/GradesTable.vue";

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
  },
  data() {
    return {
      date: new Date(Date.now()),
      tableHeaderStyles: "text-start p-0 border inline-block",
      gradeTableHeaderStyles: "text-center p-0 border inline-block",
      gradeThemeHeaderLabelStyles: "text-center p-0 border inline-block",
    };
  },
  mounted() {
    this.useTableContainerElementWidthObserver();
  },
  methods: {
    generateRandomStudent,
    generateRandomStudents,
    generateRandomThemes,
    useTableContainerElementWidthObserver(): void {
      const refs = this.$refs;
      const tableContainerKey: string = "tableContainerElementRef";
      const element = refs[tableContainerKey] as HTMLElement | undefined;
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
  <section class="flex gap-0 p-0 mx-2">
    <!-- // page container -->
    <Card :class="'h-full w-full gap-2 my-0'">
      <!-- page title -->
      <div :ref="'tableContainerElementRef'">
        <ODGradesPagePageTitle />
      </div>
      <CardContent :class="'px-2'">
        <section class="grid grid-cols-2 gap-2">
          <!-- edit journal items here -->
          <div :class="'grid grid-cols-2 gap-2'">
            <ODJournalEditBlock />
            <!-- мини информация -->
            <!-- Дата -->
            <ODJournalDateBlock />
            <!-- Placeholder временно -->
            <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
              <CardTitle>
                <Label :class="'text-xl'">Placeholder</Label>
                <Label :class="'text-xl'"></Label>
              </CardTitle>
            </Card>
            <!-- поиск студента -->
            <ODStudentsSearch />
          </div>
          <!-- темы за которые выставлены оценки -->
          <ODGradedThemesList />
        </section>
        <!-- таблица оценок -->
        <GradesTable
          :width-props="{
            blockCellWidth: blockCellWidth,
            studentCellWidth: studentCellWidth,
            gradeScoreCellWidth: gradeScoreCellWidth,
            gradeValueCellWidth: gradeValueCellWidth,
            tableWrapperWidth: tableWrapperWidth,
          }"
          :students="
            generateRandomStudents(10, 25).map((s) => ({
              name: s.name,
              grades: s.grades,
            }))
          "
          :themes="
            generateRandomThemes(25).map((t) => ({
              date: t.date,
              number: t.index,
            }))
          "
        >
        </GradesTable>
      </CardContent>
    </Card>
  </section>
</template>
