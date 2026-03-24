<script lang="ts">
import { TableCell, TableHead } from "@/components/ui/table";

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
    cellHeight: {
      type: Number,
      required: false,
    },
    textWrapperClasses: {
      type: Array as () => string[],
      required: false,
    },
    text: {
      type: String,
      required: false,
    },
    textPosition: {
      type: String,
      required: false,
    },
    refName: {
      type: String,
      required: false,
    },
    showText: {
      type: Boolean,
      required: false,
      default: true,
    },
  },
  data() {
    return {
      resizeObserver: null as ResizeObserver | null,
    };
  },
  methods: {
    getTextWrapperClasses(): string {
      if (!this.textWrapperClasses) return "";
      return this.textWrapperClasses.join(" ");
    },
  },
  mounted() {
    if (!this.refName) return;
    const refElement: HTMLElement | undefined = this.$refs[this.refName] as
      | HTMLElement
      | undefined;
    if (!refElement) return;
    this.resizeObserver = new ResizeObserver((entries) => {
      for (let entry of entries) {
        if (entry.target === refElement) {
          const newWidth = refElement.clientWidth;
          const newHeight = refElement.clientHeight;
          this.$emit("cellWidthChanged", newWidth);
          this.$emit("cellHeightChanged", newHeight);
        }
      }
    });
    this.resizeObserver.observe(refElement);
  },
  unmounted() {
    if (this.resizeObserver) {
      this.resizeObserver.disconnect();
    }
  },
  emits: {
    cellWidthChanged(width: number) {
      return true;
    },
    cellHeightChanged(height: number) {
      return true;
    },
  },
};
</script>

<template>
  <TableCell :class="cellClass">
    <div
      :ref="refName"
      :style="{
        width: cellWidth ? `${cellWidth}px` : undefined,
        height: cellHeight ? `${cellHeight}px` : undefined,
      }"
    >
      <slot>
        <!-- Стандартный контент, если слот не используется -->
        <TableHead
          v-if="showText"
          :class="
            textPosition +
            ' ' +
            getTextWrapperClasses() +
            ' ' +
            ' cursor-pointer '
          "
        >
          {{ text }}
        </TableHead>
      </slot>
    </div>
  </TableCell>
</template>
