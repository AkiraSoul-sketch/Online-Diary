<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";

const props = defineProps<{
  title?: string;
  value?: string | number;
}>();
</script>

<template>
  <Card
    class="bg-prussian-blue borderless light-font font-bold flex-1 min-w-[200px] shadow-(--shadow-basic)"
  >
    <CardContent class="p-4">
      <div class="text-responsive-secondary">{{ props.title }}</div>
      <div class="text-responsive-primary font-semibold">{{ props.value }}</div>
    </CardContent>
  </Card>
</template>
