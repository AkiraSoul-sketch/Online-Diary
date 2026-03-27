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
  <CardTitle :text="'Темы: 2'">
    <BookOpenCheckIcon />
  </CardTitle>
</template>
