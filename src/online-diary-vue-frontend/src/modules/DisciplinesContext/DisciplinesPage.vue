<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import { Card, CardContent, CardTitle } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Item, ItemTitle } from "@/components/ui/item";
import Separator from "@/components/ui/separator/Separator.vue";
import {
  TableBody,
  TableCell,
  TableFooter,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import Table from "@/components/ui/table/Table.vue";
import { PencilIcon, PlusIcon } from "lucide-vue-next";
import type { Discipline } from "./models/Discipline";
import DisciplineListItem from "./components/DisciplineListItem.vue";

const DISCIPLINE_NAMES = [
  "Математика",
  "Физика",
  "Программирование",
  "Алгоритмы",
  "Базы данных",
  "Операционные системы",
  "Дискретная математика",
  "Геометрия",
  "История",
  "Иностранный язык",
];

const TEACHERS = [
  "Иванов И.И.",
  "Петров П.П.",
  "Сидоров С.С.",
  "Кузнецова А.А.",
  "Смирнов В.В.",
  "Коваленко Н.Н.",
  "Морозова Е.Е.",
  "Новиков К.К.",
  "Федоров М.М.",
  "Ершова О.О.",
];

function generateDisciplines(amount: number): Discipline[] {
  const disciplines: Discipline[] = [];
  for (let i = 0; i < amount; i++) {
    const name =
      DISCIPLINE_NAMES[Math.floor(Math.random() * DISCIPLINE_NAMES.length)];
    const teacher = TEACHERS[Math.floor(Math.random() * TEACHERS.length)];
    const semester = Math.floor(Math.random() * 8) + 1; // 1..8
    const group = `Группа ${Math.ceil(Math.random() * 6)}`; // Группа 1..6
    const discipline = {
      id: `${i + 1}`,
      name,
      group,
      teacher,
      semester,
    } as Discipline;
    disciplines.push(discipline);
  }

  return disciplines;
}
</script>

<template>
  <!-- Корневой контейнер страницы: flex-col с корректным сжатием -->
  <section class="flex flex-col min-h-0 w-full flex-1 my-6 px-6 gap-6">
    <!-- Статистические карточки -->
    <section class="flex flex-wrap gap-4">
      <Card
        class="bg-block-light-accent-blue-grad font-light-on-light font-bold flex-1 min-w-[200px] shadow-(--shadow-basic)"
      >
        <CardContent class="p-4">
          <div class="text-responsive-secondary">Всего дисциплин</div>
          <div class="text-responsive-primary font-semibold">12</div>
        </CardContent>
      </Card>

      <Card
        class="bg-block-light-accent-blue-grad font-light-on-light font-bold flex-1 min-w-[200px] shadow-(--shadow-basic)"
      >
        <CardContent class="p-4">
          <div class="text-responsive-secondary">
            Преподаются преподавателями
          </div>
          <div class="text-responsive-primary font-semibold">8</div>
        </CardContent>
      </Card>

      <Card
        class="bg-block-light-accent-blue-grad font-light-on-light font-bold flex-1 min-w-[200px] shadow-(--shadow-basic)"
      >
        <CardContent class="p-4">
          <div class="text-responsive-secondary">Активные</div>
          <div class="text-responsive-primary font-semibold">10</div>
        </CardContent>
      </Card>
    </section>

    <!-- Панель: поиск + кнопка Создать -->
    <section>
      <Card class="shadow-(--shadow-basic) bg-block-light-neutral">
        <CardContent class="flex flex-col sm:flex-row items-center gap-3">
          <Input
            class="flex-1 bg-(--bg-light) text-responsive-primary"
            placeholder="Поиск по названию дисциплины"
          />
          <div class="flex items-center gap-2 ml-auto">
            <Button class="bg-button-green flex items-center gap-2">
              <PlusIcon :size="16" />
              <span class="text-responsive-secondary">Создать</span>
            </Button>
          </div>
        </CardContent>
      </Card>
    </section>

    <!-- Нижний CRUD-контейнер: flex-row (на малых — колонка), с корректным сжатием -->
    <section class="flex flex-col md:flex-row flex-1 min-h-0 gap-6">
      <!-- Список дисциплин (основная колонка) -->
      <div
        class="bg-block-light-neutral flex-1 min-h-0 flex flex-col rounded-md overflow-hidden border border-border bg-(--bg-light) shadow-(--shadow-basic)"
      >
        <div class="p-4 border-b border-border">
          <div class="text-responsive-secondary font-semibold">
            Список дисциплин
          </div>
        </div>

        <div class="flex-1 min-h-0 overflow-auto">
          <Table :no-wrapper="true" :class="'w-full'">
            <TableHeader>
              <TableRow
                :supress-hover-effect="true"
                class="flex flex-row flex-wrap"
              >
                <TableHead class="px-4 py-3 flex-[4] text-center"
                  >Название</TableHead
                >
                <TableHead class="px-4 py-3 flex-[2] text-center"
                  >Группировка</TableHead
                >
                <TableHead class="px-4 py-3 flex-[3] text-center"
                  >Преподает</TableHead
                >
                <TableHead class="px-4 py-3 flex-[1] text-center"
                  >Семестр</TableHead
                >
                <TableHead class="px-4 py-3 flex-[1] text-center"></TableHead>
              </TableRow>
            </TableHeader>

            <TableBody>
              <!-- Пример элемента списка (повторяется для демонстрации верстки) -->
              <TableRow class="flex flex-row flex-wrap items-center">
                <TableCell
                  class="px-4 py-3 flex-[4] text-center text-responsive-tertiary"
                  >Математика</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[2] text-center text-responsive-tertiary"
                  >Группа 1, Группа 3</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[3] text-center text-responsive-tertiary"
                  >Иванов И.И.</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[1] text-center text-responsive-tertiary"
                  >1</TableCell
                >
                <TableCell class="px-4 py-3 flex-[1] text-center">
                  <Button
                    class="cursor-pointer rounded-3xl w-8 h-8 flex items-center justify-center"
                    variant="outline"
                  >
                    <PencilIcon :size="15" />
                  </Button>
                </TableCell>
              </TableRow>

              <TableRow class="flex flex-row flex-wrap items-center">
                <TableCell
                  class="px-4 py-3 flex-[4] text-center text-responsive-tertiary"
                  >Программирование</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[2] text-center text-responsive-tertiary"
                  >Группа 2</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[3] text-center text-responsive-tertiary"
                  >Петров П.П.</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[1] text-center text-responsive-tertiary"
                  >2</TableCell
                >
                <TableCell class="px-4 py-3 flex-[1] text-center">
                  <Button
                    class="cursor-pointer rounded-3xl w-8 h-8 flex items-center justify-center"
                    variant="outline"
                  >
                    <PencilIcon :size="15" />
                  </Button>
                </TableCell>
              </TableRow>

              <TableRow class="flex flex-row flex-wrap items-center">
                <TableCell
                  class="px-4 py-3 flex-[4] text-center text-responsive-tertiary"
                  >Базы данных</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[2] text-center text-responsive-tertiary"
                  >Группа 4, Группа 5</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[3] text-center text-responsive-tertiary"
                  >Сидоров С.С.</TableCell
                >
                <TableCell
                  class="px-4 py-3 flex-[1] text-center text-responsive-tertiary"
                  >3</TableCell
                >
                <TableCell class="px-4 py-3 flex-[1] text-center">
                  <Button
                    class="cursor-pointer rounded-3xl w-8 h-8 flex items-center justify-center"
                    variant="outline"
                  >
                    <PencilIcon :size="15" />
                  </Button>
                </TableCell>
              </TableRow>
            </TableBody>
          </Table>
        </div>
      </div>

      <!-- Доп. колонка: место для формы / деталей (пустой блок верстки) -->
      <div
        class="bg-block-light-neutral w-full md:w-80 flex-shrink-0 min-h-0 rounded-md overflow-hidden border border-border bg-(--bg-light) shadow-(--shadow-basic)"
      >
        <div class="p-4">
          <div class="text-responsive-secondary font-semibold">
            Детали / Форма
          </div>
          <div class="text-responsive-tertiary text-slate-500 mt-2">
            Здесь будет форма создания/редактирования дисциплины.
          </div>
        </div>
      </div>
    </section>
  </section>
</template>
