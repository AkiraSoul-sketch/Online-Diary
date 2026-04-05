<script setup lang="ts">
import { Field } from "@/components/ui/field";
import {
  InputGroup,
  InputGroupInput,
  InputGroupAddon,
} from "@/components/ui/input-group";
import type { Component } from "vue";

defineProps<{
  placeHolder?: string;
  icon: Component;
}>();
</script>

<template>
  <Field :class="'w-full flex '">
    <InputGroup :class="'border item-bg-primary'">
      <InputGroupInput :class="'text-responsive-secondary'" :placeholder="placeHolder" />
      <InputGroupAddon :align="'inline-end'">
        <component :is="icon" />
      </InputGroupAddon>
    </InputGroup>
  </Field>
</template>
