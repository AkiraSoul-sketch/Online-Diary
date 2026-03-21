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
import ItemTitle from "@/components/ui/item/ItemTitle.vue";
import ScrollArea from "@/components/ui/scroll-area/ScrollArea.vue";
import ScrollBar from "@/components/ui/scroll-area/ScrollBar.vue";
import Separator from "@/components/ui/separator/Separator.vue";
import { TableCell } from "@/components/ui/table";
import Table from "@/components/ui/table/Table.vue";
import TableBody from "@/components/ui/table/TableBody.vue";
import TableHead from "@/components/ui/table/TableHead.vue";
import TableHeader from "@/components/ui/table/TableHeader.vue";
import TableRow from "@/components/ui/table/TableRow.vue";
import { BookOpenIcon, Weight } from "lucide-vue-next";

interface Desceplina {
  name: string;
  groupname: string;
  gradescount: number;
}

interface Activity {
  name: string;
  nameProfile: string;
  descripsion: string;
  data: string;
}

function generateAcrivity(count: number): Activity[] {
  const ActivityName =
    "Поставил А. А. Ахматшину оценку 2 по дисциплине: Информатика";

  const Activity = [];
  for (let B = 0; B < count; B++) {
    const activity: Activity = {
      name: ActivityName,
      nameProfile: "А. А. Ахматшину",
      descripsion:
        " Поставил А. А. Ахматшину оценку 2 по дисциплине: Информатика",
      data: "Час назад",
    };
    Activity.push(activity);
  }
  return Activity;
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
    Avatar,
    AvatarFallback,
    ItemContent,
    ItemDescription,
    ItemFooter,
    ScrollArea,
    ScrollBar,
    Separator,
  },
  data() {
    return {
      GenerateAcrivity: generateAcrivity(10),
      GenerateDisciplina: generate(1),
    };
  },
};
</script>

<template>
  <section :class="'p-2 h-screen flex'">
    <Card :class="'h-full w-full gap-2 flex flex-col grow'">
      <div :class="'grow'">
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
                <TableHead :class="'text-center'">
                  Оценок проставить
                </TableHead>
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
      </div>
      <div :class="'grow'">
        <Item :class="'py-0'">
          <ItemTitle :class="'text-xl p-0'">Недавняя активность:</ItemTitle>
        </Item>
        <ScrollArea :class="'h-80 mx-2 rounded-md'">
          <Item :class="'p-0 gap-0'" :variant="'muted'">
            <Table>
              <TableBody>
                <div
                  :class="'flex flex-row w-full justify-between items-center'"
                  v-for="Activity of GenerateAcrivity"
                >
                  <TableRow :class="'flex flex-row w-full justify-between'">
                    <ItemGroup>
                      <Item asChild role="listitem">
                        <ItemHeader :variant="'icon'">
                          <Avatar>
                            <AvatarFallback>АА</AvatarFallback>
                          </Avatar>
                          <ItemContent>
                            <ItemTitle> Проставление оценки </ItemTitle>
                            <ItemDescription>
                              Поставил А. А. Ахматшину оценку 2 по дисциплине:
                              Информатика
                            </ItemDescription>
                            <ItemFooter>Час назад.</ItemFooter>
                          </ItemContent>
                        </ItemHeader>
                      </Item>
                    </ItemGroup>
                    <Avatar :class="'mx-5 self-center'">
                      <AvatarFallback :class="'bg-red-500'" />
                    </Avatar>
                  </TableRow>
                </div>
              </TableBody>
            </Table>
          </Item>
          <ScrollBar :orientation="'vertical'"> </ScrollBar>
        </ScrollArea>
      </div>
    </Card>
  </section>
</template>
