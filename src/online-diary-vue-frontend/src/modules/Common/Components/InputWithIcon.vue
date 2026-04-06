<script setup lang="ts">
import { Field, FieldLabel } from "@/components/ui/field";
import {
  InputGroup,
  InputGroupInput,
  InputGroupAddon,
} from "@/components/ui/input-group";
import type { Component } from "vue";

const props = defineProps<{
  placeHolder?: string;
  label?: string | number;
  icon: Component;
}>();

function labelProvided(): boolean {
  const label: string | number | undefined = props.label;
  return label !== undefined && label !== null && label !== "";
}
</script>

<template>
  <Field :class="'w-full flex '">
    <FieldLabel v-if="labelProvided()">{{ props.label }}</FieldLabel>
    <InputGroup :class="'border item-bg-primary-accent-2'">
      <InputGroupInput
        :class="'text-responsive-secondary'"
        :placeholder="props.placeHolder"
      />
      <InputGroupAddon :align="'inline-end'">
        <component :is="props.icon" />
      </InputGroupAddon>
    </InputGroup>
  </Field>
</template>
