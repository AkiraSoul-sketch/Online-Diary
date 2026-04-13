<script setup lang="ts">
import Separator from "@/components/ui/separator/Separator.vue";
import { useGradebookLogic } from "../gradebook.logic";
import type { ThemeInfo } from "../gradebook.models";

const props = defineProps<{
  theme: ThemeInfo;
  active?: boolean;
}>();

const emit = defineEmits<{
  (e: "select", themeIndex: number): void;
}>();

const { formatThemeDate } = useGradebookLogic();

function pickTheme(): void {
  emit("select", props.theme.index);
}
</script>

<template>
  <button
    class="theme-block"
    :class="{ 'theme-block--active': props.active }"
    :data-theme-index="props.theme.index"
    type="button"
    @click="pickTheme"
  >
    <strong class="theme-block__index">Тема {{ props.theme.index }}</strong>
    <Separator class="theme-block__separator" />
    <span class="theme-block__date">{{ formatThemeDate(props.theme.date) }}</span>
  </button>
</template>

<style scoped lang="css">
.theme-block {
  position: sticky;
  top: 0;
  z-index: 20;
  display: grid;
  align-content: center;
  gap: 0.24rem;
  min-height: 100%;
  border: 1px solid hsl(0 0% 100% / 0.06);
  border-radius: 0.9rem;
  background:
    linear-gradient(180deg, hsl(220 12% 23% / 0.98), hsl(220 12% 18% / 0.98)),
    var(--bg-primary);
  box-shadow: var(--shadow-primary);
  padding: 0.45rem 0.32rem;
  text-align: center;
  transition:
    border-color 0.18s ease,
    background 0.18s ease;
}

.theme-block--active {
  border-color: hsl(102 48% 52% / 0.42);
  background:
    linear-gradient(180deg, hsl(102 34% 24% / 0.98), hsl(220 12% 18% / 0.98)),
    var(--bg-primary);
}

.theme-block__index {
  font-size: 0.74rem;
  line-height: 1.1;
  font-weight: 700;
}

.theme-block__separator {
  background: hsl(0 0% 100% / 0.12);
}

.theme-block__date {
  font-size: 0.72rem;
  line-height: 1.1;
  color: hsl(0 0% 100% / 0.72);
}

.theme-block:focus-visible {
  outline: 2px solid hsl(102 62% 50% / 0.7);
  outline-offset: 2px;
}
</style>
