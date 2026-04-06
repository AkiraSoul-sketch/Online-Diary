<script setup lang="ts">
import { useElementSizeObservabilityV2 } from "../Common/Composables/useElementSizeObservabilityV2";
import Gradebook from "./components/Gradebook.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import GradebookTools from "./components/Gradebook/GradebookTools.vue";
const container = useElementSizeObservabilityV2();
</script>

<template>
  <section class="flex-column-layout flex-constrained full-size gap-2 p-2" :ref="container.element">
    <ODJournalEditBlock />
    <GradebookTools />
    <Gradebook :containerWidth="container.width.value" />
  </section>
</template>
