<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import CardDescription from "@/components/ui/card/CardDescription.vue";
import Label from "@/components/ui/label/Label.vue";
</script>
<template>
  <Card>
    <CardTitle :class="'mx-2'" :text="'Активность пользователей'"> </CardTitle>
    <CardDescription>
      <Label :class="'text-xl p-2'">Placeholder</Label>
    </CardDescription>
  </Card>
</template>
<style></style>
