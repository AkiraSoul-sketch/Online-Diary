<script lang="ts">
import { CardTitle } from "@/components/ui/card";
import { Label } from "@/components/ui/label";

export default {
  components: {
    CardTitle,
    Label,
  },
};
</script>

<template>
  <CardTitle :class="'mx-2'">
    <Label :class="'text-2xl'">Группа: ПСК-3-25</Label>
    <Label :class="'text-2xl'">Дисциплина 1</Label>
  </CardTitle>
</template>
