<script lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { ItemTitle } from "@/components/ui/item";
import Item from "@/components/ui/item/Item.vue";
import ItemContent from "@/components/ui/item/ItemContent.vue";
import ItemFooter from "@/components/ui/item/ItemFooter.vue";
import Label from "@/components/ui/label/Label.vue";
import Separator from "@/components/ui/separator/Separator.vue";
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarGroup,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuBadge,
  SidebarMenuButton,
  SidebarMenuItem,
  SidebarProvider,
} from "@/components/ui/sidebar";
import SidebarMenuAction from "@/components/ui/sidebar/SidebarMenuAction.vue";
import SidebarMenuSection from "./components/SidebarMenuSection.vue";
import {
  GraduationCap,
  KeyRound,
  LayersIcon,
  PencilIcon,
  Table2Icon,
  UsersIcon,
} from "lucide-vue-next";
import SidebarMenuSectionItem from "./components/SidebarMenuSectionItem.vue";
import GradesPage from "../GradesContext/GradesPage.vue";

export default {
  components: {
    Sidebar,
    SidebarHeader,
    SidebarContent,
    SidebarGroup,
    SidebarFooter,
    SidebarProvider,
    SidebarMenu,
    SidebarMenuItem,
    SidebarMenuButton,
    SidebarMenuAction,
    SidebarMenuBadge,
    SidebarMenuSection,
    SidebarMenuSectionItem,
    ItemContent,
    Item,
    ItemTitle,
    ItemFooter,
    Label,
    Avatar,
    AvatarFallback,
    Separator,
    Table2Icon,
    PencilIcon,
    UsersIcon,
    LayersIcon,
    GraduationCap,
    KeyRound,
    GradesPage,
  },
  methods: {
    joinClasses(...classes: string[]): string {
      return classes.filter(Boolean).join(" ");
    },
  },
  setup() {
    const sideBarCardStyle: string = "bg-sky-600 border border-sky-700";
    return {
      sideBarCardStyle,
    };
  },
};
</script>
<template>
  <SidebarProvider
    :class="'flex flex-col h-full min-h-0 justify-start items-center'"
  >
    <SidebarHeader>
      <Item :class="joinClasses(sideBarCardStyle, 'w-full')">
        <ItemTitle :class="'text-zinc-200'">Онлайн журнал</ItemTitle>
        <ItemFooter :class="'text-zinc-600'">
          Филиала СибГУ в г.Лесосибирске
        </ItemFooter>
      </Item>
    </SidebarHeader>
    <SidebarContent :class="'w-full'">
      <SidebarGroup>
        <Item :class="joinClasses(sideBarCardStyle, 'w-full')">
          <ItemContent :class="'flex items-center gap-2'">
            <Avatar :class="'h-18 w-18'">
              <AvatarFallback>ПП</AvatarFallback>
            </Avatar>
            <Separator />
            <div :class="'flex flex-col items-center'">
              <Label :class="'text-zinc-200 text-[0.8em]'"
                >Преподаватель П. П.</Label
              >
              <Label :class="'text-zinc-200 text-[0.8em]'">Преподаватель</Label>
            </div>
          </ItemContent>
        </Item>
      </SidebarGroup>
      <SidebarGroup :class="'flex min-h-0 h-full grow'">
        <SidebarMenu
          :class="
            joinClasses(
              sideBarCardStyle,
              'rounded-sm',
              'p-1',
              'flex min-h-0 h-full grow',
            )
          "
        >
          <SidebarMenuSectionItem :name="'Страница преподавателя'">
            <GraduationCap :size="15" />
          </SidebarMenuSectionItem>
          <SidebarMenuSectionItem :name="'Журналы'">
            <Table2Icon :size="15" />
          </SidebarMenuSectionItem>
          <SidebarMenuSectionItem :name="'Участники'">
            <UsersIcon :size="15" />
          </SidebarMenuSectionItem>
          <SidebarMenuSectionItem :name="'Дисциплины'">
            <LayersIcon :size="15" />
          </SidebarMenuSectionItem>
          <SidebarMenuSectionItem :name="'Учетные записи'">
            <KeyRound :size="15" />
          </SidebarMenuSectionItem>
        </SidebarMenu>
      </SidebarGroup>
    </SidebarContent>
    <SidebarFooter :class="'bg-amber-200 inline-block w-full'">
      <label>Футер</label>
    </SidebarFooter>
  </SidebarProvider>
</template>
