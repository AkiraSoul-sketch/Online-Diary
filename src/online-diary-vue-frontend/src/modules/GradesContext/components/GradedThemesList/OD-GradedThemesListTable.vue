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
import ODGradedThemesListItem from "./OD-GradedThemesListItem.vue";

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
    ODGradedThemesListItem,
  },
};
</script>

<template>
  <Table>
    <TableHeader>
      <TableRow
        :supress-hover-effect="true"
        :class="'p-0 gap-0 text-(--text-default)'"
      >
        <TableHead :class="'px-0'"></TableHead>
        <TableHead :class="'px-0'">Тема</TableHead>
        <TableHead :class="'px-0'"></TableHead>
      </TableRow>
    </TableHeader>
    <TableBody>
      <ODGradedThemesListItem :name="'Тема 1'" :number="1" />
      <ODGradedThemesListItem :name="'Тема 2'" :number="1" />
      <ODGradedThemesListItem :name="'Тема 3'" :number="1" />
      <ODGradedThemesListItem :name="'Тема 4'" :number="1" />
      <ODGradedThemesListItem :name="'Тема 5'" :number="1" />
      <ODGradedThemesListItem :name="'Тема 6'" :number="1" />
    </TableBody>
  </Table>
</template>
