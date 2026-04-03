<script setup lang="ts">
import { Button } from "@/components/ui/button";
import { CardContent } from "@/components/ui/card";
import Field from "@/components/ui/field/Field.vue";
import FieldError from "@/components/ui/field/FieldError.vue";
import { Input } from "@/components/ui/input";
import type { AuthInputs } from "@/modules/AuthenticationСontext/Authentication.vue";
import { ref } from "vue";

const props = defineProps<{
  authInputs: AuthInputs;
  onAuth: () => void;
}>();

const ErrorLogin = ref(false);
const ErrorPassword = ref(false);

function CheckForPassword(): boolean {
  if (props.authInputs.password === "") {
    ErrorPassword.value = true;
  } else {
    ErrorPassword.value = false;
  }
  return ErrorPassword.value;
}
function CheckForLogin(): boolean {
  if (props.authInputs.login === "") {
    ErrorLogin.value = true;
  } else {
    ErrorLogin.value = false;
  }
  return ErrorLogin.value;
}
function Auth(): void {
  const IsLoginInvalid = CheckForLogin();
  const IsPasswordInvalid = CheckForPassword();
  if (IsLoginInvalid || IsPasswordInvalid == true) {
    return;
  }
  props.onAuth();
}
</script>

<template>
  <CardContent :class="'flex flex-col justify-center items-center gap-2'">
    <Field>
      <FieldError
        v-if="ErrorLogin === true"
        :errors="[{ message: 'Пустой логин!' }]"
      />
      <Input
        placeholder="Логин"
        v-model="props.authInputs.login"
        :aria-invalid="ErrorLogin"
      >
      </Input>
    </Field>
    <Field>
      <FieldError
        v-if="ErrorPassword === true"
        :errors="[{ message: 'Пустой пароль!' }]"
      />
      <Input
        type="password"
        placeholder="Пароль"
        v-model="props.authInputs.password"
        :aria-invalid="ErrorPassword"
      >
      </Input>
    </Field>

    <Button variant="outline" @click="Auth">
      <div>Войти</div>
    </Button>
  </CardContent>
</template>
