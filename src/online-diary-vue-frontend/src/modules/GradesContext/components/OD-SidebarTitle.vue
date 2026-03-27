<script lang="ts">
import { Card, CardFooter, CardTitle } from "@/components/ui/card";
import { Label } from "@/components/ui/label";

export default {
  components: {
    Card,
    CardFooter,
    CardTitle,
    Label,
  },
};
</script>

<template>
  <Card>
    <CardTitle>
      <Label :class="'text-2xl mx-2'">Online</Label>
      <Label :class="'text-2xl mx-10'">Diary</Label>
    </CardTitle>
    <CardFooter>
      <Label :class="'justify-end'">Онлайн журнал</Label>
    </CardFooter>
  </Card>
</template>
