<script setup lang="js">
import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
</script>

<template>
  <Separator />
  <Button :variant="'secondary'" :class="'self-center'">
    <div :class="'text-responsive-tertiary'">Сбросить пароль</div>
  </Button>
</template>
