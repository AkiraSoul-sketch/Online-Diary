<script lang="ts">
import Item from "@/components/ui/item/Item.vue";
import ItemTitle from "@/components/ui/item/ItemTitle.vue";

export default {
  components: {
    Item,
    ItemTitle,
  },
  props: {
    HeaderValue: {
      type: String,
      required: true,
    },
  },
};
</script>

<template>
  <Item :class="'p-1 mx-2'">
    <ItemTitle :class="'text-xl'">{{ HeaderValue }}</ItemTitle>
  </Item>
</template>
