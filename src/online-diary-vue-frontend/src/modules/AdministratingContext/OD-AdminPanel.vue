<script setup lang="ts">
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import CardFooter from "../../components/ui/card/CardFooter.vue";
import CardContent from "../../components/ui/card/CardContent.vue";
import SidebarProvider from "../../components/ui/sidebar/SidebarProvider.vue";
import Sidebar from "../../components/ui/sidebar/Sidebar.vue";
import SidebarHeader from "../../components/ui/sidebar/SidebarHeader.vue";
import SidebarContent from "../../components/ui/sidebar/SidebarContent.vue";
import { Badge } from "@/components/ui/badge";
import Button from "../../components/ui/button/Button.vue";
import {
  Item,
  ItemContent,
  ItemDescription,
  ItemGroup,
  ItemTitle,
} from "@/components/ui/item";
import CardDescription from "@/components/ui/card/CardDescription.vue";
import Label from "@/components/ui/label/Label.vue";
const array = [1, 2, 3, 4, 5];
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
    <section class="gap-0 mx-2 w-full h-screen p-2">
      <Card :class="'w-full h-full gap-2 grow'">
        <CardTitle :class="'p-2'">
          <Label :class="'text-2xl'">Страница Администратора</Label>
        </CardTitle>
        <div class="flex flex-col gap-2 h-full">
          <section class="grid grid-cols-2 gap-2 p-0 mx-2 h-full">
            <!-- users activity section -->
            <Card>
              <CardTitle :class="'mx-2'">
                <Label> Активность пользователей</Label>
              </CardTitle>
              <CardDescription>
                <Label :class="'text-xl p-2'">Placeholder</Label>
              </CardDescription>
            </Card>
            <!-- recent activity -->
            <Card :class="'w-full'">
              <CardTitle :class="'mx-2'">
                <Label class="text-2x1">Недавние действия</Label>
              </CardTitle>
              <!--notifications-->
              <section class="grid grid-cols-1 gap-2 p-1">
                <div className="flex w-full flex-col gap-0.5">
                  <ItemGroup v-for="a of array">
                    <Item variant="outline">
                      <ItemContent>
                        <ItemDescription
                          :class="'flex flex-row gap-1 justify-between'"
                        >
                          <span>Преподаватель П. П.</span>
                          <span> проставил Студенту С. С. База Данных 3</span>
                          <span>1 час назад</span>
                        </ItemDescription>
                      </ItemContent>
                    </Item>
                  </ItemGroup>
                </div>
              </section>
            </Card>
          </section>
          <!--administration section-->
          <section class="gap-2 p-2 h-full grow">
            <CardTitle :class="'p-2'">
              <Label :class="'text-2xl'">Секция администрирования</Label>
            </CardTitle>
            <div class="grid grid-cols-3 gap-2 w-full grow">
              <section
                v-for="a of [1, 2, 3, 4, 5, 6]"
                :class="'grow min-h-42.5'"
              >
                <!--settings-->
                <Card :class="'p-1'">
                  <CardTitle :class="'p-1'">
                    <Label>Управление студентами</Label>
                  </CardTitle>
                  <CardContent>
                    <div :class="'flex flex-row gap-2 p-2 justify-between'">
                      <div class="flex flex-col gap-2">
                        <Badge variant="secondary">Создание</Badge>
                        <Badge variant="secondary">Редактирование</Badge>
                        <Badge variant="destructive">Архивация</Badge>
                      </div>
                      <div :class="'flex flex-col gap-2 justify-between'">
                        <div></div>
                        <Button
                          :class="'inline-block self-end'"
                          variant="outline"
                          >Открыть</Button
                        >
                      </div>
                    </div>
                  </CardContent>
                </Card>
              </section>
            </div>
          </section>
        </div>
      </Card>
    </section>
  </SidebarProvider>
</template>
