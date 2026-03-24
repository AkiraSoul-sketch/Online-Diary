<script setup lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Label from "@/components/ui/label/Label.vue";
import Avatar from "@/components/ui/avatar/Avatar.vue";
import AvatarFallback from "@/components/ui/avatar/AvatarFallback.vue";
import Button from "@/components/ui/button/Button.vue";
</script>
<template>
  <aside class="flex">
    <Card>
      <Card>
        <CardTitle :text="'Online Diary'"> </CardTitle>
      </Card>
      <section>
        <Card>
          <Avatar :class="'self-center w-20 h-20'">
            <AvatarFallback>И</AvatarFallback>
          </Avatar>
          <Label :class="'flex self-center'">ИВАНОВ И. И.</Label>
          <Label :class="'flex self-center'">преподаватель</Label>
        </Card>
      </section>
      <section class="flex grow">
        <Card :class="'p-2 w-full'">
          <CardTitle :class="'flex self-center'" :text="'Меню'">
            <Label :class="'text-2xl'">Меню</Label>
          </CardTitle>
          <Button>Открыть профиль</Button>
          <Button>Настройки профиля</Button>
          <Button variant="destructive">Выйти</Button>
        </Card>
      </section>
    </Card>
    <div :class="'w-full'">
      <slot />
    </div>
  </aside>
</template>
