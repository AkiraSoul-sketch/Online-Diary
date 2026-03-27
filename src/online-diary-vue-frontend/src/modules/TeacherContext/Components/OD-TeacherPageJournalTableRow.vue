<script lang="ts">
import Button from "@/components/ui/button/Button.vue";
import { TableBody } from "@/components/ui/table";
import TableCell from "@/components/ui/table/TableCell.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import { BookOpenIcon } from "lucide-vue-next";

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
    TableBody,
    TableCell,
    TableRow,
    Button,
    BookOpenIcon,
  },
  props: {
    disiscipline: {
      type: Object as () => Desceplina,
      required: true,
    },
  },
  data() {
    return {
      GenerateDisciplina: generate(1),
    };
  },
};
</script>

<template>
  <TableRow :class="'w-full'">
    <TableCell :class="'text-center'">
      {{ disiscipline.name }}
    </TableCell>
    <TableCell :class="'text-center'">
      {{ disiscipline.groupname }}
    </TableCell>
    <TableCell :class="'text-center'">
      {{ disiscipline.gradescount }}
    </TableCell>
    <TableCell :class="'text-center'">
      <Button><BookOpenIcon></BookOpenIcon>Открыть</Button>
    </TableCell>
  </TableRow>
</template>
