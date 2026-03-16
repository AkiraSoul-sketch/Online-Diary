<script lang="ts">
import { TableCell, TableHead } from "@/components/ui/table";
import { onMounted, ref } from "@vue/runtime-dom";
import { toRefs } from "@vueuse/core";

function resolveWidthClass(width: number | undefined) {
  if (!width) return "";
  return `w-${width}`;
}

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
  setup(props, { emit }) {
    const width = ref(0);

    onMounted(() => {
      if (props.refName) {
        const element =
          (document.querySelector(`#${props.refName}`) as HTMLElement) ?? null;
        if (element) {
          width.value = element.clientWidth;
          const observer: ResizeObserver = new ResizeObserver((entries) => {
            for (let entry of entries) {
              console.log("Observed width change:", entry.contentRect.width);
              width.value = entry.contentRect.width;
              emit("update:width", width.value);
            }
          });
          observer.observe(element);
        }
      }
    });

    return { ...toRefs(props), width };
  },
  methods: {
    resolveWidthClass,
  },
  emits: ["update:width"],
};
</script>

<template>
  <TableCell :class="cellClass">
    <div :ref="refName" :class="resolveWidthClass(cellWidth)">
      <TableHead :class="textPosition">
        {{ text }}
      </TableHead>
    </div>
  </TableCell>
</template>
