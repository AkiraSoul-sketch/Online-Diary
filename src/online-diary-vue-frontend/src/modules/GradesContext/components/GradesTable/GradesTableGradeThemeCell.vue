<script lang="ts">
import { Label } from "@/components/ui/label";
import { Separator } from "@/components/ui/separator";
import { TableCell } from "@/components/ui/table";
import { ref, type PropType } from "vue";

interface Theme {
  date: Date;
  number: number;
}

const dateFormat: "2-digit" = "2-digit";
const dateLocale = undefined;
const dateFormatOptions: Intl.DateTimeFormatOptions = {
  year: dateFormat,
  month: dateFormat,
  day: dateFormat,
};

export default {
  components: {
    Label,
    Separator,
    TableCell,
  },
  props: {
    theme: {
      type: Object as PropType<Theme>,
      required: true,
    },
    cellClass: {
      type: String,
      required: true,
    },
    cellRef: {
      type: String,
      required: true,
    },
    cellHeight: {
      type: Number,
      required: true,
    },
  },
  setup() {
    const resizeObserver = ref<ResizeObserver | undefined>(undefined);
    return { resizeObserver };
  },
  mounted() {
    const cellElement = this.$refs[this.cellRef] as HTMLElement | undefined;
    this.useResizeObserver(cellElement);
  },
  methods: {
    formatThemeDate(theme: Theme): string {
      return theme.date.toLocaleDateString(dateLocale, dateFormatOptions);
    },
    emitCellWidth(width: number) {
      this.$emit("gradeThemeCellWidthChanged", width);
    },
    useResizeObserver(element: HTMLElement | undefined): void {
      if (!element) return;
      if (this.resizeObserver) return;
      const width = element.clientWidth;
      this.resizeObserver = new ResizeObserver((entries) => {
        for (let entry of entries) {
          if (entry.target === element) {
            this.emitCellWidth(width);
          }
        }
      });
      this.resizeObserver.observe(element);
    },
  },
  emits: {
    gradeThemeCellWidthChanged(width: number) {
      return true;
    },
  },
};
</script>

<template>
  <TableCell :class="cellClass">
    <div
      :ref="cellRef"
      :class="'flex flex-col justify-center items-center gap-1 w-18'"
      :style="{ height: `${cellHeight}px` }"
    >
      <Label :class="'text-center p-0.5'">
        {{ formatThemeDate(theme) }}
      </Label>
      <Separator />
      <Label :class="'text-center'">{{ theme.number }}</Label>
    </div>
  </TableCell>
</template>
