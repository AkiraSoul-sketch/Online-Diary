<script setup lang="ts">
import Label from "@/components/ui/label/Label.vue";
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
</script>
<template>
  <!-- recent activity -->
  <Card :class="'w-full'">
    <CardTitle :class="'mx-2'">
      <Label :class="'text-2x1'">Недавние действия</Label>
    </CardTitle>
    <div>
      <slot />
    </div>
  </Card>
</template>
