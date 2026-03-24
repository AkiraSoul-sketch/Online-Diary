<script lang="ts">
import GradesTableControls from "./GradesTableControls.vue";
import GradesTableGradeThemeCell from "./GradesTableGradeThemeCell.vue";
import { ScrollArea, ScrollBar } from "@/components/ui/scroll-area";
import { Table, TableBody, TableHeader } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { LockIcon } from "lucide-vue-next";
import GradesTableCell from "./GradesTableCell.vue";
import GradesTableGradeValueCell from "./GradesTableGradeValueCell.vue";
import GradesTableGradeScoreCell from "./GradesTableGradeScoreCell.vue";
import GradesTableGradeLockCell from "./GradesTableGradeLockCell.vue";

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
    GradesTableControls,
    Table,
    TableHeader,
    GradesTableCell,
    TableBody,
    ScrollArea,
    Button,
    LockIcon,
    GradesTableGradeThemeCell,
    GradesTableGradeValueCell,
    GradesTableGradeScoreCell,
    GradesTableGradeLockCell,
    ScrollBar,
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
      tableHeaderStyles:
        "text-start p-0 inline-block border-[0.5px] border-[color:var(--text-muted)]",
      gradeTableHeaderStyles: "text-center p-0 border inline-block",
      gradeThemeHeaderLabelStyles: "text-center p-0 border inline-block",
      cellsHeight: 0,
      fixedPartWidth: 0,
      scrollLimitWidth: 0,
      blockCellWidth: 0,
      studentCellWidth: 0,
      gradeScoreCellWidth: 0,
      gradeValueCellWidth: 0,
      resizeObserver: null as ResizeObserver | null,
    };
  },
  mounted() {
    const widthProps: WidthProps = this.widthProps;
    this.blockCellWidth = widthProps.blockCellWidth;
    this.studentCellWidth = widthProps.studentCellWidth;
    this.gradeScoreCellWidth = widthProps.gradeScoreCellWidth;
    this.gradeValueCellWidth = widthProps.gradeValueCellWidth;
    this.useFixedPartWidth();
    this.$nextTick(() => {
      const tableWrapper = document.getElementById("table-wrapper");
      if (tableWrapper) {
        const resizeObserver = new ResizeObserver(() => {
          this.useScrollLimitWidth();
        });
        resizeObserver.observe(tableWrapper);
        this.resizeObserver = resizeObserver;
        window.addEventListener("resize", this.useScrollLimitWidth);
      }
    });
  },
  unmounted() {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
    window.removeEventListener("resize", this.useScrollLimitWidth);
  },
  methods: {
    adjustBlockCellWidth(width: number): void {
      this.blockCellWidth = Math.floor(width);
    },
    adjustStudentCellWidth(width: number): void {
      this.studentCellWidth = Math.floor(width);
    },
    adjustGradeScoreCellWidth(width: number): void {
      this.gradeScoreCellWidth = Math.floor(width);
    },
    adjustGradeValueCellWidth(width: number): void {
      this.gradeValueCellWidth = Math.floor(width);
    },
    adjustCellsHeight(height: number): void {
      this.cellsHeight = Math.floor(height);
    },
    adjustScrollLimitWidth(): void {
      const tableWrapperWidth = this.widthProps.tableWrapperWidth;
      this.scrollLimitWidth = tableWrapperWidth - this.fixedPartWidth;
    },
    useFixedPartWidth(): void {
      this.fixedPartWidth =
        this.blockCellWidth + this.studentCellWidth + this.gradeScoreCellWidth;
    },
    useScrollLimitWidth(): void {
      this.adjustScrollLimitWidth();
    },
  },
};
</script>

<template>
  <section
    :class="' rounded-md my-2 shadow-sm border-input'"
    :id="'table-wrapper'"
  >
    <GradesTableControls />
    <Table :class="'flex flex-row bg-(--text-muted) p-1 rounded-b-[0.45rem]'">
      <div :class="'flex flex-col'" :ref="'fixed-part-width'">
        <TableHeader
          :class="'flex flex-row bg-(--text-muted) text-(--highlight)  '"
        >
          <!-- заголовок блокировки -->
          <GradesTableCell
            :cell-class="tableHeaderStyles"
            :ref-name="'block-table-header'"
            :text="'Блокировка'"
            :text-position="'text-start'"
            @cell-width-changed="adjustBlockCellWidth($event)"
          />

          <!-- заголовок студента (имя) -->
          <GradesTableCell
            :cell-class="tableHeaderStyles"
            :ref-name="'student-table-header'"
            :text="'Студент'"
            :text-position="'text-start'"
            :cell-width="200"
            @cell-width-changed="adjustStudentCellWidth($event)"
            @cell-height-changed="adjustCellsHeight($event)"
          />

          <!-- заголовок успеваемости -->
          <GradesTableCell
            :cell-class="tableHeaderStyles"
            :ref-name="'grade-score-value-table-header'"
            :text="'Успеваемость'"
            :text-position="'text-center'"
            @cell-width-changed="adjustGradeScoreCellWidth($event)"
          />
        </TableHeader>
        <!-- часть таблицы, которая не скроллится по горизонтали -->
        <TableBody>
          <div
            v-for="(student, index) in students"
            :key="index"
            :class="'flex flex-row w-full'"
          >
            <!-- блокировка ячейка -->
            <GradesTableGradeLockCell
              :cell-class="tableHeaderStyles + ' bg-(--bg-light) '"
              :cell-width="blockCellWidth"
            />

            <!-- студент ячейка -->
            <GradesTableCell
              :cell-class="tableHeaderStyles + ' bg-(--bg-light) '"
              :text="student.name"
              :text-position="'text-start'"
              :cell-width="studentCellWidth"
            />

            <!-- успеваемость (средняя) ячейка -->
            <GradesTableGradeScoreCell
              :student="student"
              :cell-class="tableHeaderStyles + ' bg-(--bg-light) '"
              :cell-width="gradeScoreCellWidth"
              :text-position="'text-center'"
              :text-wrapper-classes="[
                'text-center',
                'flex',
                'justify-center',
                'items-center',
              ]"
            />
          </div>
        </TableBody>
      </div>

      <!-- часть таблицы, которая скроллится по горизонтали -->
      <ScrollArea
        :style="{
          width: `${scrollLimitWidth}px`,
        }"
        :class="'whitespace-nowrap overflow-hidden'"
      >
        <!-- заголовки тем и дат -->
        <section class="'flex flex-row bg-(--text-muted) '">
          <GradesTableGradeThemeCell
            v-for="(theme, index) in themes"
            :key="index"
            :cell-class="tableHeaderStyles + ' text-(--highlight)'"
            :cell-ref="'grade-value-header'"
            :cell-height="cellsHeight"
            :theme="{ date: theme.date, number: theme.number }"
            @gradeThemeCellWidthChanged="adjustGradeValueCellWidth($event)"
          />
        </section>
        <!-- оценки -->
        <section>
          <div
            v-for="(student, index) of students"
            :key="index"
            class="'flex flex-row'"
          >
            <GradesTableGradeValueCell
              v-for="(grade, gradeIndex) of student.grades"
              :key="gradeIndex"
              :cell-class="tableHeaderStyles"
              :cell-width="gradeValueCellWidth"
              :cell-height="cellsHeight"
              :grade="grade"
              :text-position="'text-center'"
            />
          </div>
        </section>
        <ScrollBar :orientation="'horizontal'" />
      </ScrollArea>
    </Table>
  </section>
</template>
