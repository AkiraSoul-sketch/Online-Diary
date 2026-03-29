<script setup lang="ts">
import Badge from "@/components/ui/badge/Badge.vue";
import { CardContent, CardHeader } from "@/components/ui/card";
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Label from "@/components/ui/label/Label.vue";
import Separator from "@/components/ui/separator/Separator.vue";
import { UserIcon } from "lucide-vue-next";
</script>

<template>
  <Card
    :class="'my-0.5 min-w-0 min-h-0 grid grid-cols-[auto_auto_1fr] p-0 w-full border-zinc-200 bg-zinc-100  rounded-sm'"
  >
    <div :class="'flex flex-col gap-0.5 justify-between'">
      <CardHeader :class="'p-1 text-start block w-full'">
        <!-- статус действия -->
        <div :class="'flex items-center'">
          <span :class="'text-responsive-secondary'">Статус:</span>
          <Badge
            :class="'text-responsive-secondary text-white mx-1 bg-lime-600'"
            >успешно</Badge
          >
        </div>
      </CardHeader>

      <!-- актор действия -->
      <CardTitle :class="'flex flex-row p-1 font-normal text-start w-full'">
        <UserIcon> </UserIcon>
        <span :class="'text-responsive-tertiary underline mx-1'"
          >Проничкин Д.С.</span
        >
      </CardTitle>
      <Label :class="'p-1 text-responsive-tertiary self-end'"
        >2 часа назад</Label
      >
    </div>
    <Separator :orientation="'vertical'" :class="'bg-zinc-200'" />

    <!-- текст действия -->
    <CardContent :class="'flex items-center'">
      <Label :class="'text-responsive-tertiary text-start gap-0'"
        >действие действие действие действие действие действие действие действие
        действие</Label
      >
    </CardContent>
  </Card>
</template>
