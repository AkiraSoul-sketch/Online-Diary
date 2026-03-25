<script lang="ts">
import { Button } from "@/components/ui/button";
import { Calendar } from "@/components/ui/calendar";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from "@/components/ui/popover";
import { CalendarDate } from "@internationalized/date";
import {
  ChevronDownIcon,
  LockIcon,
  SaveIcon,
  SearchCheckIcon,
  SearchXIcon,
  SquarePlus,
} from "lucide-vue-next";
import type { DateValue } from "reka-ui";

export default {
  components: {
    Button,
    SearchCheckIcon,
    SearchXIcon,
    SaveIcon,
    Input,
    SquarePlus,
    Label,
    Popover,
    PopoverTrigger,
    ChevronDownIcon,
    PopoverContent,
    Calendar,
    LockIcon,
  },
  data() {
    return {
      currentDate: new Date(Date.now()),
    };
  },
  methods: {
    convertDateToCalendarDate(date: Date): DateValue {
      const day: number = date.getDay();
      const month: number = date.getMonth();
      const year: number = date.getFullYear();
      const calendarDate = new CalendarDate(year, month, day);
      return calendarDate;
    },
  },
};
</script>

<template>
  <div
    :class="'inline-flex gap-2 items-center p-2 bg-(--bg-muted) rounded-t-sm w-full'"
  >
    <!-- поиск студента -->
    <div :class="'flex flex-row items-start gap-2 min-w-2/5'">
      <Button
        :class="'border-(--info) shadow-sm text-(--info)'"
        :size="'icon'"
        :variant="'outline'"
        :id="'student-search-button'"
      >
        <SearchCheckIcon for="student-search-button" />
      </Button>
      <Button
        :class="'border-(--info) shadow-sm text-(--info)'"
        :size="'icon'"
        :variant="'outline'"
        :id="'student-search-button'"
      >
        <SearchXIcon for="student-search-button" />
      </Button>
      <Input
        :class="'border-(--info) shadow-xs bg-background hover:bg-accent hover:text-accent-foreground dark:bg-input/30 dark:border-input dark:hover:bg-input/50'"
        :placeholder="'Поиск студента'"
        id="student-search-icon"
      />
    </div>
    <Button
      :variant="'outline'"
      :class="'border-(--info) shadow-sm text-(--info)'"
      id="save-button"
    >
      <SaveIcon for="save-button" />
      Экспорт</Button
    >
    <Button
      :class="'border-(--danger) shadow-sm text-(--danger)'"
      :variant="'outline'"
      id="block-icon"
    >
      <LockIcon for="block-icon" />
      Заблокировать</Button
    >
    <Button
      :variant="'outline'"
      id="create-column-icon"
      :class="'inline-flex border-(--success) shadow-sm text-(--success) justify-around'"
    >
      <SquarePlus for="create-column-icon" />
      <Label>Создать колонку </Label>
    </Button>
    <Popover>
      <PopoverTrigger :as-child="true">
        <Button
          :class="'shadow-sm border-(--text) text(--text)'"
          :variant="'outline'"
        >
          <Label>Выбрать дату</Label>
          <ChevronDownIcon />
        </Button>
      </PopoverTrigger>
      <PopoverContent>
        <Calendar :default-value="convertDateToCalendarDate(currentDate)" />
      </PopoverContent>
    </Popover>
  </div>
</template>
