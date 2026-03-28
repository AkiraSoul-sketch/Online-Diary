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
import type { Discipline } from "../Models/Discipline";
import ODTeacherDisciplineCardMD from "./adaptive/OD-TeacherDisciplineCard-MD.vue";

const mediaTracker = Components.CreateMediaQueryTracker();
const scrollAreaLimit = ref(0);
const headerRef = ref<HTMLElement | null>(null);
const headerSize = useElementSizeObservability(headerRef);
const props = defineProps<{
  containerHeight?: number;
}>();

watch(
  () => props.containerHeight,
  ($containerHeight) => {
    const headerHeight = headerSize.value.height;
    const containerHeight = $containerHeight ?? 0;
    const newLimit = containerHeight - headerHeight;
    scrollAreaLimit.value = newLimit;
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
  <Card
    v-if="mediaTracker.isMd"
    :class="'shadow-(--shadow-basic) h-full flex-inner'"
  >
    <CardContent :class="'flex-col flex flex-inner'">
      <div ref="headerRef">
        <ODTeacherPageJournalTableHeader :header-value="'Мои группы:'" />
        <DefaultSearch :class="'shadow-(--shadow-basic) rounded-md w-full'" />
      </div>
      <ScrollArea
        :class="'my-3 overflow-auto p-2.5'"
        :style="{
          height: `${scrollAreaLimit}px`,
        }"
      >
        <section :class="'flex flex-wrap gap-2'">
          <ODTeacherDisciplineCardMD
            v-for="(discipline, index) of generate(50)"
            :key="index"
            :name="discipline.name"
            :groupname="discipline.groupname"
            :gradescount="discipline.gradescount"
          />
        </section>
        <ScrollBar />
      </ScrollArea>
    </CardContent>
  </Card>
  <!-- for LG and XL screens. -->
  <Card v-else :class="'shadow-(--shadow-basic) h-full flex-inner'">
    <CardContent :class="'flex-col flex flex-inner'">
      <div ref="headerRef">
        <ODTeacherPageJournalTableHeader :header-value="'Мои группы:'" />
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
