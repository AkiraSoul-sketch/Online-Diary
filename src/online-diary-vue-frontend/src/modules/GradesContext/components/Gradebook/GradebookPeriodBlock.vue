<script setup lang="ts">
const props = withDefaults(
  defineProps<{
    compact?: boolean;
  }>(),
  {
    compact: false,
  },
);
</script>

<template>
  <section class="period-block" :class="{ 'period-block--compact': props.compact }">
    <div class="period-block__values">
      <div class="period-block__item">
        <small>Начало</small>
        <strong>01.03.2026</strong>
      </div>
      <div class="period-block__item">
        <small>Окончание</small>
        <strong>31.03.2026</strong>
      </div>
      <div class="period-block__item period-block__item--accent item-bg-primary-accent-2">
        <small>Текущий день</small>
        <strong>16.03.2026</strong>
      </div>
    </div>
  </section>
</template>

<style scoped lang="css">
.period-block {
  display: grid;
  gap: 0.35rem;
}

.period-block__values {
  display: grid;
  grid-template-columns: minmax(0, 1fr);
  gap: 0.35rem;
}

.period-block__item {
  display: grid;
  gap: 0.1rem;
  border: 1px solid hsl(0 0% 100% / 0.06);
  border-radius: 0.8rem;
  background: hsl(220 10% 22% / 0.5);
  padding: 0.35rem 0.45rem;
}

.period-block__item small {
  color: hsl(0 0% 100% / 0.58);
  font-size: 0.62rem;
  line-height: 1.1;
}

.period-block__item strong {
  font-size: 0.74rem;
  font-weight: 600;
  line-height: 1.15;
}

.period-block__item--accent {
  background-color: var(--bg-primary-accent-2);
  background-image: none;
  color: var(--fg-primary);
}

.period-block--compact {
  gap: 0;
}

.period-block--compact .period-block__values {
  display: flex;
  flex-wrap: wrap;
  gap: 0.45rem;
}

.period-block--compact .period-block__item {
  display: inline-flex;
  align-items: center;
  gap: 0.32rem;
  padding: 0.24rem 0.55rem;
  border-radius: 999px;
  white-space: nowrap;
  background: hsl(220 10% 22% / 0.38);
}

.period-block--compact .period-block__item small,
.period-block--compact .period-block__item strong {
  font-size: 0.74rem;
  line-height: 1.1;
}

.period-block--compact .period-block__item small::after {
  content: ":";
  margin-left: 0.1rem;
}

.period-block--compact .period-block__item strong {
  font-weight: 700;
}

.period-block--compact .period-block__item--accent {
  border-color: hsl(0 0% 100% / 0.08);
  background-image: none;
}

@media (max-width: 820px) {
  .period-block--compact .period-block__item small,
  .period-block--compact .period-block__item strong {
    font-size: 0.7rem;
  }
}

@media (max-width: 640px) {
  .period-block--compact .period-block__values {
    gap: 0.35rem;
  }
}
</style>
