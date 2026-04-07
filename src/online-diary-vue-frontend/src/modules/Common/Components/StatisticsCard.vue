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
  <Card :class="'card-fifth flex-1 min-w-[200px] border-none'">
    <CardContent :class="'p-4'">
      <div :class="'text-responsive-primary font-semibold  text-white'">{{ props.title }}</div>
      <div :class="'text-responsive-primary font-semibold text-white'">{{ props.value }}</div>
    </CardContent>
  </Card>
</template>
