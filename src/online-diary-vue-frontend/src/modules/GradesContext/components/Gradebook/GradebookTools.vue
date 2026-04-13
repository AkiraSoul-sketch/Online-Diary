<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import Calendar from "@/components/ui/calendar/Calendar.vue";
import Popover from "@/components/ui/popover/Popover.vue";
import PopoverContent from "@/components/ui/popover/PopoverContent.vue";
import PopoverTrigger from "@/components/ui/popover/PopoverTrigger.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import GradebookPeriodBlock from "./GradebookPeriodBlock.vue";
import ODGroupDisciplinesSelect from "../JournalEditBlock/OD-GroupDisciplinesSelect.vue";
import ODStudentGroupsSelect from "../JournalEditBlock/OD-StudentGroupsSelect.vue";
import { CalendarDate } from "@internationalized/date";
import type { DateValue } from "reka-ui";
import {
  CalendarRangeIcon,
  Grid2X2PlusIcon,
  SearchIcon,
} from "lucide-vue-next";

const props = withDefaults(
  defineProps<{
    embedded?: boolean;
  }>(),
  {
    embedded: false,
  },
);

const date: Date = new Date(Date.now());

function convertDateToCalendarDate(date: Date): DateValue {
  const day: number = date.getDate();
  const month: number = date.getMonth() + 1;
  const year: number = date.getFullYear();
  return new CalendarDate(year, month, day);
}
</script>

<template>
  <section
    class="gradebook-toolbar card-primary shadow-none!"
    :class="{ 'gradebook-toolbar--embedded': props.embedded }"
  >
    <div class="gradebook-toolbar__period-row">
      <span class="gradebook-toolbar__period-label">Период журнала</span>
      <GradebookPeriodBlock :compact="true" />
    </div>

    <div class="gradebook-toolbar__controls">
      <div class="gradebook-toolbar__field">
        <ODStudentGroupsSelect />
      </div>

      <div class="gradebook-toolbar__field">
        <ODGroupDisciplinesSelect />
      </div>

      <InputWithIcon class="gradebook-toolbar__search" :place-holder="'Найти студента по фамилии'" :icon="SearchIcon" />

      <Button class="gradebook-toolbar__button" :variant="'sixth'">
        <Grid2X2PlusIcon />
        <span>Создать колонку</span>
      </Button>

      <div class="gradebook-toolbar__date">
        <Popover>
          <PopoverTrigger :as-child="true">
            <Button class="gradebook-toolbar__button" :variant="'secondary'">
              <CalendarRangeIcon />
              <span>Выбрать дату</span>
            </Button>
          </PopoverTrigger>
          <PopoverContent class="gradebook-toolbar__calendar" :align="'end'">
            <Calendar :default-value="convertDateToCalendarDate(date)" :locale="'ru-RU'" />
          </PopoverContent>
        </Popover>
      </div>
    </div>
  </section>
</template>

<style scoped lang="css">
.gradebook-toolbar {
  display: grid;
  gap: 0.7rem;
  border: 1px solid hsl(0 0% 100% / 0.05);
  border-radius: 1.25rem;
  padding: clamp(0.75rem, 1.2vw, 0.95rem);
}

.gradebook-toolbar--embedded {
  border: none;
  background: transparent;
  padding: 0;
}

.gradebook-toolbar__period-row {
  display: flex;
  align-items: center;
  gap: 0.6rem;
  flex-wrap: wrap;
}

.gradebook-toolbar__period-label {
  flex-shrink: 0;
  font-size: 0.72rem;
  line-height: 1;
  font-weight: 700;
  color: hsl(0 0% 100% / 0.66);
  text-transform: uppercase;
  letter-spacing: 0.08em;
}

.gradebook-toolbar__controls {
  display: grid;
  grid-template-columns:
    minmax(10rem, 12rem) minmax(10rem, 12rem) minmax(13rem, 1fr) max-content max-content max-content;
  gap: 0.65rem;
  align-items: center;
}

.gradebook-toolbar__field {
  min-width: 0;
}

.gradebook-toolbar__date {
  min-width: 0;
}

.gradebook-toolbar__date :deep(button) {
  width: 100%;
}

.gradebook-toolbar__button {
  min-width: 0;
  height: 2.65rem;
  justify-content: flex-start;
  padding-inline: 0.82rem;
  white-space: nowrap;
  box-shadow: none;
}

.gradebook-toolbar__search:deep([data-slot="field"]) {
  min-width: 0;
}

.gradebook-toolbar__search:deep([data-slot="input-group-control"]) {
  font-size: 0.86rem;
  line-height: 1.2;
}

.gradebook-toolbar__search:deep([data-slot="input-group-control"]::placeholder) {
  font-size: inherit;
}

.gradebook-toolbar__calendar {
  width: auto;
  padding: 0;
}

@media (min-width: 1440px) {
  .gradebook-toolbar {
    height: 100%;
    min-height: 0;
    grid-template-rows: auto minmax(0, 1fr);
    padding: clamp(0.9rem, 1.4vw, 1rem);
    overflow: auto;
  }

  .gradebook-toolbar__period-row {
    align-items: flex-start;
    gap: 0.55rem;
  }

  .gradebook-toolbar__controls {
    grid-template-columns: minmax(0, 1fr);
    align-content: start;
    gap: 0.7rem;
  }

  .gradebook-toolbar__button,
  .gradebook-toolbar__date {
    width: 100%;
  }

  .gradebook-toolbar__button {
    justify-content: flex-start;
  }
}

@media (max-width: 1099px) {
  .gradebook-toolbar__controls {
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }

  .gradebook-toolbar__button {
    width: 100%;
  }
}

@media (max-width: 720px) {
  .gradebook-toolbar__period-row {
    align-items: flex-start;
    gap: 0.4rem;
  }

  .gradebook-toolbar__controls {
    grid-template-columns: minmax(0, 1fr);
  }
}
</style>
