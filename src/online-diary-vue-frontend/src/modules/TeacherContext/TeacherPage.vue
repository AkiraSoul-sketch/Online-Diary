<script lang="ts">
import Avatar from "@/components/ui/avatar/Avatar.vue";
import AvatarFallback from "@/components/ui/avatar/AvatarFallback.vue";
import Button from "@/components/ui/button/Button.vue";
import { Card } from "@/components/ui/card";

import CardTitle from "@/components/ui/card/CardTitle.vue";
import Item from "@/components/ui/item/Item.vue";
import ItemContent from "@/components/ui/item/ItemContent.vue";
import ItemDescription from "@/components/ui/item/ItemDescription.vue";
import ItemFooter from "@/components/ui/item/ItemFooter.vue";
import ItemGroup from "@/components/ui/item/ItemGroup.vue";
import ItemHeader from "@/components/ui/item/ItemHeader.vue";
import ItemMedia from "@/components/ui/item/ItemMedia.vue";
import ItemTitle from "@/components/ui/item/ItemTitle.vue";
import { TableCell } from "@/components/ui/table";
import Table from "@/components/ui/table/Table.vue";
import TableBody from "@/components/ui/table/TableBody.vue";
import TableHead from "@/components/ui/table/TableHead.vue";
import TableHeader from "@/components/ui/table/TableHeader.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import { BookOpenIcon } from "lucide-vue-next";

interface Desceplina {
  name: string;
  groupname: string;
  gradescount: number;
}
function generate(count: number): Desceplina[] {
  const DesceplinaName = "Дисциплина 1";

  const Desceplina = [];
  for (let A = 0; A < count; A++) {
    const desceplina: Desceplina = {
      name: DesceplinaName,
      groupname: "ПСК-3-25",
      gradescount: 26,
    };
    Desceplina.push(desceplina);
  }
  return Desceplina;
}

export default {
  components: {
    Card,
    CardTitle,
    Item,
    ItemHeader,
    ItemTitle,
    Table,
    TableBody,
    TableRow,
    TableCell,
    TableHeader,
    TableHead,
    Button,
    BookOpenIcon,
    ItemGroup,
    ItemMedia,
    Avatar,
    AvatarFallback,
    ItemContent,
    ItemDescription,
    ItemFooter,
  },
  data() {
    return { GenerateDisciplina: generate(1) };
  },
};
</script>

<template>
  <section :class="'p-2'">
    <Card :class="'w-full gap-2'">
      <Item>
        <ItemTitle :class="'text-xl'">Мои группы:</ItemTitle>
      </Item>
      <Item :class="'mx-2'" :variant="'muted'">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead :class="'text-center'">
                Название дисциплины
              </TableHead>
              <TableHead :class="'text-center'"> Группа </TableHead>
              <TableHead :class="'text-center'"> Оценок проставить </TableHead>
              <TableHead :class="'text-center'"> Действия </TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow v-for="Desceplina of GenerateDisciplina">
              <TableCell :class="'text-center'">
                {{ Desceplina.name }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.groupname }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.gradescount }}
              </TableCell>
              <TableCell :class="'text-center'">
                <Button><BookOpenIcon></BookOpenIcon>Открыть</Button>
              </TableCell>
            </TableRow>
            <TableRow v-for="Desceplina of GenerateDisciplina">
              <TableCell :class="'text-center'">
                {{ Desceplina.name }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.groupname }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.gradescount }}
              </TableCell>
              <TableCell :class="'text-center'">
                <Button><BookOpenIcon></BookOpenIcon>Открыть</Button>
              </TableCell>
            </TableRow>
            <TableRow v-for="Desceplina of GenerateDisciplina">
              <TableCell :class="'text-center'">
                {{ Desceplina.name }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.groupname }}
              </TableCell>
              <TableCell :class="'text-center'">
                {{ Desceplina.gradescount }}
              </TableCell>
              <TableCell :class="'text-center'">
                <Button><BookOpenIcon></BookOpenIcon>Открыть</Button>
              </TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </Item>
      <Item :class="'py-0'">
        <ItemTitle :class="'text-xl p-0'">Недавняя активность:</ItemTitle>
      </Item>
      <Item :class="'p-0 mx-2'" :variant="'muted'">
        <div :class="'flex w-full flex-col gap-1'">
          <ItemGroup>
            <Item asChild role="listitem">
              <ItemHeader :variant="'icon'">
                <Avatar>
                  <AvatarFallback>АА</AvatarFallback>
                </Avatar>
                <ItemContent>
                  <ItemTitle> Проставление оценки </ItemTitle>
                  <ItemDescription>
                    Поставил А. А. Ахматшину оценку 2 по дисциплине: Информатика
                  </ItemDescription>
                  <ItemFooter>Час назад.</ItemFooter>
                </ItemContent>
              </ItemHeader>
            </Item>
          </ItemGroup>
        </div>
      </Item>
    </Card>
  </section>
</template>
