<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";

// Компонент для отображения статистики в виде карточки.
// Особенности:
// 1. Принимает пропсы title и value, которые отображаются внутри карточки.
// 2. Стилизован в соответствии с дизайном приложения, с акцентом натные цвета и адаптивный текст.

const props = defineProps<{
  title?: string;
  value?: string | number;
}>();
</script>

<template>
  <Card class="statistics-card card-fifth border-none">
    <CardContent class="statistics-card__content">
      <div class="statistics-card__title text-white">{{ props.title }}</div>
      <div class="statistics-card__value text-white">{{ props.value }}</div>
    </CardContent>
  </Card>
</template>

<style lang="css" scoped>
.statistics-card {
  flex: 1 1 180px;
  min-width: 0;
}

.statistics-card__content {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.75rem;
  padding: clamp(0.75rem, 2vw, 1rem);
}

.statistics-card__title {
  min-width: 0;
  flex: 1 1 auto;
  font-size: clamp(0.875rem, 1.4vw, 1rem);
  line-height: 1.3;
  font-weight: 600;
  overflow-wrap: anywhere;
}

.statistics-card__value {
  flex: 0 0 auto;
  white-space: nowrap;
  font-size: clamp(1rem, 2vw, 1.25rem);
  line-height: 1.3;
  font-weight: 600;
}

@media (width < 680px) {
  .statistics-card {
    flex-basis: 100%;
  }
}
</style>
