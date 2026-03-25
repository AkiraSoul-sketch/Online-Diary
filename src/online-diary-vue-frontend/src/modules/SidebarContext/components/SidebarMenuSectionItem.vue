<script lang="ts">
import Label from "@/components/ui/label/Label.vue";
import { TableRow } from "@/components/ui/table";

export default {
  components: {
    Label,
    TableRow,
  },
  props: {
    name: {
      type: String,
      required: true,
    },
  },
};
</script>

<template>
  <TableRow
    :class="'border-0 rounded-md p-1 cursor-pointer mx-2.5 flex justify-start gap-2'"
  >
    <div :class="'cursor-pointer'">
      <slot></slot>
    </div>
    <Label :class="'cursor-pointer  text-[0.7em]'">{{ name }}</Label>
  </TableRow>
</template>
