<script setup lang="ts">
import { CalendarIcon } from "lucide-vue-next";
</script>

<template>
  <section :class="'flex flex-col gap-[0.6px] my-0.5 p-1'">
    <div :class="'flex flex-row gap-1'">
      <CalendarIcon />
      <label :class="'text-lg'">Период:</label>
    </div>
    <label :class="'mx-2'">01.03.2026</label>
    <label :class="'mx-2'">31.03.2026</label>
    <label :class="'text-lg'">Текущий день:</label>
    <label :class="'mx-2'">16.03.2026</label>
  </section>
</template>
