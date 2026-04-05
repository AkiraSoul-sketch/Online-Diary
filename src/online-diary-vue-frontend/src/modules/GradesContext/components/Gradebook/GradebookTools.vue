<script setup lang="ts">
import Button from "@/components/ui/button/Button.vue";
import Calendar from "@/components/ui/calendar/Calendar.vue";
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import Popover from "@/components/ui/popover/Popover.vue";
import PopoverContent from "@/components/ui/popover/PopoverContent.vue";
import PopoverTrigger from "@/components/ui/popover/PopoverTrigger.vue";
import InputWithIcon from "@/modules/Common/Components/InputWithIcon.vue";
import { CalendarDate } from "@internationalized/date";
import {
  ChevronDownIcon,
  Grid2X2PlusIcon,
  LockIcon,
  SaveIcon,
  SearchIcon,
} from "lucide-vue-next";
import type { DateValue } from "reka-ui";

// текущая дата. Является временным для отображения рендера и корректной работы
// функции convertDateToCalendarDate().
const date: Date = new Date(Date.now());

function convertDateToCalendarDate(date: Date): DateValue {
  const day: number = date.getDay();
  const month: number = date.getMonth();
  const year: number = date.getFullYear();
  const calendarDate = new CalendarDate(year, month, day);
  return calendarDate;
}
</script>

<template>
  <Card :class="'card-primary p-2'">
    <CardContent :class="'flex gap-2 flex-row'">
      <InputWithIcon :class="'w-full'" :place-holder="'Поиск студента'" :icon="SearchIcon" />
      <Button :class="'p-1 w-auto text-responsive-tertiary'" :variant="'sixth'" :size="'icon'">
        <Grid2X2PlusIcon />
        <label :class="'hidden md:inline'">Создать колонку</label>
      </Button>
      <Button :class="'p-1 w-auto text-responsive-tertiary'" :variant="'secondary'" :size="'icon'">
        <LockIcon />
        <label :class="'hidden md:inline'">Блокировать таблицу</label>
      </Button>
      <Button :class="'p-1 w-auto text-responsive-tertiary'" :variant="'quaternary'" :size="'icon'">
        <SaveIcon />
        <label :class="'hidden md:inline'">Сохранить</label>
      </Button>
      <Popover :class="'w-auto'">
        <PopoverTrigger :as-child="true">
          <Button :class="'text-responsive-tertiary'" :variant="'tetriary'">
            <label>Выбрать дату</label>
            <ChevronDownIcon />
          </Button>
        </PopoverTrigger>
        <PopoverContent>
          <Calendar :default-value="convertDateToCalendarDate(date)" :locale="'ru-RU'" />
        </PopoverContent>
      </Popover>
    </CardContent>
  </Card>
</template>
