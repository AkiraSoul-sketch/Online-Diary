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

// Обертка над стандартным select в shadcn.
// Особенности:
// 1. Обязательные пропсы:
//   - placeholder: строка, отображаемая при отсутствии выбранного элемента.
//   - label: строка, отображаемая в качестве метки для селекта.
//   - items: массив элементов типа T, которые будут отображаться в селекте.
//   - itemKey: функция, которая принимает элемент типа T и возвращает его уникальный ключ (строка или число).
//   - itemDisplayText: функция, которая принимает элемент типа T и возвращает текст для
// 2. Селект содержит акцентный фоновый цвет (по-умолчанию, в shadcn сложно переопределить цвет фона для триггера селекта)
// Рекомендуется использовать этот селект, вместо shadcn селекта напрямую.

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
    <!-- метка (подсказка над селектом) -->
    <FieldLabel> {{ props.label }} </FieldLabel>

    <Select :class="'w-full'">

      <!-- кнопка внутри селекта, которая его раскрывает при нажатии -->
      <SelectTrigger :class="'text-responsive-tertiary w-full !bg-[var(--bg-primary-accent-2)]'">
        <SelectValue :placeholder="props.placeholder" />
      </SelectTrigger>

      <!-- контент, который рендерится в селекте (список элементов) -->
      <SelectContent>
        <SelectGroup>
          <SelectItem :value="props.itemKey(item)" v-for="item of props.items" :key="props.itemKey(item)">
            {{ props.itemDisplayText(item) }}
          </SelectItem>
        </SelectGroup>
      </SelectContent>

    </Select>
  </Field>
</template>
