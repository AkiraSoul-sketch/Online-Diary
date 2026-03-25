<script lang="ts">
import { Button } from "@/components/ui/button";
import { ButtonGroup } from "@/components/ui/button-group";
import { Card, CardContent, CardTitle } from "@/components/ui/card";
import CardContent from "@/components/ui/card/CardContent.vue";
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
  },
};
</script>

<template>
  <section :class="'h-screen px-75 flex flex-col gap-20'">
    <section :class="'flex flex-wrap shrink gap-10'">
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
      <div :class="'rounded-md border border-border overflow-hidden'">
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
                Математика
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
                Математика
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-2 shrink justify-center items-center'"
              >
                Математика
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
                Математика
              </TableCell>
              <TableCell
                :class="'py-2 px-0 flex flex-1 shrink justify-center items-center'"
              >
              </TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </div>
    </section>
  </section>
</template>
