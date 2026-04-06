<script setup lang="ts" generic="T">
import { Field, FieldLabel } from "@/components/ui/field";
import {
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectGroup,
  SelectItem,
} from "@/components/ui/select";

const props = defineProps<{
  placeholder: string;
  label: string;
  items: Array<T>;
  itemKey: (item: T) => string | number;
  itemDisplayText: (item: T) => string | number;
}>();
</script>

<template>
  <Field>
    <FieldLabel> {{ props.label }} </FieldLabel>
    <Select :class="'w-full'">
      <SelectTrigger
        :class="'text-responsive-tertiary w-full !bg-[var(--bg-primary-accent-2)]'"
      >
        <SelectValue :placeholder="props.placeholder" />
      </SelectTrigger>
      <SelectContent>
        <SelectGroup>
          <SelectItem
            :value="props.itemKey(item)"
            v-for="item of props.items"
            :key="props.itemKey(item)"
          >
            {{ props.itemDisplayText(item) }}
          </SelectItem>
        </SelectGroup>
      </SelectContent>
    </Select>
  </Field>
</template>
