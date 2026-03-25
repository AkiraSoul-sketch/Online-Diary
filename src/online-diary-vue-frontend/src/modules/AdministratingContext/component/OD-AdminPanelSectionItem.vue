<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import Badge from "@/components/ui/badge/Badge.vue";
import Button from "@/components/ui/button/Button.vue";
</script>

<template>
  <Card :class="'p-1'">
    <CardTitle :class="'p-1'" :text="'Управление студентами'"> </CardTitle>
    <CardContent>
      <div :class="'flex flex-row gap-2 p-2 justify-between'">
        <div class="flex flex-col gap-2">
          <Badge variant="secondary">Создание</Badge>
          <Badge variant="secondary">Редактирование</Badge>
          <Badge variant="destructive">Архивация</Badge>
        </div>
        <div :class="'flex flex-col gap-2 justify-between'">
          <div></div>
          <Button :class="'inline-block self-end'" variant="outline"
            >Открыть</Button
          >
        </div>
      </div>
    </CardContent>
  </Card>
</template>
