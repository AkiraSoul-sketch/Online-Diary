<script lang="ts">
import { defineComponent } from "vue";
import {
  Empty,
  EmptyContent,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "./components/ui/empty";
import { CodeIcon } from "lucide-vue-next";

export default defineComponent({
  components: {
    Empty,
    EmptyContent,
    EmptyDescription,
    EmptyHeader,
    EmptyMedia,
    EmptyTitle,
    CodeIcon,
  },
});
</script>

<template>
  <Empty :class="'flex items-center justify-center w-full h-full'">
    <EmptyHeader>
      <EmptyMedia>
        <CodeIcon />
      </EmptyMedia>
      <EmptyTitle>В разработке...</EmptyTitle>
      <EmptyDescription>
        Данная страница находится в разработке. Пожалуйста, зайдите позже.
      </EmptyDescription>
    </EmptyHeader>
  </Empty>
</template>
