<script lang="ts">
import Button from "@/components/ui/button/Button.vue";
import Card from "@/components/ui/card/Card.vue";
import Input from "@/components/ui/input/Input.vue";
import { XCircleIcon } from "lucide-vue-next";

export default {
  components: {
    Button,
    Card,
    Input,
    XCircleIcon,
  },
};
</script>

<template>
  <Card :class="'inner-card-1 flex flex-row gap-2 p-2 w-full col-span-2'">
    <Input :class="'col-span-3'" :placeholder="'Поиск студента'">Text:</Input>
    <Button :size="'icon-sm'">
      <SearchIcon></SearchIcon>
    </Button>
    <Button :size="'icon-sm'">
      <XCircleIcon></XCircleIcon>
    </Button>
  </Card>
</template>
