<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { CardContent } from "@/components/ui/card";
import Field from "@/components/ui/field/Field.vue";
import FieldError from "@/components/ui/field/FieldError.vue";
import { Input } from "@/components/ui/input";
import type { AuthInputs } from "@/modules/AuthenticationСontext/Authentication.vue";
import { ref } from "vue";

const props = defineProps<{
  authenticationData: AuthInputs;
}>();

const emits = defineEmits<{
  (e: 'onAuthenticationConfirm', data: AuthInputs): void;
}>();

const isLoginFailure = ref(false);
const isPasswordFailure = ref(false);

function validatePassword(): boolean {
  if (props.authenticationData.password === "") {
    isPasswordFailure.value = true;
  } else {
    isPasswordFailure.value = false;
  }
  return isPasswordFailure.value;
}

function validateLogin(): boolean {
  if (props.authenticationData.login === "") {
    isLoginFailure.value = true;
  } else {
    isLoginFailure.value = false;
  }
  return isLoginFailure.value;
}

function authenticate(): void {
  const isLoginInvalid = validateLogin();
  const isPasswordInvalid = validatePassword();
  if (isLoginInvalid || isPasswordInvalid == true) return;
  emits('onAuthenticationConfirm', props.authenticationData);
}

</script>

<template>
  <CardContent :class="'flex-column-layout justify-center items-center gap-2'">

    <Field>
      <FieldError v-if="isLoginFailure === true" :errors="[{ message: 'Необходимо ввести логин' }]" />
      <Input :class="'text-responsive-secondary item-bg-primary-accent-2'" placeholder="Логин"
        v-model="props.authenticationData.login" :aria-invalid="isLoginFailure">
      </Input>
    </Field>

    <Field>
      <FieldError v-if="isPasswordFailure === true" :errors="[{ message: 'Необходимо ввести пароль' }]" />
      <Input type="password" :class="'text-responsive-secondary item-bg-primary-accent-2'" placeholder="Пароль"
        v-model="props.authenticationData.password" :aria-invalid="isPasswordFailure">
      </Input>
    </Field>

    <Button :variant="'sixth'" @click="authenticate">
      <span :class="'text-responsive-tertiary'">Войти</span>
    </Button>

  </CardContent>
</template>
