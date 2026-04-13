<script setup lang="ts">
import { computed } from "vue";
import { useGradebookLogic } from "../gradebook.logic";
import { useGradebookStore } from "../gradebook.store";
import { PenIcon } from "lucide-vue-next";

const props = withDefaults(
  defineProps<{
    embedded?: boolean;
    drawerMode?: boolean;
  }>(),
  {
    embedded: false,
    drawerMode: false,
  },
);

const emit = defineEmits<{
  (e: "theme-picked", themeIndex: number): void;
}>();

const store = useGradebookStore();
const themes = computed(() => store.themes);
const activeThemeIndex = computed(() => store.activeThemeIndex);
const { formatThemeDate } = useGradebookLogic();

function pickTheme(themeIndex: number): void {
  store.selectTheme(themeIndex);
  emit("theme-picked", themeIndex);
}
</script>

<template>
  <aside class="themes-panel" :class="[
    'card-primary',
    {
      'themes-panel--embedded': props.embedded,
      'themes-panel--drawer': props.drawerMode,
    },
  ]">
    <div class="themes-panel__header">
      <h2 class="themes-panel__title">Темы</h2>
      <p class="themes-panel__caption">Выберите тему, чтобы прокрутить журнал к нужной колонке.</p>
    </div>

    <div class="themes-panel__list">
      <button v-for="theme in themes" :key="theme.index" class="themes-panel__item"
        :class="{ 'themes-panel__item--active': theme.index === activeThemeIndex }" type="button"
        @click="pickTheme(theme.index)">
        <div class="themes-panel__item-copy">
          <strong class="themes-panel__item-title">{{ theme.title }}</strong>
          <span class="themes-panel__item-meta">№{{ theme.index }} · {{ formatThemeDate(theme.date) }}</span>
        </div>
        <PenIcon class="themes-panel__item-icon" :size="14" />
      </button>
    </div>
  </aside>
</template>

<style scoped lang="css">
.themes-panel {
  height: 100%;
  min-height: 0;
  display: grid;
  grid-template-rows: auto minmax(0, 1fr);
  gap: 0.75rem;
  border: 1px solid var(--panel-border-color);
  border-radius: 1.25rem;
  padding: clamp(0.85rem, 1.5vw, 1rem);
  overflow: hidden;
}

.themes-panel--embedded {
  height: 100%;
  border: none;
  border-radius: 0;
  background: transparent;
  padding: 0;
  box-shadow: none;
}

.themes-panel--drawer {
  width: min(100%, 28rem);
  height: 100%;
  min-height: 0;
  margin-inline: auto;
}

.themes-panel--drawer .themes-panel__header {
  justify-items: center;
  text-align: center;
}

.themes-panel--drawer .themes-panel__list {
  padding-right: 0.1rem;
}

.themes-panel__header {
  display: grid;
  gap: 0.35rem;
}

.themes-panel__title {
  margin: 0;
  font-size: 1.05rem;
  line-height: 1.1;
}

.themes-panel__caption {
  margin: 0;
  color: var(--panel-text-color);
  opacity: 0.68;
  font-size: 0.82rem;
  line-height: 1.35;
}

.themes-panel__list {
  min-height: 0;
  overflow-y: auto;
  overscroll-behavior: contain;
  -webkit-overflow-scrolling: touch;
  display: grid;
  align-content: start;
  gap: 0.55rem;
  padding-right: 0.15rem;
}

.themes-panel__item {
  display: grid;
  grid-template-columns: minmax(0, 1fr) auto;
  align-items: center;
  gap: 0.7rem;
  border: 1px solid var(--panel-border-color);
  border-radius: 0.95rem;
  background: var(--panel-bg);
  padding: 0.8rem 0.9rem;
  text-align: left;
  transition:
    background 0.18s ease,
    border-color 0.18s ease;
}

.themes-panel__item--active {
  border-color: var(--panel-active-border);
  background: var(--panel-bg-active-gradient), var(--panel-bg-active);
}

.themes-panel__item-copy {
  display: grid;
  gap: 0.18rem;
  min-width: 0;
}

.themes-panel__item-title {
  font-size: 0.95rem;
  line-height: 1.2;
}

.themes-panel__item-icon {
  display: block;
  flex-shrink: 0;
  color: var(--panel-text-color);
  opacity: 0.72;
}

.themes-panel__item-meta {
  color: var(--panel-text-color);
  opacity: 0.68;
  font-size: 0.78rem;
  line-height: 1.2;
}
</style>
