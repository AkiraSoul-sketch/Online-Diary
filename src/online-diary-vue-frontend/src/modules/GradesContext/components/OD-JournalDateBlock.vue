<script lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import { Item } from "@/components/ui/item";
import Label from "@/components/ui/label/Label.vue";

export default {
  components: {
    Card,
    CardTitle,
    Label,
    Item,
  },
};
</script>

<template>
  <Item :variant="'muted'" :class="'rounded-md h-full'">
    <Label :class="'text-responsive'">Март 2026</Label>
  </Item>
</template>
