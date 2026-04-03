<script setup lang="ts">
import Alert from "@/components/ui/alert/Alert.vue";
import { AlertCircleIcon } from "lucide-vue-next";
import AlertTitle from "@/components/ui/alert/AlertTitle.vue";
import AlertDescription from "@/components/ui/alert/AlertDescription.vue";

const props = defineProps<{
  isError: boolean;
}>();
</script>

<template>
  <Alert
    v-if="isError"
    variant="destructive"
    :class="'mb-4 w-full max-w-md absolute top-20 sm:top-20 md:top-20 lg:top-20 xl:top-22 2xl:top-25 shadow-(--shadow-basic) '"
  >
    <AlertCircleIcon />
    <AlertTitle>Произошла ошибка!</AlertTitle>
    <AlertDescription>
      <p>Возможно, Вы сейчас не сделали определенные действия, а именно:</p>
      <ul class="mt-2 list-inside list-disc space-y-1">
        <li>Проверьте данные логина и пароля.</li>
        <li>Вы не ввели пароль/логин.</li>
      </ul>
    </AlertDescription>
  </Alert>
</template>
