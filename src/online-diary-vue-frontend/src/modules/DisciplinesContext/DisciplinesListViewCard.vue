<script setup lang="ts">
import BlockText from "@/modules/Common/Components/BlockText.vue";
import BooleanBlockText from "@/modules/Common/Components/BooleanBlockText.vue";
import type { Discipline } from "./discipline.models";

defineProps<{
  discipline: Discipline;
}>();
</script>

<template>
  <article class="discipline-card item-bg-primary-accent border rounded-md">
    <div class="discipline-card__header item-bg-primary-accent-2">
      <div class="text-responsive-primary font-semibold text-white wrap-break-word">
        <BlockText :value="discipline.name" :placeholder="'-'" />
      </div>
    </div>

    <div class="discipline-card__content">
      <div class="discipline-field-group">
        <div class="discipline-field">
          <span class="discipline-field__label">Семестр</span>
          <span class="discipline-field__value">
            <BlockText :value="discipline.semester?.number" :placeholder="'-'" />
          </span>
        </div>

        <div class="discipline-field">
          <span class="discipline-field__label">Группа</span>
          <span class="discipline-field__value">
            <BlockText :value="discipline.group?.name" :placeholder="'-'" />
          </span>
        </div>
      </div>

      <div class="discipline-field-group">
        <div class="discipline-field">
          <span class="discipline-field__label">Преподаватель</span>
          <span class="discipline-field__value">
            <BlockText :value="discipline.teacher?.name" :placeholder="'-'" />
          </span>
        </div>

        <div class="discipline-field">
          <span class="discipline-field__label">Создана</span>
          <span class="discipline-field__value">
            <BlockText :value="discipline.lifeTime.createdAt" :placeholder="'-'" />
          </span>
        </div>
      </div>

      <div class="discipline-field-group">
        <div class="discipline-field">
          <span class="discipline-field__label">Изменена</span>
          <span class="discipline-field__value">
            <BlockText :value="discipline.lifeTime.updatedAt" :placeholder="'-'" />
          </span>
        </div>

        <div class="discipline-field">
          <span class="discipline-field__label">Архив</span>
          <span class="discipline-field__value">
            <BooleanBlockText :value="discipline.lifeTime.archived" :onTrue="'да'" :onFalse="'-'" />
          </span>
        </div>
      </div>
    </div>
  </article>
</template>

<style lang="css" scoped>
.discipline-card {
  min-width: 0;
  flex: 1 1 320px;
  display: flex;
  flex-direction: column;
}

.discipline-card__header {
  padding: 1rem 1rem 0.75rem;
  border-bottom: 1px solid rgb(255 255 255 / 0.08);
}

.discipline-card__content {
  display: grid;
  gap: 0.75rem;
  padding: 1rem;
}

.discipline-field-group {
  display: flex;
  flex-direction: row;
  gap: 0.75rem;
}

.discipline-field {
  min-width: 0;
  flex: 1 1 0;
  display: grid;
  gap: 0.25rem;
}

.discipline-field__label {
  color: rgb(255 255 255 / 0.6);
  font-size: 0.875rem;
  line-height: 1.25rem;
}

.discipline-field__value {
  color: white;
  font-size: 0.95rem;
  line-height: 1.35rem;
  word-break: break-word;
}

@media (width < 768px) {
  .discipline-card {
    flex-basis: 100%;
  }
}
</style>
