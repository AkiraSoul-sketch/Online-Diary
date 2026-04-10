<script setup lang="ts">
import {
  Empty,
  EmptyDescription,
  EmptyHeader,
  EmptyMedia,
  EmptyTitle,
} from "./components/ui/empty";
import { CodeIcon } from "lucide-vue-next";
import { useCommonStore } from "./modules/Common/Stores/common.store";
import { onMounted } from "vue";
import { toast, ToastContainer } from "vue-toastflow";

const commonStore = useCommonStore();

onMounted(() => {
  greetUserAfterAuthentication();
})

function greetUserAfterAuthentication(): void {
  const isAfterAuth: boolean = commonStore.isAfterLogin;
  if (isAfterAuth) {
    toast.success({
      title: 'Успешная аутентификация',
      description: 'Вы успешно вошли в систему. Добро пожаловать!',
    })
    setTimeout(() => {
      commonStore.$state.isAfterLogin = false;
    }, 5000)
  }
}

</script>

<template>
  <Empty :class="'flex items-center justify-center w-full h-full'">
    <EmptyHeader>
      <EmptyMedia>
        <CodeIcon />
      </EmptyMedia>
      <EmptyTitle>В разработке...</EmptyTitle>
      <EmptyDescription>
        Данная страница находится в разработке. Пожалуйста, зайдите позже.
      </EmptyDescription>
    </EmptyHeader>
  </Empty>
  <ToastContainer />
</template>
