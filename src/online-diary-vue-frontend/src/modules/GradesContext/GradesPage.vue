<script lang="ts">
import ODSidebar from "./components/OD-Sidebar.vue";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardTitle,
  CardContent,
  CardDescription,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import SidebarProvider from "@/components/ui/sidebar/SidebarProvider.vue";
import ODGradesPagePageTitle from "./components/OD-GradesPage-PageTitle.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import ODJournalDateBlock from "./components/OD-JournalDateBlock.vue";
import ODStudentsSearch from "./components/OD-StudentsSearch.vue";
import ODGradedThemesList from "./components/GradedThemesList/OD-GradedThemesList.vue";
import {
  Table,
  TableBody,
  TableCaption,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Separator } from "@/components/ui/separator";
import { ScrollBar, ScrollArea } from "@/components/ui/scroll-area";
import { Checkbox } from "@/components/ui/checkbox";
import { FieldContent, FieldLabel } from "@/components/ui/field";
import {
  ChevronDownIcon,
  LockIcon,
  SaveIcon,
  SearchCheckIcon,
  SearchXIcon,
  SquarePlus,
} from "lucide-vue-next";
import { Popover, PopoverTrigger } from "@/components/ui/popover";
import Calendar from "@/components/ui/calendar/Calendar.vue";
import PopoverContent from "@/components/ui/popover/PopoverContent.vue";
import type { DateValue } from "reka-ui";
import { CalendarDate } from "@internationalized/date";
import { ref } from "vue";

type ThemeInfo = {
  index: number;
  date: Date;
};

function generateRandomThemes(count: number): ThemeInfo[] {
  const themes: ThemeInfo[] = [];
  const currentDate = new Date();
  for (let i = 0; i < count; i++) {
    const info: ThemeInfo = { date: currentDate, index: i + 1 };
    themes.push(info);
  }
  return themes;
}

function formatDate(date: Date): string {
  const format: "2-digit" = "2-digit";
  const locale = undefined;
  const options: Intl.DateTimeFormatOptions = {
    year: format,
    month: format,
    day: format,
  };
  return date.toLocaleDateString(locale, options);
}

function convertDateToCalendarDate(date: Date): DateValue {
  const day: number = date.getDay();
  const month: number = date.getMonth();
  const year: number = date.getFullYear();
  const calendarDate = new CalendarDate(year, month, day);
  return calendarDate;
}

export default {
  components: {
    Label,
    Button,
    Card,
    CardTitle,
    CardContent,
    CardDescription,
    Input,
    ODSidebar,
    ODGradesPagePageTitle,
    ODJournalEditBlock,
    ODJournalDateBlock,
    SidebarProvider,
    ODStudentsSearch,
    ODGradedThemesList,
    Table,
    TableCaption,
    TableHeader,
    TableRow,
    TableHead,
    TableBody,
    TableCell,
    Separator,
    ScrollArea,
    ScrollBar,
    Checkbox,
    FieldLabel,
    FieldContent,
    Popover,
    PopoverTrigger,
    ChevronDownIcon,
    Calendar,
    PopoverContent,
    SaveIcon,
    LockIcon,
    SquarePlus,
    SearchCheckIcon,
    SearchXIcon,
  },
  data() {
    return {
      theme_indexes: generateRandomThemes(25),
      date: new Date(Date.now()),
      tableHeaderStyles: "text-start px-0 pr-0 p-0 border",
      gradeTableHeaderStyles: "text-center px-0 pr-0 p-0 border",
      gradeThemeHeaderLabelStyles: "block text-center p-1",
    };
  },
  methods: {
    formatDate,
    convertDateToCalendarDate,
  },
  setup() {
    // создание реактивных переменных для хранения ширины элементов таблицы.
    // эти переменные будут обновляться, если им поменять значение.
    // стандартное значение = 0 (defaultWidth).
    const defaultWidth: number = 0;
    const blockTableHeaderWidth = ref(defaultWidth);
    const tableWrapperWidth = ref(defaultWidth);
    const studentTableHeaderWidth = ref(defaultWidth);
    const gradeScoreValueHeaderWidth = ref(defaultWidth);
    const gradeHeaderWidth = ref(defaultWidth);
    return {
      tableWrapperWidth,
      blockTableHeaderWidth,
      studentTableHeaderWidth,
      gradeScoreValueHeaderWidth,
      gradeHeaderWidth,
    };
  },
  mounted() {
    // получение ссылок на HTML DOM-элементы.
    const tableWrapper: HTMLElement | null = this.$refs
      .tableContainerElementRef as HTMLElement | null;
    const blockTableHeaderWrapper: HTMLElement | null = this.$refs[
      "block-table-header"
    ] as HTMLElement | null;
    const studentTableHeaderWrapper: HTMLElement | null = this.$refs[
      "student-table-header"
    ] as HTMLElement | null;
    const gradeScoreValueHeaderWrapper: HTMLElement | null = this.$refs[
      "grade-score-value-header"
    ] as HTMLElement | null;
    const gradeHeaderWrapper: HTMLElement[] | null = this.$refs[
      "grade-value-header"
    ] as HTMLElement[] | null;

    // могут не быть (быть null, если на них нет ref="название"), поэтому нужно проверить
    if (!gradeScoreValueHeaderWrapper) {
      return;
    }

    if (!tableWrapper) {
      return;
    }

    if (!blockTableHeaderWrapper) {
      return;
    }

    if (!studentTableHeaderWrapper) {
      return;
    }

    if (gradeHeaderWrapper && gradeHeaderWrapper.length > 0) {
      const firstGradeHeaderWrapper = gradeHeaderWrapper[0];
      this.gradeHeaderWidth = firstGradeHeaderWrapper.clientWidth;
    }

    // установка новых значений после ресайза окна в переменные при резайсе окна
    // resizeObserve - слушатель изменения размера, внутри себя вызывает функцию.
    // функция принимает массив entries, который содержит информацию о том, какой элемент изменился и его новые размеры.
    // необходимо сопоставлять элементы, перед их изменениями.
    this.tableWrapperWidth = tableWrapper.clientWidth;
    this.blockTableHeaderWidth = blockTableHeaderWrapper.clientWidth;
    this.studentTableHeaderWidth = studentTableHeaderWrapper.clientWidth;
    this.gradeScoreValueHeaderWidth = gradeScoreValueHeaderWrapper.clientWidth;
    const resizeObserver = new ResizeObserver((entries) => {
      for (let entry of entries) {
        const newWidth: number = entry.contentRect.width;
        if (entry.target === tableWrapper) {
          this.tableWrapperWidth = newWidth;
        }
        if (entry.target === blockTableHeaderWrapper) {
          this.blockTableHeaderWidth = newWidth;
        }
        if (entry.target === studentTableHeaderWrapper) {
          this.studentTableHeaderWidth = newWidth;
        }
        if (entry.target === gradeScoreValueHeaderWrapper) {
          this.gradeScoreValueHeaderWidth = newWidth;
        }
        if (gradeHeaderWrapper) {
          if (entry.target === gradeHeaderWrapper[0]) {
            this.gradeHeaderWidth = newWidth;
          }
        }
      }
    });

    resizeObserver.observe(tableWrapper);
    resizeObserver.observe(blockTableHeaderWrapper);
    resizeObserver.observe(studentTableHeaderWrapper);
    resizeObserver.observe(gradeScoreValueHeaderWrapper);
    if (gradeHeaderWrapper) {
      for (let gradeHeader of gradeHeaderWrapper) {
        resizeObserver.observe(gradeHeader);
      }
    }
  },
};
</script>

<template>
  <section class="flex gap-0 p-0 mx-2">
    <!-- // page container -->
    <Card :class="'h-full w-full gap-2 my-0'">
      <!-- page title -->
      <ODGradesPagePageTitle />
      <CardContent :class="'px-2'">
        <section class="grid grid-cols-2 gap-2">
          <!-- edit journal items here -->
          <div :class="'grid grid-cols-2 gap-2'">
            <ODJournalEditBlock />
            <!-- мини информация -->
            <!-- Дата -->
            <ODJournalDateBlock />
            <!-- Placeholder временно -->
            <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
              <CardTitle>
                <Label :class="'text-xl'">Placeholder</Label>
                <Label :class="'text-xl'"></Label>
              </CardTitle>
            </Card>
            <!-- поиск студента -->
            <ODStudentsSearch />
          </div>
          <!-- темы за которые выставлены оценки -->
          <ODGradedThemesList />
        </section>
        <!-- начало таблицы оценок -->
        <section
          ref="tableContainerElementRef"
          :class="'border rounded-md my-2'"
        >
          <!-- контролы (фильтры, сортировка, кнопки над таблицей) -->
          <div :class="'inline-flex gap-2 items-center p-2'">
            <!-- поиск студента -->
            <div :class="'inline-flex gap-2 flex-1 min-w-2/5 grow'">
              <Button
                :size="'icon'"
                :variant="'outline'"
                :id="'student-search-button'"
              >
                <SearchCheckIcon for="student-search-button" />
              </Button>
              <Button
                :size="'icon'"
                :variant="'outline'"
                :id="'student-search-button'"
              >
                <SearchXIcon for="student-search-button" />
              </Button>
              <Input
                :placeholder="'Поиск студента'"
                id="student-search-icon"
              ></Input>
            </div>
            <Button :variant="'outline'" id="save-button">
              <SaveIcon for="save-button" />
              Экспорт</Button
            >
            <Button :variant="'outline'" id="block-icon">
              <LockIcon for="block-icon" />
              Заблокировать</Button
            >
            <Button
              :variant="'outline'"
              id="create-column-icon"
              :class="'inline-flex justify-around'"
            >
              <SquarePlus for="create-column-icon" />
              <Label>Создать колонку </Label>
            </Button>
            <Popover>
              <PopoverTrigger :as-child="true">
                <Button :variant="'outline'">
                  <Label>Выбрать дату</Label>
                  <ChevronDownIcon />
                </Button>
              </PopoverTrigger>
              <PopoverContent>
                <Calendar
                  :default-value="convertDateToCalendarDate(date)"
                ></Calendar>
              </PopoverContent>
            </Popover>
          </div>
          <ScrollArea
            :style="{
              width: `${tableWrapperWidth - 19}px`,
            }"
            :class="'whitespace-nowrap overflow-x-auto'"
          >
            <Table :class="'border-t'">
              <TableHeader :class="'flex'">
                <TableCell :class="tableHeaderStyles">
                  <div ref="block-table-header">
                    <TableHead :class="'text-start px-0 pr-0 p-2'"
                      >Блокировка</TableHead
                    >
                  </div>
                </TableCell>

                <TableCell :class="tableHeaderStyles">
                  <div ref="student-table-header" class="w-80">
                    <TableHead :class="'text-start '">Студент</TableHead>
                  </div>
                </TableCell>
                <TableCell :class="tableHeaderStyles">
                  <div ref="grade-score-value-header">
                    <TableHead :class="''">Успеваемость</TableHead>
                  </div>
                </TableCell>
                <TableCell
                  :class="gradeTableHeaderStyles"
                  v-for="index in theme_indexes"
                >
                  <div ref="grade-value-header">
                    <Label :class="gradeThemeHeaderLabelStyles">
                      {{ formatDate(index.date) }}
                    </Label>
                    <Label :class="gradeThemeHeaderLabelStyles">
                      {{ index.index }}</Label
                    >
                  </div>
                </TableCell>
              </TableHeader>
              <TableBody :class="'flex flex-row items-center justify-start'">
                <div class="inline-flex">
                  <div class="border">
                    <!-- блокировка -->
                    <TableRow
                      :style="{
                        width: `${blockTableHeaderWidth}px`,
                      }"
                      :class="'flex items-center justify-center'"
                    >
                      <TableCell :class="'p-3'">
                        <Button :size="'icon-sm'">
                          <LockIcon />
                        </Button>
                      </TableCell>
                    </TableRow>
                  </div>
                  <!-- студент -->
                  <div
                    :style="{
                      width: `${studentTableHeaderWidth + 2}px`,
                    }"
                    class="border flex"
                  >
                    <TableRow :class="'p-0 w-full'">
                      <TableCell>
                        <TableHead>Студент</TableHead>
                      </TableCell>
                    </TableRow>
                  </div>
                  <div
                    :style="{
                      width: `${gradeScoreValueHeaderWidth + 2}px`,
                    }"
                    class="border flex"
                  >
                    <TableRow :class="'p-0 w-full'">
                      <TableCell>
                        <TableHead>Успев</TableHead>
                      </TableCell>
                    </TableRow>
                  </div>
                  <div
                    :style="{
                      width: `${gradeHeaderWidth + 2}px`,
                    }"
                    class="border block"
                    v-for="index in theme_indexes"
                  >
                    <TableRow :class="'flex p-0 justify-center'">
                      <TableCell>
                        <TableHead>5</TableHead>
                      </TableCell>
                    </TableRow>
                  </div>
                </div>
              </TableBody>
            </Table>
            <ScrollBar :orientation="'horizontal'" />
          </ScrollArea>
        </section>
      </CardContent>
    </Card>
  </section>
</template>
