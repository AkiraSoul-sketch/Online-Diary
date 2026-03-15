<script lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Label from "@/components/ui/label/Label.vue";

export default {
  components: {
    Card,
    CardTitle,
    Label,
  },
};
</script>

<template>
  <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
    <CardTitle>
      <Label :class="'text-xl'">Журнал Март 2026</Label>
      <Label :class="'text-xl'"></Label>
    </CardTitle>
  </Card>
</template>
