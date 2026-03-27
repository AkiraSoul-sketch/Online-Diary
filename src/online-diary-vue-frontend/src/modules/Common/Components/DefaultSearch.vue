<script setup lang="ts">
import Field from "@/components/ui/field/Field.vue";
import InputGroup from "@/components/ui/input-group/InputGroup.vue";
import InputGroupAddon from "@/components/ui/input-group/InputGroupAddon.vue";
import InputGroupInput from "@/components/ui/input-group/InputGroupInput.vue";
import { SearchIcon } from "lucide-vue-next";
</script>

<template>
  <Field>
    <InputGroup>
      <InputGroupInput :class="'text-responsive'" placeholder="поиск..." />
      <InputGroupAddon :align="'inline-end'">
        <SearchIcon :class="'cursor-auto'" />
      </InputGroupAddon>
    </InputGroup>
  </Field>
</template>
