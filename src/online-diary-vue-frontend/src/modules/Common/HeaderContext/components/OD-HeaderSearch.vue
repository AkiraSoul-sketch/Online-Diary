<script setup lang="ts">
import { Field } from "@/components/ui/field";
import {
  InputGroupAddon,
  InputGroup,
  InputGroupInput,
} from "@/components/ui/input-group";
import { SearchIcon } from "lucide-vue-next";
</script>

<template>
  <Field>
    <InputGroup :class="'h-7 bg-amber-50'">
      <InputGroupInput placeholder="поиск..." />
      <InputGroupAddon :align="'inline-end'">
        <SearchIcon :class="'cursor-auto'" />
      </InputGroupAddon>
    </InputGroup>
  </Field>
</template>
