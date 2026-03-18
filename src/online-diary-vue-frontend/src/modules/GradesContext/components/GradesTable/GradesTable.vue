<script lang="ts">
import { ref } from "vue";
import GradesTableControls from "./GradesTableControls.vue";
import GradesTableGradeThemeCell from "./GradesTableGradeThemeCell.vue";
import GradesTableHeaderCell from "./GradesTableHeaderCell.vue";
import GradesTableStudentRow from "./GradesTableStudentRow.vue";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";
import { Table, TableBody, TableHeader } from "@/components/ui/table";

interface WidthProps {
  tableWrapperWidth: number;
  blockCellWidth: number;
  studentCellWidth: number;
  gradeScoreCellWidth: number;
  gradeValueCellWidth: number;
}

interface ThemeProp {
  date: Date;
  number: number;
}

interface StudentProp {
  name: string;
  grades: number[];
}

export default {
  components: {
    GradesTableHeaderCell,
    GradesTableGradeThemeCell,
    GradesTableStudentRow,
    GradesTableControls,
    ScrollArea,
    ScrollBar,
    Table,
    TableHeader,
    TableBody,
  },
  props: {
    widthProps: {
      type: Object as () => WidthProps,
      required: true,
    },
    themes: {
      type: Array as () => ThemeProp[],
      required: true,
    },
    students: {
      type: Array as () => StudentProp[],
      required: true,
    },
  },
  data() {
    return {
      tableHeaderStyles: "text-start p-0 border inline-block",
      gradeTableHeaderStyles: "text-center p-0 border inline-block",
      gradeThemeHeaderLabelStyles: "text-center p-0 border inline-block",
    };
  },
  setup() {
    const tableWrapperWidth = ref<number>(0);
    const blockCellWidth = ref<number>(0);
    const studentCellWidth = ref<number>(0);
    const gradeScoreCellWidth = ref<number>(0);
    const gradeValueCellWidth = ref<number>(0);
    return {
      tableWrapperWidth,
      blockCellWidth,
      studentCellWidth,
      gradeScoreCellWidth,
      gradeValueCellWidth,
    };
  },
  mounted() {
    const widthProps: WidthProps = this.widthProps;
    this.tableWrapperWidth = widthProps.tableWrapperWidth;
    this.blockCellWidth = widthProps.blockCellWidth;
    this.studentCellWidth = widthProps.studentCellWidth;
    this.gradeScoreCellWidth = widthProps.gradeScoreCellWidth;
    this.gradeValueCellWidth = widthProps.gradeValueCellWidth;
  },
  methods: {
    adjustBlockCellWidth(width: number): void {
      this.blockCellWidth = width;
    },
    adjustStudentCellWidth(width: number): void {
      this.studentCellWidth = width;
    },
    adjustGradeScoreCellWidth(width: number): void {
      this.gradeScoreCellWidth = width;
    },
    adjustGradeValueCellWidth(width: number): void {
      this.gradeValueCellWidth = width;
    },
  },
};
</script>

<template>
  <section :class="'border rounded-md my-2'">
    <!-- контролы (фильтры, сортировка, кнопки над таблицей) -->
    <GradesTableControls />
    <ScrollArea
      :style="{
        width: `${tableWrapperWidth - 19}px`,
      }"
      :class="'whitespace-nowrap overflow-x-auto'"
    >
      <Table :class="'border-t'">
        <!-- заголовки таблицы -->
        <TableHeader :class="'flex flex-row'">
          <!-- блокировка заголовок таблицы -->
          <GradesTableHeaderCell
            :cell-class="tableHeaderStyles"
            :ref-name="'block-table-header'"
            :text="'Блокировка'"
            :text-position="'text-start'"
            @cell-width-changed="adjustBlockCellWidth($event)"
          />

          <!-- студент заголовок таблицы -->
          <GradesTableHeaderCell
            :cell-class="tableHeaderStyles"
            :ref-name="'student-table-header'"
            :text="'Студент'"
            :text-position="'text-start'"
            :cell-width="200"
            @cell-width-changed="adjustStudentCellWidth($event)"
          />

          <!-- успеваемость (средняя) заголовок таблицы -->
          <GradesTableHeaderCell
            :cell-class="tableHeaderStyles"
            :ref-name="'grade-score-value-table-header'"
            :text="'Успеваемость'"
            :text-position="'text-center'"
            @cell-width-changed="adjustGradeScoreCellWidth($event)"
          />

          <!-- темы оценок заголовок таблицы -->
          <GradesTableGradeThemeCell
            v-for="theme in themes"
            :key="theme.number"
            :cell-class="tableHeaderStyles"
            :cell-ref="'grade-value-header'"
            :theme="{ date: theme.date, number: theme.number }"
            @gradeThemeCellWidthChanged="adjustGradeValueCellWidth($event)"
          />
        </TableHeader>

        <TableBody :class="'flex flex-col'">
          <!-- строка студента таблицы -->
          <GradesTableStudentRow
            :students="
              students.map((s) => ({
                name: s.name,
                grades: s.grades,
              }))
            "
            :block-cell-width="blockCellWidth"
            :cell-class="tableHeaderStyles"
            :grade-score-cell-width="gradeScoreCellWidth"
            :grade-value-cell-width="gradeValueCellWidth"
            :student-cell-width="studentCellWidth"
          />
        </TableBody>
      </Table>
      <ScrollBar :orientation="'horizontal'" />
    </ScrollArea>
  </section>
</template>
