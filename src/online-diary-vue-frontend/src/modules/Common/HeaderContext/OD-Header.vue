<script setup lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { FieldContent, Field } from "@/components/ui/field";
import { LogInIcon, SearchIcon } from "lucide-vue-next";
import InputWithIcon from "../Components/InputWithIcon.vue";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import { useElementSizeObservabilityV2 } from "../Composables/useElementSizeObservabilityV2";
import { watch } from "vue";
import { useAuthenticationStatusStore } from "../Authentication/authentication.status.store";
import { Button } from "@/components/ui/button";
import { classConstructor } from "../ComponentsLogic/classConstructor";

// Хедер страницы — содержит логотип, поиск и профиль пользователя (если залогинен).
// Компонент использует локальные композиции и глобальные сторы для управления видом.

// Используем общий стор: управление сайдбаром, корректировка высоты хедера и пр.
const common = useCommonStore();

// Композиция для наблюдения за размерами DOM-элемента хедера (например, высота).
// Экспортирует `element` и реактивное `height`.
const header = useElementSizeObservabilityV2();

// Статус аутентификации — используется для условного рендера профиля.
const auth = useAuthenticationStatusStore();

// Входные пропсы компонента.
const props = defineProps<{
  sideBarPanelWidth?: number;
}>();

// Константа для переопределения стилей кнопки "Войти" в хедере, чтобы она соответствовала дизайну header.
const headerButtonBgOverride: string = '!bg-[#79c653b3] [background-image:linear-gradient(180deg,transparent,rgba(0,0,0,0.1))] hover:!bg-[#59923db3] !shadow-none !text-[var(--fg-reversed)]';

// Синхронизируем высоту хедера со стором `common` — для корректного layout-а приложения.
// Опция `immediate: true` вызывает обработчик при инициализации, чтобы сразу установить высоту.
watch(
  () => header.height.value,
  ($height) => {
    common.adjustHeaderHeight($height);
  },
  {
    immediate: true,
  },
);
</script>

<template>
  <header :ref="header.element"
    :class="'flex w-full card-sixth-accent justify-between gap-2 items-center bg-(--header-background-color)'">
    <img src="/main_logo.svg" :class="'p-1 h-8 brightness-0 sm:h-9 md:h-11 lg:h-13 xl:h-15 2xl:h-17'"
      v-on:click="common.toggleSideBar" />
    <div :class="'p-1 w-1/2'">
      <InputWithIcon :place-holder="'Поиск...'" :icon="SearchIcon" />
    </div>

    <!-- Показываем сведения о пользователе (аватар, имя, роль) только если он залогинен -->
    <!-- иначе - показываем кнопку "Войти" -->
    <!-- template - заглушка, которая не рендерится в DOM, но позволяет использовать логику -->

    <template v-if="auth.isLoggedIn && auth.login">
      <Avatar
        :class="'shadow-(--shadow-basic) h-8 w-8 sm:h-9 sm:w-9 md:h-10 md:w-10 lg:h-11 lg:w-11 xl:h-13 xl:w-13 2xl:h-15 2xl:w-15'">
        <AvatarFallback :class="'text-responsive-tertiary'">{{ auth.login[0].toUpperCase() }}</AvatarFallback>
      </Avatar>
      <FieldContent :class="'gap-0'">
        <Field :class="'text-responsive-tertiary text-nowrap'">{{ auth.login }}</Field>
        <Field :class="'text-responsive-tertiary text-nowrap'">{{ 'роль: ' + auth.login }}</Field>
      </FieldContent>
    </template>

    <template v-else>
      <RouterLink :to="'/auth'" :class="'p-0 h-full'">
        <Button :variant="'sixth'" :class="classConstructor('p-0 h-full w-full rounded-none', headerButtonBgOverride)">
          <LogInIcon />
          <span :class="'text-responsive-tertiary'">Войти</span>
        </Button>
      </RouterLink>
    </template>

  </header>
</template>