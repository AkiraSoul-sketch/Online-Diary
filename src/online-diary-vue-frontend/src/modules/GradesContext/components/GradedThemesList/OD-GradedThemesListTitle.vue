<script lang="ts">
import { CardTitle } from "@/components/ui/card";
import { BookOpenCheckIcon } from "lucide-vue-next";
import { Label } from "reka-ui";

export default {
  components: {
    CardTitle,
    Label,
    BookOpenCheckIcon,
  },
};
</script>

<template>
  <CardTitle>
    <Label :class="'text-2xl'">
      <BookOpenCheckIcon></BookOpenCheckIcon>
      Темы: 2</Label
    >
  </CardTitle>
</template>
