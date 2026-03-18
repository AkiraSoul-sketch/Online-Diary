<script lang="ts">
import { Button } from "@/components/ui/button";
import {
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { LockIcon } from "lucide-vue-next";

interface Student {
  name: string;
  grades: number[];
}

export default {
  components: {
    TableCell,
    TableHeader,
    TableRow,
    TableHead,
    Button,
    LockIcon,
  },
  props: {
    cellClass: {
      type: String,
      required: true,
    },
    blockCellWidth: {
      type: Number,
      required: true,
    },
    studentCellWidth: {
      type: Number,
      required: true,
    },
    gradeScoreCellWidth: {
      type: Number,
      required: true,
    },
    gradeValueCellWidth: {
      type: Number,
      required: true,
    },
    students: {
      type: Array as () => Student[],
      required: true,
    },
  },
  methods: {
    calculateStudentAverageGrade(student: Student): string {
      if (student.grades.length === 0) return "-";
      const sum = student.grades.reduce((acc, grade) => acc + grade);
      const total = student.grades.length;
      const average = sum / total;
      return average.toFixed(2);
    },
  },
};
</script>

<template>
  <TableHeader v-for="student in students" :class="'flex flex-row'">
    <TableCell :class="cellClass">
      <!-- Ячейка с кнопкой заблокировать -->
      <TableRow>
        <div>
          <TableHead
            :class="'justify-center text-center items-center'"
            :style="{
              width: `${blockCellWidth}px`,
            }"
          >
            <Button :size="'sm'">
              <LockIcon />
            </Button>
          </TableHead>
        </div>
      </TableRow>
    </TableCell>

    <!-- Ячейка с именем студента -->
    <TableCell :class="cellClass">
      <TableRow>
        <div>
          <TableHead
            :class="'text-start '"
            :style="{
              width: `${studentCellWidth}px`,
            }"
            >{{ student.name }}</TableHead
          >
        </div>
      </TableRow>
    </TableCell>

    <!-- Ячейка с успеваемостью студента -->
    <TableCell :class="cellClass">
      <TableRow>
        <div>
          <TableHead
            :class="'p-0 justify-center text-center items-center'"
            :style="{
              width: `${gradeScoreCellWidth}px`,
            }"
            >{{ calculateStudentAverageGrade(student) }}</TableHead
          >
        </div>
      </TableRow>
    </TableCell>

    <!-- Ячейка с оценками студента -->
    <TableCell v-for="grade in student.grades" :class="cellClass">
      <TableRow>
        <div
          :style="{
            width: `${gradeValueCellWidth}px`,
          }"
        >
          <TableHead
            :class="'p-0 justify-center text-center items-center'"
            :style="{
              width: `${gradeValueCellWidth}px`,
            }"
          >
            <label>{{ grade }}</label>
          </TableHead>
        </div>
      </TableRow>
    </TableCell>
  </TableHeader>
</template>
