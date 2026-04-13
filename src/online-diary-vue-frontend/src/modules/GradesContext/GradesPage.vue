<script setup lang="ts">
import { useMediaQuery } from "@vueuse/core";
import Gradebook from "./components/Gradebook.vue";
import GradebookMobile from "./components/GradebookMobile.vue";
import GradebookTools from "./components/Gradebook/GradebookTools.vue";
import GradebookThemesList from "./components/Gradebook/GradebookThemesList.vue";

const isMobile = useMediaQuery("(max-width: 720px)");
</script>

<template>
  <GradebookMobile v-if="isMobile" />

  <section v-else class="grades-page">
    <div class="grades-page__tools">
      <GradebookTools />
    </div>

    <div class="grades-page__journal">
      <Gradebook />
    </div>

    <div class="grades-page__themes">
      <GradebookThemesList />
    </div>
  </section>
</template>

<style scoped lang="css">
.grades-page {
  height: 100%;
  min-height: 0;
  min-width: 0;
  display: grid;
  grid-template-columns: minmax(0, 1fr) minmax(15rem, 18rem);
  grid-template-areas:
    "tools themes"
    "journal themes";
  grid-template-rows: auto minmax(0, 1fr);
  gap: clamp(0.75rem, 1.8vw, 1.25rem);
  overflow: hidden;
  padding: clamp(0.75rem, 1.4vw, 1rem);
}

.grades-page__tools {
  grid-area: tools;
  display: grid;
  min-width: 0;
  min-height: 0;
  overflow: hidden;
}

.grades-page__journal {
  grid-area: journal;
  min-width: 0;
  min-height: 0;
  overflow: hidden;
}

.grades-page__themes {
  grid-area: themes;
  display: grid;
  height: 100%;
  min-height: 0;
  overflow: hidden;
}

@media (min-width: 1440px) {
  .grades-page {
    grid-template-columns: minmax(16rem, 18rem) minmax(0, 1fr) minmax(15rem, 18rem);
    grid-template-areas: "tools journal themes";
    grid-template-rows: minmax(0, 1fr);
  }
}

@media (max-width: 720px) {
  .grades-page {
    grid-template-columns: minmax(0, 1fr);
    grid-template-areas:
      "tools"
      "journal";
    grid-template-rows: auto minmax(0, 1fr);
  }

  .grades-page__themes {
    display: none;
  }
}
</style>
