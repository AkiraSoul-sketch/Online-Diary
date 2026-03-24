<script lang="ts">
import { Button } from "@/components/ui/button";
import { TableCell, TableRow } from "@/components/ui/table";
import { PenIcon } from "lucide-vue-next";

export default {
  props: {
    number: {
      type: Number,
      required: true,
    },
    name: {
      type: String,
      required: true,
    },
    spacing: {
      type: String,
      required: false,
      default: "p-px",
    },
  },
  components: {
    TableRow,
    TableCell,
    Button,
    PenIcon,
  },
};
</script>

<template>
  <TableRow>
    <TableCell :class="spacing"> {{ number }} </TableCell>
    <TableCell :class="spacing">{{ name }}</TableCell>
    <TableCell :class="spacing">
      <Button
        :variant="'default'"
        :class="
          'w-1 rounded-xl h-6 flex items-center justify-center' + ' ' + spacing
        "
      >
        <PenIcon :size="15"> </PenIcon>
      </Button>
    </TableCell>
  </TableRow>
</template>
