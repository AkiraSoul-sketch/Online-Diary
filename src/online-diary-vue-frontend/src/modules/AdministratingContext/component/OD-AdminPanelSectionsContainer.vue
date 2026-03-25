<script setup lang="ts">
import Label from "@/components/ui/label/Label.vue";
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
</script>
<template>
  <Card>
    <CardTitle :class="'p-2'">
      <Label :class="'text-2xl'">Секция администрирования</Label>
    </CardTitle>
    <div>
      <slot />
    </div>
  </Card>
</template>
