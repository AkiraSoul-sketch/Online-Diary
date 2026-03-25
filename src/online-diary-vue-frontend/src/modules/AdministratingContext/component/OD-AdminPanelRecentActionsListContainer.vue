<script setup lang="ts">
import Label from "@/components/ui/label/Label.vue";
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
</script>
<template>
  <!-- recent activity -->
  <Card :class="'w-full'">
    <CardTitle :class="'mx-2'" :text="'Недавние действия'"> </CardTitle>
    <slot></slot>
  </Card>
</template>
