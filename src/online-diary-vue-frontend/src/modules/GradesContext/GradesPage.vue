<script setup lang="ts">
import { useColorMode } from "@vueuse/core";
import Label from "../../components/ui/label/Label.vue";
import SidebarProvider from "../../components/ui/sidebar/SidebarProvider.vue";
import Sidebar from "../../components/ui/sidebar/Sidebar.vue";
import SidebarHeader from "../../components/ui/sidebar/SidebarHeader.vue";
import SidebarContent from "../../components/ui/sidebar/SidebarContent.vue";
import Button from "../../components/ui/button/Button.vue";
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import CardFooter from "../../components/ui/card/CardFooter.vue";
import CardContent from "../../components/ui/card/CardContent.vue";
import CardDescription from "../../components/ui/card/CardDescription.vue";
import Select from "../../components/ui/select/Select.vue";
import SelectContent from "../../components/ui/select/SelectContent.vue";
import SelectGroup from "../../components/ui/select/SelectGroup.vue";
import SelectTrigger from "../../components/ui/select/SelectTrigger.vue";
import SelectValue from "../../components/ui/select/SelectValue.vue";
import SelectItem from "../../components/ui/select/SelectItem.vue";
import Table from "../../components/ui/table/Table.vue";
import TableHeader from "../../components/ui/table/TableHeader.vue";
import TableHead from "../../components/ui/table/TableHead.vue";
import TableRow from "../../components/ui/table/TableRow.vue";
import TableBody from "../../components/ui/table/TableBody.vue";
import TableCell from "../../components/ui/table/TableCell.vue";
import Input from "../../components/ui/input/Input.vue";
import {
  BookOpenCheckIcon,
  BookOpenTextIcon,
  PenIcon,
  SearchIcon,
  XCircleIcon,
} from "lucide-vue-next";

const mode = useColorMode();
</script>

<template>
  <SidebarProvider>
    <Sidebar>
      <SidebarHeader>
        <Card>
          <CardTitle>
            <Label :class="'text-2xl mx-2'">Online</Label>
            <Label :class="'text-2xl mx-10'">Diary</Label>
          </CardTitle>
          <CardFooter>
            <Label :class="'justify-end'">Онлайн журнал</Label>
          </CardFooter>
        </Card>
      </SidebarHeader>
      <SidebarContent>
        <Button>Element</Button>
      </SidebarContent>
    </Sidebar>
    <!-- page begin -->
    <section class="gap-0 p-0 mx-2 w-full">
      <!-- // page container -->
      <!-- // page container -->
      <Card :class="'w-full h-full gap-2 my-0'">
        <!-- page title -->
        <CardTitle :class="'mx-2'">
          <Label :class="'text-2xl'">Группа: ПСК-3-25</Label>
          <Label :class="'text-2xl'">Дисциплина 1</Label>
        </CardTitle>
        <CardContent :class="'px-2'">
          <section class="grid grid-cols-2 gap-2">
            <!-- edit journal items here -->
            <div :class="'grid grid-cols-2 gap-2'">
              <Card :class="'inner-card-1 gap-0 p-2 col-span-full'">
                <CardTitle>
                  <Label :class="'text-2xl'">
                    <BookOpenTextIcon> </BookOpenTextIcon>
                    Выбрать журнал
                  </Label>
                </CardTitle>
                <CardDescription :class="'my-2'">
                  Вы можете изменить группу и дисциплину, выбрав другую из
                  списка.
                </CardDescription>
                <CardContent :class="'p-0'">
                  <!-- group/disciplines selects -->
                  <section class="grid grid-cols-2 gap-2 w-full h-full">
                    <!-- group -->
                    <Select>
                      <SelectTrigger :class="'w-full'">
                        <SelectValue :placeholder="'Группы'"> </SelectValue>
                      </SelectTrigger>
                      <SelectContent>
                        <SelectGroup>
                          <SelectItem value="ПСК-3-25">ПСК-3-25</SelectItem>
                          <SelectItem value="ПСК-3-26">ПСК-3-26</SelectItem>
                          <SelectItem value="ПСК-3-27">ПСК-3-27</SelectItem>
                        </SelectGroup>
                      </SelectContent>
                    </Select>
                    <!-- discipline -->
                    <Select>
                      <SelectTrigger :class="'w-full'">
                        <SelectValue :placeholder="'Дисциплины'"> </SelectValue>
                      </SelectTrigger>
                      <SelectContent>
                        <SelectGroup>
                          <SelectItem value="Дисциплина 1"
                            >Дисциплина 1</SelectItem
                          >
                          <SelectItem value="Дисциплина 2"
                            >Дисциплина 2</SelectItem
                          >
                          <SelectItem value="Дисциплина 3"
                            >Дисциплина 3</SelectItem
                          >
                        </SelectGroup>
                      </SelectContent>
                    </Select>
                  </section>
                </CardContent>
              </Card>
              <!-- мини информация -->
              <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
                <CardTitle>
                  <Label :class="'text-xl'">Журнал Март 2026</Label>
                  <Label :class="'text-xl'"></Label>
                </CardTitle>
              </Card>
              <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
                <CardTitle>
                  <Label :class="'text-xl'">Placeholder</Label>
                  <Label :class="'text-xl'"></Label>
                </CardTitle>
              </Card>
              <!-- поиск студента -->
              <Card
                :class="'inner-card-1 flex flex-row gap-2 p-2 w-full col-span-2'"
              >
                <Input :class="'col-span-3'" :placeholder="'Поиск студента'"
                  >Text:</Input
                >
                <Button :size="'icon-sm'">
                  <SearchIcon></SearchIcon>
                </Button>
                <Button :size="'icon-sm'">
                  <XCircleIcon></XCircleIcon>
                </Button>
              </Card>
            </div>
            <!-- темы за которые выставлены оценки -->
            <div>
              <Card :class="'inner-card-1 gap-0 p-2 h-full'">
                <CardTitle>
                  <Label :class="'text-2xl'">
                    <BookOpenCheckIcon></BookOpenCheckIcon>
                    Темы: 2</Label
                  >
                </CardTitle>
                <CardContent>
                  <Table>
                    <TableHeader>
                      <TableRow :class="'p-0 gap-0'">
                        <TableHead :class="'p-0.5 gap-0.5'"></TableHead>
                        <TableHead :class="'p-0.5 gap-0.5'"> Тема </TableHead>
                        <TableHead
                          :class="'flex p-0.5 gap-0.5 items-center justify-center w-fit'"
                        >
                        </TableHead>
                      </TableRow>
                    </TableHeader>
                    <TableBody>
                      <TableRow>
                        <TableCell :class="'p-0.5 gap-0.5'"> 1 </TableCell>
                        <TableCell :class="'p-0.5 gap-0.5'"> Тема 1 </TableCell>
                        <TableCell
                          :class="'p-0.5 gap-0.5 flex items-center justify-center w-fit'"
                        >
                          <Button
                            :class="'flex-none p-0 w-1 rounded-xl h-6 flex items-center justify-center'"
                          >
                            <PenIcon :size="15"> </PenIcon>
                          </Button>
                        </TableCell>
                      </TableRow>
                      <TableRow>
                        <TableCell :class="'p-0.5 gap-0.5'"> 2 </TableCell>
                        <TableCell :class="'p-0.5 gap-0.5'"> Тема 2 </TableCell>
                        <TableCell
                          :class="'p-0.5 gap-0.5 flex items-center justify-center w-fit'"
                        >
                          <Button
                            :class="'flex-none p-0 w-1 rounded-xl h-6 flex items-center justify-center'"
                          >
                            <PenIcon :size="15"> </PenIcon>
                          </Button>
                        </TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </CardContent>
              </Card>
            </div>
            <section class="col-span-full">
              <Card :class="'inner-card-1 gap-0 p-2'">
                <CardTitle>
                  <Label :class="'text-2xl'">Оценки:</Label>
                </CardTitle>
              </Card>
            </section>
          </section>
        </CardContent>
      </Card>
    </section>
  </SidebarProvider>
</template>
