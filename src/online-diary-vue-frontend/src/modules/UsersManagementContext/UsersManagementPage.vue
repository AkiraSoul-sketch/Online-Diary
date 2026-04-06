<script setup lang="ts">
import { ref } from "vue";
import Card from "@/components/ui/card/Card.vue";
import Button from "@/components/ui/button/Button.vue";
import Input from "@/components/ui/input/Input.vue";
import {
  PlusIcon,
  PenIcon,
  CheckCheck,
  CheckCheckIcon,
  FilterIcon,
} from "lucide-vue-next";

import {
  Drawer,
  DrawerContent,
  DrawerHeader,
  DrawerTitle,
  DrawerClose,
  DrawerDescription,
} from "@/components/ui/drawer";
import type { FocusOutsideEvent, PointerDownOutsideEvent } from "reka-ui";
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectLabel,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";

type User = {
  id: number;
  name: string;
  login: string;
  email: string;
  emailConfirmed: boolean;
  lastSeen: string;
  role: "администратор" | "студент" | "преподаватель";
};

const users = ref<User[]>([
  {
    id: 1,
    name: "Иван Иванов",
    login: "ivan.ivanov",
    email: "ivan@example.com",
    emailConfirmed: true,
    lastSeen: "2026-03-28 09:12",
    role: "студент",
  },
  {
    id: 2,
    name: "Мария Петрова",
    login: "maria.petrov",
    email: "maria@example.com",
    emailConfirmed: false,
    lastSeen: "2026-03-27 18:40",
    role: "преподаватель",
  },
  {
    id: 3,
    name: "Алексей Смирнов",
    login: "alex.smirnov",
    email: "alex@example.com",
    emailConfirmed: true,
    lastSeen: "2026-03-29 08:05",
    role: "администратор",
  },
]);

const selectedUser = ref<User | null>(users.value[0] ?? null);
const isSelected = ref<boolean>(false);

function selectUser(u: User) {
  selectedUser.value = u;
  isSelected.value = true;
}
function Handleoutsideclic(event: PointerDownOutsideEvent | FocusOutsideEvent) {
  isSelected.value = false;
}
</script>

<template>
  <!-- Основной контейнер страницы -->
  <div class="flex flex-col min-h-0 flex-1 gap-4 p-4">
    <!-- Статистические карточки -->
    <!-- Комментарий: блок статистики с четырьмя карточками -->
    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-4">
      <Card :class="'card-sixth p-4 rounded-sm'">
        <div>
          <div class="text-responsive-primary font-semibold">
            Всего пользователей
          </div>
          <div class="text-responsive-secondary">1 234</div>
          <div class="text-responsive-primary font-semibold">
            Пользователей онлайн
          </div>
          <div class="text-responsive-secondary">123</div>
        </div>
        <div>
          <div class="text-responsive-primary font-semibold">
            Преподавателей
          </div>
          <div class="text-responsive-secondary">45</div>
          <div class="text-responsive-primary font-semibold">Студентов</div>
          <div class="text-responsive-secondary">1 089</div>
        </div>
      </Card>
    </div>

    <!-- CRUD и информация о пользователе -->
    <!-- Комментарий: основной flex-контейнер для списка и карточки пользователя -->
    <div class="flex flex-col min-h-0 flex-1 gap-4 lg:flex-row">
      <!-- Левый блок: список пользователей -->
      <!-- Комментарий: панель с поиском и кнопкой Создать + список пользователей -->
      <div class="flex flex-col min-h-0 flex-1 lg:w-2/3 gap-2">
        <!-- Панель поиска и создания -->
        <!-- Комментарий: input поиска и кнопка Создать -->
        <div
          class="flex items-center justify-between gap-2 p-2 shadow-(--shadow-basic) rounded-sm"
        >
          <div class="flex-1 mr-2 btn-primary-accent">
            <Input class="w-full" placeholder="Поиск пользователя..." />
          </div>
          <Select multiple>
            <SelectTrigger class="btn-primary-accent">
              <SelectValue>
                <FilterIcon />
              </SelectValue>
            </SelectTrigger>
            <SelectContent>
              <SelectGroup>
                <SelectLabel> Роль </SelectLabel>
                <SelectItem value="преподаватель">Преподаватель</SelectItem>
                <SelectItem value="студент">Студент</SelectItem>
                <SelectItem value="администратор">Администратор</SelectItem>
              </SelectGroup>
            </SelectContent>
          </Select>
          <Button class="flex items-center gap-2 px-3 py-1 btn-primary-accent">
            <PlusIcon :size="16" />
            <span class="text-responsive-tertiary">Создать</span>
          </Button>
        </div>

        <!-- Список пользователей -->
        <!-- Комментарий: вертикальный список с кнопкой редактирования для каждой записи -->
        <div
          class="flex-1 min-h-0 overflow-auto p-2 shadow-(--shadow-basic) rounded-sm"
        >
          <ul class="divide-y">
            <li
              v-for="user in users"
              :key="user.id"
              class="flex items-center justify-between p-3"
            >
              <div>
                <div class="text-responsive-primary font-medium">
                  {{ user.name }}
                </div>
                <div class="text-responsive-tertiary text-muted-foreground">
                  {{ user.email }} · {{ user.login }}
                </div>
              </div>

              <div class="flex items-center gap-3">
                <div class="text-responsive-tertiary text-muted-foreground">
                  {{ user.role }}
                </div>
                <Button
                  class="px-2 py-1 btn-primary-accent"
                  @click="selectUser(user)"
                >
                  <PenIcon :size="15" />
                </Button>
              </div>
            </li>
          </ul>
        </div>
      </div>

      <!-- Правый блок: информация о выбранном пользователе -->
      <!-- Комментарий: карточка с детальной информацией о пользователе -->
    </div>
  </div>
  <Drawer :open="isSelected" :no-body-styles="true" :direction="'bottom'">
    <DrawerContent
      @interact-outside="Handleoutsideclic"
      :class="'item-bg-primary'"
    >
      <div class="flex flex-col gap-4 p-4">
        <Input type="FIO" placeholder="ФИО" />
        <Input type="login" placeholder="Логин" />
        <Input type="email" placeholder="Почта" />
        <Input type="Role" placeholder="Роль" />
      </div>
      <DrawerClose>
        <div class="flex p-4 justify-center">
          <Button v-on:click="isSelected = false"> Закрыть </Button>
        </div>
      </DrawerClose>
    </DrawerContent>
  </Drawer>
</template>
