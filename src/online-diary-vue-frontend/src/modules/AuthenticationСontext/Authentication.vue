<script setup lang="ts">
import { Card } from "@/components/ui/card";
import AuthBody from "./components/AuthBody.vue";
import AuthFooter from "./components/AuthFooter.vue";
import AuthHeader from "./components/AuthHeader.vue";
import AuthAlert from "./components/AuthAlert.vue";
import { ref } from "vue";

export type AuthInputs = {
  login: string;
  password: string;
};

const authInputs = ref<AuthInputs>({
  login: "",
  password: "",
});

const isError = ref(false);

function ErrorInputs(): void {
  if (authInputs.value.login === "" || authInputs.value.password === "") {
    isError.value = true;
  } else {
    isError.value = false;
  }
}
</script>

<template>
  <div
    :class="'flex flex-1 flex-col min-h-0 min-w-0 justify-center items-center h-full w-full'"
  >
    <AuthAlert :is-error="isError"></AuthAlert>
    <Card :class="'gap-4 shadow-(--shadow-basic)'">
      <AuthHeader></AuthHeader>
      <AuthBody :authInputs="authInputs" :onAuth="ErrorInputs"></AuthBody>
      <AuthFooter></AuthFooter>
    </Card>
  </div>
</template>
