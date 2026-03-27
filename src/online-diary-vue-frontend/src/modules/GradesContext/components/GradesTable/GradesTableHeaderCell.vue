<script lang="ts">
import { TableCell, TableHead } from "@/components/ui/table";
import { ref } from "@vue/runtime-dom";

export default {
  components: {
    TableCell,
    TableHead,
  },
  props: {
    cellClass: {
      type: String,
      required: false,
    },
    cellWidth: {
      type: Number,
      required: false,
    },
    text: {
      type: String,
      required: true,
    },
    textPosition: {
      type: String,
      required: false,
    },
    refName: {
      type: String,
      required: false,
    },
  },
  setup() {
    const resizeObserver = ref<ResizeObserver | null>(null);
    return { resizeObserver };
  },
  mounted() {
    this.observeCellForWidthResize(this.refName);
  },
  methods: {
    getResizedWidthStyle(width: number | undefined): string | undefined {
      if (!width) return undefined;
      return `${width}px`;
    },
    observeCellForWidthResize(refName: string | undefined): void {
      const refElement = this.getCurrentRefElement(refName);
      if (!refElement) return;
      if (this.resizeObserver) return;
      this.resizeObserver = new ResizeObserver((entries) => {
        for (let entry of entries) {
          if (entry.target === refElement) {
            const newWidth = refElement.clientWidth;
            const newHeight = refElement.clientHeight;
            this.emitCellWidthChange(newWidth);
            this.emitCellHeightChange(newHeight);
          }
        }
      });
      this.resizeObserver.observe(refElement);
    },
    getCurrentRefElement(refName: string | undefined): HTMLElement | undefined {
      if (!refName) return undefined;
      const refElement = this.$refs[refName] as HTMLElement | undefined;
      if (!refElement) return undefined;
      return refElement;
    },
    emitCellWidthChange(newWidth: number): void {
      this.$emit("cellWidthChanged", newWidth);
    },
    emitCellHeightChange(newHeight: number): void {
      this.$emit("cellHeightChanged", newHeight);
    },
  },
  emits: {
    cellWidthChanged: (newWidth: number) => true,
    cellHeightChanged: (newHeight: number) => true,
  },
};
</script>

<template>
  <TableCell :class="cellClass">
    <div
      :ref="refName"
      :style="cellWidth ? { width: `${cellWidth}px` } : undefined"
    >
      <TableHead :class="textPosition">
        {{ text }}
      </TableHead>
    </div>
  </TableCell>
</template>
