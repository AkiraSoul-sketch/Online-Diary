<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import { BookOpenIcon } from "lucide-vue-next";
import ODTeacherPageJournalsTableCell from "./OD-TeacherPageJournalsTableCell.vue";

interface Desceplina {
  name: string;
  groupname: string;
  gradescount: number;
  index?: number | undefined;
}

function resolveIndexBackgroundColor(index: number | undefined): string {
  if (index === undefined) {
    return "";
  }

  if (index % 2 === 0) {
    return "bg-[var(--accent-background-color-5)]";
  } else {
    return "bg-[var(--accent-background-color-4)]";
  }
}

const props = defineProps<Desceplina>();
</script>

<template>
  <TableRow
    :class="
      'w-full ' +
      resolveIndexBackgroundColor(props.index) +
      ' hover:bg-[var(--accent-background-color-6)]'
    "
  >
    <ODTeacherPageJournalsTableCell :cell-value="props.name" />
    <ODTeacherPageJournalsTableCell :cell-value="props.groupname" />
    <ODTeacherPageJournalsTableCell :cell-value="undefined">
      <Button :class="'text-responsive text-center'"
        ><BookOpenIcon />
        Открыть
      </Button>
    </ODTeacherPageJournalsTableCell>
  </TableRow>
</template>
