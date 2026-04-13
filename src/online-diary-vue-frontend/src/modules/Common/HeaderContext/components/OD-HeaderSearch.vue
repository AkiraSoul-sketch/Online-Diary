<script setup lang="ts">
import { Field } from "@/components/ui/field";
import {
  InputGroup,
  InputGroupAddon,
  InputGroupInput,
} from "@/components/ui/input-group";
import { SearchIcon } from "lucide-vue-next";
</script>

<template>
  <Field class="od-header-search gap-0">
    <InputGroup class="od-header-search__group">
      <InputGroupAddon align="inline-start" class="od-header-search__icon">
        <SearchIcon />
      </InputGroupAddon>

      <InputGroupInput class="od-header-search__input text-responsive-secondary dark:text-white!" placeholder="поиск" />

      <InputGroupAddon align="inline-end" class="od-header-search__hint">
        <span>Быстрый поиск</span>
      </InputGroupAddon>
    </InputGroup>
  </Field>
</template>

<style scoped lang="css">
.od-header-search {
  width: 100%;
  min-width: 0;
}

.od-header-search__group {
  min-height: 3rem;
  border-color: hsl(104 44% 55% / 0.18);
  border-radius: 1rem;
  background: hsl(0 0% 100% / 0.7);
  box-shadow:
    inset 0 1px 0 hsl(0 0% 100% / 0.8),
    0 10px 24px hsl(240 25% 8% / 0.04);
}

.od-header-search__icon {
  color: hsl(104 44% 34%);
}

.od-header-search__input {
  height: 3rem;
  padding-inline: 0.2rem;
  font-size: 0.92rem;
}

.od-header-search__input::placeholder {
  color: hsl(240 10% 36% / 0.68);
}

.od-header-search__hint {
  padding-right: 0.6rem;
}

.od-header-search__hint span {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  min-height: 1.8rem;
  border-radius: 999px;
  background: hsl(104 44% 55% / 0.14);
  padding-inline: 0.7rem;
  font-size: 0.72rem;
  font-weight: 700;
  letter-spacing: 0.03em;
  white-space: nowrap;
  color: hsl(104 44% 30%);
}

.dark .od-header-search__group {
  border-color: hsl(0 0% 100% / 0.08);
  background: hsl(220 8% 21% / 0.88);
  box-shadow:
    inset 0 1px 0 hsl(0 0% 100% / 0.04),
    0 10px 24px hsl(220 30% 4% / 0.16);
}

.dark .od-header-search__icon {
  color: hsl(104 52% 74%);
}

.dark .od-header-search__input::placeholder {
  color: hsl(0 0% 100% / 0.48);
}

.dark .od-header-search__input,
.dark .od-header-search__input,
.dark .od-header-search__input input,
.dark .od-header-search__input textarea {
  color: var(--fg-reversed);
  caret-color: var(--fg-reversed);
}

/* Ensure styles penetrate child components (Input component) */
.dark ::v-deep .od-header-search__input,
.dark ::v-deep .od-header-search__input input,
.dark ::v-deep .od-header-search__input textarea {
  color: var(--fg-reversed) !important;
  caret-color: var(--fg-reversed) !important;
}

.dark ::v-deep .od-header-search__input::placeholder,
.dark ::v-deep .od-header-search__input input::placeholder {
  color: hsl(0 0% 100% / 0.48) !important;
}

.dark .od-header-search__hint span {
  background: hsl(104 44% 45% / 0.18);
  color: hsl(104 52% 78%);
}

@media (max-width: 720px) {
  .od-header-search__group {
    min-height: 2.8rem;
    border-radius: 0.95rem;
  }

  .od-header-search__input {
    height: 2.8rem;
  }

  .od-header-search__hint {
    display: none;
  }
}

@media (max-width: 420px) {
  .od-header-search__input {
    font-size: 0.86rem;
  }
}
</style>
