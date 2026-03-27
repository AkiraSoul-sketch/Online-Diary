<script lang="ts">
import Button from "@/components/ui/button/Button.vue";
import Item from "@/components/ui/item/Item.vue";
import Table from "@/components/ui/table/Table.vue";
import TableBody from "@/components/ui/table/TableBody.vue";
import TableCell from "@/components/ui/table/TableCell.vue";
import TableHead from "@/components/ui/table/TableHead.vue";
import TableHeader from "@/components/ui/table/TableHeader.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import { BookOpenIcon } from "lucide-vue-next";
import ODTeacherPageJournalTableRow from "./OD-TeacherPageJournalTableRow.vue";
import ODTeacherPageJournalTableHeader from "./OD-TeacherPageJournalTableHeader.vue";
import ScrollArea from "@/components/ui/scroll-area/ScrollArea.vue";

interface Desceplina {
  name: string;
  groupname: string;
  gradescount: number;
}

function generate(count: number): Desceplina[] {
  const DesceplinaName = "Дисциплина 1";

  const Desceplina = [];
  for (let A = 0; A < count; A++) {
    const desceplina: Desceplina = {
      name: DesceplinaName,
      groupname: "ПСК-3-25",
      gradescount: 26,
    };
    Desceplina.push(desceplina);
  }
  return Desceplina;
}

export default {
  components: {
    Item,
    Table,
    TableHeader,
    TableRow,
    TableHead,
    TableBody,
    TableCell,
    Button,
    ScrollArea,
    BookOpenIcon,
    ODTeacherPageJournalTableRow,
    ODTeacherPageJournalTableHeader,
  },
  data() {
    return {
      GenerateDisciplina: generate(10),
    };
  },
};
</script>

<template>
  <Item :class="' p-0 flex flex-col w-full   items-stretch'" :variant="'muted'">
    <ODTeacherPageJournalTableHeader :-header-value="'Мои группы:'" />
    <ScrollArea :class="'h-80 min-h-0'">
      <Table :no-wrapper="false">
        <TableHeader :class="'sticky top-0 bg-accent'">
          <TableHead :class="'text-center'"> Название дисциплины </TableHead>
          <TableHead :class="'text-center'"> Группа </TableHead>
          <TableHead :class="'text-center'"> Оценок проставить </TableHead>
          <TableHead :class="'text-center'"> Действия </TableHead>
        </TableHeader>
        <TableBody :class="'bg-green-40'">
          <ODTeacherPageJournalTableRow
            v-for="discipline of GenerateDisciplina"
            :disiscipline="{
              name: discipline.name,
              gradescount: discipline.gradescount,
              groupname: discipline.groupname,
            }"
          />
        </TableBody>
      </Table>
    </ScrollArea>
  </Item>
</template>
