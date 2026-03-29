<script setup lang="ts">
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import type { StudentInfo, ThemeInfo } from "../GradesPage.vue";
import HorizontalScrollableContent from "@/modules/Common/Components/HorizontalScrollableContent.vue";
import ODJournalEditorsList from "./JournalEditorsList/OD-JournalEditorsList.vue";

const props = defineProps<{
  students: StudentInfo[];
  themes: ThemeInfo[];
  containerWidth: number;
}>();
const themeSize = useElementSizeObservabilityV2();
const rightColumn = useElementSizeObservabilityV2();
function studentNameText(student: StudentInfo): string {
  const parts = student.name.split(" ");
  return `${parts[0]} ${parts[1][0]}. ${parts[2][0]}.`;
}
</script>

<template>
  <div
    :class="'flex gap-2 flex-1 min-w-0 min-h-0 overflow-auto'"
    :key="themeSize.height.value"
  >
    <div
      :class="'justify-center items-center min-w-0 min-h-0'"
      :ref="rightColumn.element"
      v-if="themeSize.height.value > 0"
      :style="{
        height: '826px',
        display: 'grid',
        gridTemplateRows: `auto repeat(${students.length}, 1fr)`,
      }"
    >
      <div
        :style="{
          height: themeSize.height.value + 16 + 'px',
        }"
        :class="'min-w-0'"
      >
        <ODJournalEditorsList />
      </div>

      <div :class="'flex flex-col my-3.5 gap-2 justify-center'">
        <div
          v-for="student of students"
          :class="'bg-zinc-100 w-full text-center'"
        >
          {{ studentNameText(student) }}
        </div>
      </div>
    </div>
    <div :class="'flex-1 min-w-0 min-h-0'">
      <HorizontalScrollableContent
        :class="'min-w-0 min-h-0'"
        :width-limit="containerWidth - rightColumn.width.value - 8"
      >
        <div
          :class="'bg-zinc-100'"
          :style="{
            display: 'grid',
            gridTemplateColumns: `repeat(${themes.length}, 1fr)`,
          }"
        >
          <div
            v-for="(theme, index) in themes"
            :ref="
              (element) => {
                if (index === 0) {
                  const htmlEl: HTMLElement = element as HTMLElement;
                  themeSize.element.value = htmlEl;
                }
              }
            "
            :key="'header-' + theme.index"
            :class="'grid grid-rows-[auto_1fr] gap-5 p-2 justify-center'"
          >
            <div :class="'text-center [writing-mode:vertical-rl]'">
              {{ theme.index }}
            </div>
            <div :class="'text-center [writing-mode:vertical-rl]'">
              {{ "Тема " + theme.date.toLocaleDateString() }}
            </div>
          </div>

          <div
            :class="'bg-zinc-200'"
            :style="{ gridColumn: '1 / -1', height: '8px' }"
          ></div>

          <template v-for="student of students" :key="student.id">
            <div
              v-for="grade of student.grades"
              :key="student.id + '-' + grade.theme"
              :class="'p-1 justify-center items-center text-center'"
            >
              {{ grade.gradeValue }}
            </div>
          </template>
        </div>
      </HorizontalScrollableContent>
    </div>
  </div>
</template>

<style lang="css" scoped>
.theme-header {
  text-orientation: upright;
}
</style>
