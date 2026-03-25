<script lang="ts">
import { Label } from "@/components/ui/label";
import { Card, CardTitle, CardContent } from "@/components/ui/card";
import ODGradesPagePageTitle from "./components/OD-GradesPage-PageTitle.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import ODJournalDateBlock from "./components/OD-JournalDateBlock.vue";
import ODStudentsSearch from "./components/OD-StudentsSearch.vue";
import ODGradedThemesList from "./components/GradedThemesList/OD-GradedThemesList.vue";
import { ref, type Ref } from "vue";
import GradesTable from "./components/GradesTable/GradesTable.vue";
import { Item, ItemHeader, ItemMedia, ItemTitle } from "@/components/ui/item";
import ItemContent from "@/components/ui/item/ItemContent.vue";
import Table from "@/components/ui/table/Table.vue";
import {
  BookOpenCheckIcon,
  BookOpenTextIcon,
  PenIcon,
  SearchIcon,
  XCircleIcon,
} from "lucide-vue-next";

const mode = useColorMode();
</script>

<template>
  <section class="flex gap-0 p-2" :ref="'tableContainerElementRef'">
    <!-- // page container -->
    <section :class="'w-full gap-2 my-0'">
      <!-- page title -->
      <CardContent :class="'px-2'">
        <section class="grid grid-cols-2 gap-2">
          <!-- edit journal items here -->
          <div :class="'grid grid-cols-2 gap-2'">
            <ODJournalEditBlock />
            <!-- редакторы -->
            <section :class="'col-span-2'">
              <ODJournalEditorsList />
            </section>
          </div>
          <!-- темы за которые выставлены оценки -->
          <ODGradedThemesList />
        </section>
        <!-- таблица оценок -->
        <GradesTable
          :width-props="{
            blockCellWidth: blockCellWidth,
            studentCellWidth: studentCellWidth,
            gradeScoreCellWidth: gradeScoreCellWidth,
            gradeValueCellWidth: gradeValueCellWidth,
            tableWrapperWidth: tableWrapperWidth,
          }"
          :students="
            generateRandomStudents(10, 20).map((s) => ({
              name: s.name,
              grades: s.grades,
            }))
          "
          :themes="
            generateRandomThemes(20).map((t) => ({
              date: t.date,
              number: t.index,
            }))
          "
        >
        </GradesTable>
      </CardContent>
    </section>
  </section>
</template>
