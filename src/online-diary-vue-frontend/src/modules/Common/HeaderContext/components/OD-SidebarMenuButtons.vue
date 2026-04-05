<script setup lang="ts">
import { LogInIcon, UserPlus2Icon } from "lucide-vue-next";
import { Button } from "@/components/ui/button";
import { Label } from "@/components/ui/label";
</script>

<template>
  <section>
    <Button id="register-button" :class="'button-flat-default text-dark h-auto'" :variant="'header'">
      <UserPlus2Icon :class="'text-dark'" />
      <Label :for="'register-button'" :class="'text-responsive text-dark'">Регистрация</Label>
    </Button>
    <Button id="auth-button" :class="'button-flat-default text-dark h-auto'" :variant="'header'">
      <LogInIcon :class="'text-dark'" />
      <Label :for="'auth-button'" :class="'text-responsive text-dark'">Авторизация</Label>
    </Button>
  </section>
</template>
