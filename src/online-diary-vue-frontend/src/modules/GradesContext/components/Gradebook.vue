<script setup lang="ts">
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import HorizontalScrollableContent from "@/modules/Common/Components/HorizontalScrollableContent.vue";
import ODJournalEditorsList from "./JournalEditorsList/OD-JournalEditorsList.vue";
import type { StudentInfo, ThemeInfo } from "./gradebook.models";
import { useGradebookLogic } from "./gradebook.logic";
import GradebookPeriodBlock from "./GradebookPeriodBlock.vue";

const { resolveGradebookColor } = useGradebookLogic();
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
  <section :class="'flex flex-constrained'">
    <div
      :class="'flex gap-1.5 flex-constrained overflow-auto'"
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
          <GradebookPeriodBlock />
        </div>

        <div :class="'drop-shadow-xl flex flex-col my-3 gap-2 justify-center'">
          <div
            v-for="student of students"
            :class="'bg-zinc-100 w-full text-center p-1'"
          >
            {{ studentNameText(student) }}
          </div>
        </div>
      </div>
      <div :class="'flex-constrained'">
        <HorizontalScrollableContent
          :width-limit="containerWidth - rightColumn.width.value - 8"
        >
          <div
            :style="{
              display: 'grid',
              gridTemplateColumns: `repeat(${themes.length}, 45px)`,
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
              :class="' bg-zinc-100 left-9 grid grid-rows-[auto_1fr] p-1 my-1 mx-1 justify-center shadow-2xl'"
            >
              <div
                :class="'z-10  relative text-center [writing-mode:vertical-rl] rotate-220'"
              >
                <span :class="'relative right-10 z-20'">
                  {{ "Тема " + theme.date.toLocaleDateString() }}
                </span>
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
                :class="'mx-1 my-1 p-1 drop-shadow-xl justify-center items-center text-center bg-zinc-100'"
              >
                <div :style="{ backgroundColor: resolveGradebookColor(grade) }">
                  {{ grade.gradeValue }}
                </div>
              </div>
            </template>
          </div>
        </HorizontalScrollableContent>
      </div>
    </div>
  </section>
</template>

<style lang="css" scoped>
.theme-header {
  text-orientation: upright;
}
</style>
