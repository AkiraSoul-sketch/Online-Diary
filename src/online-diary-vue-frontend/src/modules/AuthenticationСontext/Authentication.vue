<script setup lang="ts">
import { Card } from "@/components/ui/card";
import AuthBody from "./components/AuthBody.vue";
import AuthFooter from "./components/AuthFooter.vue";
import AuthHeader from "./components/AuthHeader.vue";
</script>

<template>
  <div
    :class="'flex flex-1 min-h-0 min-w-0 justify-center items-center h-full w-full'"
  >
    <Card :class="'gap-4 shadow-(--shadow-basic)'">
      <AuthHeader></AuthHeader>
      <AuthBody></AuthBody>
      <AuthFooter></AuthFooter>
    </Card>
  </div>
</template>
