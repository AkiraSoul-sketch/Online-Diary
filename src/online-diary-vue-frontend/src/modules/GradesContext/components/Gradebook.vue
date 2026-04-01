<script setup lang="ts">
import { useElementSizeObservabilityV2 } from "@/modules/Common/Composables/useElementSizeObservabilityV2";
import type { StudentInfo, ThemeInfo } from "./gradebook.models";
import GradebookPeriodBlock from "./Gradebook/GradebookPeriodBlock.vue";
import HorizontalScrollableContent from "@/modules/Common/Components/HorizontalScrollableContent.vue";
import GradebookStudents from "./Gradebook/GradebookStudents.vue";
import GradebookTheme from "./Gradebook/GradebookTheme.vue";
import GradebookThemeSeparator from "./Gradebook/GradebookThemeSeparator.vue";
import GradebookStudentGrades from "./Gradebook/GradebookStudentGrades.vue";

const props = defineProps<{
  students: StudentInfo[];
  themes: ThemeInfo[];
  containerWidth: number;
}>();

const themeSize = useElementSizeObservabilityV2();
const leftColumn = useElementSizeObservabilityV2();
</script>

<template>
  <section :class="'flex flex-constrained'">
    <div
      :class="'flex gap-1.5 flex-constrained overflow-auto'"
      :key="themeSize.height.value"
    >
      <div
        :class="'justify-center items-center min-w-0 min-h-0'"
        v-if="themeSize.height.value > 0"
        :ref="leftColumn.element"
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
          <GradebookStudents :students="props.students" />
        </div>
      </div>
      <div :class="'flex-constrained'">
        <HorizontalScrollableContent
          :width-limit="containerWidth - leftColumn.width.value - 8"
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
              :class="' left-9 grid grid-rows-[auto_1fr] p-1 my-1 mx-1 justify-center'"
            >
              <GradebookTheme :theme="theme" />
            </div>
            <GradebookThemeSeparator />

            <template v-for="student of students" :key="student.id">
              <GradebookStudentGrades :student="student" />
            </template>
          </div>
        </HorizontalScrollableContent>
      </div>
    </div>
  </section>
</template>
