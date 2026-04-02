<script setup lang="ts">
import { useElementSizeObservabilityV2 } from "../Common/Composables/useElementSizeObservabilityV2";
import Gradebook from "./components/Gradebook.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import GradebookTools from "./components/Gradebook/GradebookTools.vue";
const container = useElementSizeObservabilityV2();
</script>

<template>
  <section
    class="flex flex-col gap-2 flex-1 min-h-0 min-w-0 bg-zinc-200 p-2 h-full w-full"
    :ref="container.element"
  >
    <ODJournalEditBlock />
    <GradebookTools />
    <Gradebook :containerWidth="container.width.value" />
  </section>
</template>
