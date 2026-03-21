<script lang="ts">
import { Button } from "@/components/ui/button";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { PenIcon } from "lucide-vue-next";

export default {
  components: {
    Table,
    TableHeader,
    TableRow,
    TableHead,
    TableBody,
    TableCell,
    Button,
    PenIcon,
  },
};
</script>

<template>
  <Table>
    <TableHeader>
      <TableRow :class="'p-0 gap-0'">
        <TableHead :class="'p-0.5 gap-0.5'"></TableHead>
        <TableHead :class="'p-0.5 gap-0.5'"> Тема </TableHead>
        <TableHead
          :class="'flex p-0.5 gap-0.5 items-center justify-center w-fit'"
        >
        </TableHead>
      </TableRow>
    </TableHeader>
    <TableBody>
      <TableRow>
        <TableCell :class="'p-0.5 gap-0.5'"> 1 </TableCell>
        <TableCell :class="'p-0.5 gap-0.5'"> Тема 1 </TableCell>
        <TableCell
          :class="'p-0.5 gap-0.5 flex items-center justify-center w-fit'"
        >
          <Button
            :class="'flex-none p-0 w-1 rounded-xl h-6 flex items-center justify-center'"
          >
            <PenIcon :size="15"> </PenIcon>
          </Button>
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell :class="'p-0.5 gap-0.5'"> 2 </TableCell>
        <TableCell :class="'p-0.5 gap-0.5'"> Тема 2 </TableCell>
        <TableCell
          :class="'p-0.5 gap-0.5 flex items-center justify-center w-fit'"
        >
          <Button
            :class="'flex-none p-0 w-1 rounded-xl h-6 flex items-center justify-center'"
          >
            <PenIcon :size="15"> </PenIcon>
          </Button>
        </TableCell>
      </TableRow>
    </TableBody>
  </Table>
</template>
