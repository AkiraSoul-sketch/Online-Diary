<script lang="ts">
import { Item, ItemContent, ItemMedia, ItemTitle } from "@/components/ui/item";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { UserRoundPen } from "lucide-vue-next";
import ODJournalEditorsListItem from "./OD-JournalEditorsListItem.vue";

export default {
  props: {
    spacing: {
      type: String,
      required: false,
      default: "p-px",
    },
  },
  components: {
    Item,
    ItemTitle,
    ItemMedia,
    UserRoundPen,
    Table,
    TableHeader,
    TableRow,
    TableHead,
    ItemContent,
    TableBody,
    TableCell,
    ODJournalEditorsListItem,
  },
};
</script>

<template>
  <Item :class="'text-responsive h-full rounded-md'">
    <ItemContent>
      <ItemTitle :text="'Редакторы март 2026'">
        <ItemMedia><UserRoundPen /></ItemMedia>
      </ItemTitle>
      <Table>
        <TableHeader>
          <TableRow
            :supress-hover-effect="true"
            :class="'text-(--text-default)'"
          >
            <TableHead :class="spacing">Редактор</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          <ODJournalEditorsListItem :editor-name="'Иванов Иван'" />
          <ODJournalEditorsListItem :editor-name="'Петров Петр'" />
          <ODJournalEditorsListItem :editor-name="'Сидоров Сидор'" />
        </TableBody>
      </Table>
    </ItemContent>
  </Item>
</template>
