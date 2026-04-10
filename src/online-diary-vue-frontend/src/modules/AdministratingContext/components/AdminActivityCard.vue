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
    :class="'border-none item-bg-primary-accent my-1 min-w-0 min-h-0 grid grid-cols-[auto_auto_1fr] w-full rounded-sm'">
    <div :class="'flex flex-col gap-1 justify-between'">
      <CardHeader :class="'p-1 text-center inline-flex items-center  w-full'">

        <!-- статус действия -->
        <div :class="'flex flex-row justify-between mx-auto items-center gap-2'">
          <span :class="'text-responsive-tertiary'">Статус:</span>
          <Badge :class="'bg-lime-600 text-center p-1'">
            <span :class="'text-responsive-fourth self-center text-center'">успешно</span>
          </Badge>
        </div>
      </CardHeader>

      <!-- актор действия -->
      <CardTitle :class="'flex flex-row p-1 font-normal text-start w-full'">
        <UserIcon />
        <span :class="'text-responsive-tertiary underline mx-1'">Проничкин Д.С.</span>
      </CardTitle>
      <Label :class="'p-1 text-responsive-tertiary self-end'">2 часа назад</Label>
    </div>
    <Separator :orientation="'vertical'" :class="'item-bg-primary'" />

    <!-- текст действия -->
    <CardContent :class="'flex items-center'">
      <Label :class="'text-responsive-tertiary text-start gap-0'">действие действие действие действие действие действие
        действие действие
        действие</Label>
    </CardContent>
  </Card>
</template>
