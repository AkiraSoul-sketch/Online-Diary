<script lang="ts">
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
import { PencilIcon } from "lucide-vue-next";
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

export default {
  components: {
    Item,
    ItemTitle,
    Table,
    TableHead,
    TableHeader,
    TableRow,
    TableBody,
    TableFooter,
    TableCell,
    Button,
    ButtonGroup,
    Card,
    CardTitle,
    CardContent,
    Input,
    Separator,
    PencilIcon,
    DisciplineListItem,
  },
  methods: {
    generateDisciplines(amount: number): Discipline[] {
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
    },
  },
};
</script>

<template>
  <section :class="'h-screen my-20 px-75 flex flex-col gap-5'">
    <section :class="'flex flex-wrap shrink gap-5'">
      <Item :class="'flex-[1_1_200px] shrink'">
        <ItemTitle :text="'Всего дисциплин'" />
      </Item>
      <Item :class="'flex-[1_1_200px] shrink'">
        <ItemTitle :text="'Не преподаются'" />
      </Item>
      <Item :class="'flex-[1_1_200px] shrink'">
        <ItemTitle :text="'Активные'" />
      </Item>
    </section>
    <section :class="'block'">
      <Card>
        <CardContent
          :class="'flex flex-row justify-around w-full shrink items-center align-middle gap-2'"
        >
          <Input
            :class="'bg-(--bg-light)'"
            placeholder="Поиск по названию дисциплины"
          />
          <Separator :orientation="'vertical'" />
          <Button variant="outline">Добавить дисциплину</Button>
        </CardContent>
      </Card>
    </section>
    <section :class="'flex flex-col shrink gap-10'">
      <div
        :class="'rounded-md border border-border overflow-hidden bg-(--bg-light)'"
      >
        <Table :class="'w-full'">
          <TableHeader :class="'block'">
            <TableRow
              :supress-hover-effect="true"
              :class="'flex flex-row flex-wrap justify-around'"
            >
              <TableHead
                :class="'px-0 flex flex-4 shrink flex-wrap justify-center items-center'"
                >Название</TableHead
              >
              <TableHead
                :class="'px-0 flex flex-1 shrink flex-wrap justify-center items-center'"
                >Группа</TableHead
              >
              <TableHead
                :class="'px-0 flex flex-2 shrink flex-wrap justify-center items-center'"
                >Преподает</TableHead
              >
              <TableHead
                :class="'px-0 flex flex-1 shrink flex-wrap justify-center items-center'"
                >Семестр</TableHead
              >
              <TableHead
                :class="'px-0 flex flex-1 shrink flex-wrap justify-center items-center'"
              ></TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow :class="'flex flex-row flex-wrap justify-around'">
              <TableCell
                :class="'py-2 px-0 flex flex-4 shrink justify-center items-center'"
              >
                Дисциплина
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
                Группа
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-2 shrink justify-center items-center'"
              >
                Преподает
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
                Семестр
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
                <Button
                  :class="'cursor-pointer rounded-3xl w-6 h-6'"
                  :variant="'outline'"
                >
                  <PencilIcon :size="15" />
                </Button>
              </TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </div>
    </section>
  </section>
</template>
