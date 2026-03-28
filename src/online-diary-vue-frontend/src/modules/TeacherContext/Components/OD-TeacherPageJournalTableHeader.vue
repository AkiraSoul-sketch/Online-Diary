<script lang="ts">
import Item from "@/components/ui/item/Item.vue";
import ItemTitle from "@/components/ui/item/ItemTitle.vue";

export default {
  components: {
    Item,
    ItemTitle,
  },
  props: {
    headerValue: {
      type: String,
      required: true,
    },
  },
};
</script>

<template>
  <ItemTitle :class="'p-4 text-responsive leading-4'">{{
    headerValue
  }}</ItemTitle>
</template>
