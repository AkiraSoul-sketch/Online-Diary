<script lang="ts">
import { CardTitle } from "@/components/ui/card";
import { Label } from "@/components/ui/label";
import Separator from "@/components/ui/separator/Separator.vue";

export default {
  components: {
    CardTitle,
    Label,
    Separator,
  },
};
</script>

<template>
  <CardTitle :class="'mx-2'">
    <Label :class="'text-responsive'">Группа: ПСК-3-25 Дисциплина: 1</Label>
    <Separator />
  </CardTitle>
</template>
