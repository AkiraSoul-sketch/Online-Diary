<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
</script>
<template>
  <Card>
    <CardTitle :class="'p-2'" :text="'Секция администрирования'"> </CardTitle>
    <div>
      <slot />
    </div>
  </Card>
</template>
