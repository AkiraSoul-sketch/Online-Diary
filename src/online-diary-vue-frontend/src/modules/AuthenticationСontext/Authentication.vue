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

const authenticationData = ref<AuthInputs>({
  login: "",
  password: "",
});

const isError = ref(false);

function validateAuthentication(): void {
  if (authenticationData.value.login === "" || authenticationData.value.password === "") {
    isError.value = true;
  } else {
    isError.value = false;
  }
}
</script>

<template>
  <div :class="'flex-column-layout flex-constrained full-size justify-center items-center'">
    <AuthAlert :is-error="isError" />
    <Card :class="'gap-4 card-primary'">
      <AuthHeader />
      <AuthBody :authenticationData="authenticationData" :onAuth="validateAuthentication" />
      <AuthFooter />
    </Card>
  </div>
</template>
