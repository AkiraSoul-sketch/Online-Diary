<script setup lang="js">
import { CardTitle } from "@/components/ui/card";
import { Separator } from "@/components/ui/separator";
</script>

<template>
  <img src="/main_logo.svg" :class="'h-12'" />
  <CardTitle :class="'px-4 self-center text-[20px] sm:text-[22px]'"
    >Электронный журнал</CardTitle
  >
  <div :class="'self-center text-responsive-primary'">Авторизация</div>
  <Separator />
</template>
