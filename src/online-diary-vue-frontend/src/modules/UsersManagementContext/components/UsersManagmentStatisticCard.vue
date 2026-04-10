<script setup lang="ts">import Card from '@/components/ui/card/Card.vue';
</script>
<template>
  <div class="flex flex-row gap-2 flex-wrap w-full">
    <Card :class="'item-bg-sixth p-3 flex-1'">
      <div class="text-responsive-primary font-semibold">
        Всего пользователей
      </div>
      <div class="text-responsive-secondary p-1">1 234</div>
    </Card>
    <Card :class="'item-bg-sixth p-3 flex-1'">
      <div class="text-responsive-primary font-semibold">
        Пользователей онлайн
      </div>
      <div class="text-responsive-secondary">123</div>
    </Card>
    <Card :class="'item-bg-sixth p-3 flex-1'">
      <div class="text-responsive-primary font-semibold">Преподавателей</div>
      <div class="text-responsive-secondary">45</div>
    </Card>
    <Card :class="'item-bg-sixth p-3 flex-1'">
      <div class="text-responsive-primary font-semibold">Студентов</div>
      <div class="text-responsive-secondary p-1">1 089</div>
    </Card>
  </div>
</template>
