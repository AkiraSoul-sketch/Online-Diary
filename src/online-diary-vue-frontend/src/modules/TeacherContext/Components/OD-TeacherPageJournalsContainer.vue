<script setup lang="ts">
import Table from "@/components/ui/table/Table.vue";
import TableBody from "@/components/ui/table/TableBody.vue";
import TableHeader from "@/components/ui/table/TableHeader.vue";
import ODTeacherPageJournalTableRow from "./OD-TeacherPageJournalTableRow.vue";
import ODTeacherPageJournalTableHeader from "./OD-TeacherPageJournalTableHeader.vue";
import ScrollArea from "@/components/ui/scroll-area/ScrollArea.vue";
import ODTeacherPageJournalsTableHead from "./OD-TeacherPageJournalsTableHead.vue";
import DefaultSearch from "@/modules/Common/Components/DefaultSearch.vue";
import { Card } from "@/components/ui/card";
import CardContent from "@/components/ui/card/CardContent.vue";
import ScrollBar from "@/components/ui/scroll-area/ScrollBar.vue";
import { ref, watch } from "vue";
import { useElementSizeObservability } from "@/modules/Common/Composables/useElementSizeObservability";
import { Components } from "@/modules/Common/ComponentsLogic/Components";

interface Discipline {
  name: string;
  groupname: string;
  gradescount: number;
}

const mediaTracker = Components.CreateMediaQueryTracker();
const props = defineProps<{
  containerHeight?: number;
}>();
const scrollAreaLimit = ref(0);
const headerRef = ref<HTMLElement | null>(null);
const headerRefSizeObservability = useElementSizeObservability(headerRef);
watch(
  [
    () => headerRefSizeObservability.size.value.height,
    () => props.containerHeight,
  ],
  ([headerHeight, containerHeight]) => {
    const containerHeightValue = containerHeight ?? 0;
    const newLimit = containerHeightValue - headerHeight;
    scrollAreaLimit.value = newLimit;
  },
  {
    immediate: true,
  },
);

function generate(count: number): Discipline[] {
  const DesceplinaName = "Дисциплина 1";

  const Desceplina = [];
  for (let A = 0; A < count; A++) {
    const desceplina: Discipline = {
      name: DesceplinaName,
      groupname: "ПСК-3-25",
      gradescount: 26,
    };
    Desceplina.push(desceplina);
  }
  return Desceplina;
}
</script>

<template>
  <Card :class="'shadow-(--shadow-basic) h-full flex-1 min-h-0'">
    <CardContent>
      <div ref="headerRef">
        <ODTeacherPageJournalTableHeader :-header-value="'Мои группы:'" />
        <DefaultSearch :class="'shadow-(--shadow-basic) rounded-md'" />
      </div>
      <ScrollArea
        :class="'shadow-(--shadow-basic) my-3 overflow-auto'"
        :style="{
          height: `${scrollAreaLimit}px`,
        }"
      >
        <Table :no-wrapper="false">
          <TableHeader
            :class="'sticky z-10 top-0 bg-(--accent-background-color-2)'"
          >
            <ODTeacherPageJournalsTableHead
              :header-value="'Название дисциплины'"
            />
            <ODTeacherPageJournalsTableHead :header-value="'Группа'" />
            <ODTeacherPageJournalsTableHead :header-value="'Действия'" />
          </TableHeader>
          <TableBody :class="'bg-green-40'">
            <ODTeacherPageJournalTableRow
              v-for="(discipline, index) of generate(50)"
              :key="index"
              :gradescount="discipline.gradescount"
              :groupname="discipline.groupname"
              :name="discipline.name"
              :index="index"
            />
          </TableBody>
        </Table>
        <ScrollBar />
      </ScrollArea>
    </CardContent>
  </Card>
</template>
