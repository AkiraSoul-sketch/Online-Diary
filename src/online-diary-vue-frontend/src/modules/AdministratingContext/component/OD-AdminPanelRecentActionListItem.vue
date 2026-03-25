<script setup lang="ts">
import {
  Item,
  ItemGroup,
  ItemContent,
  ItemDescription,
} from "@/components/ui/item";
</script>
<template>
  <!--notifications-->
  <section class="grid grid-cols-1 gap-2 p-1">
    <div className="flex w-full flex-col gap-0.5">
      <ItemGroup>
        <Item variant="outline">
          <ItemContent>
            <ItemDescription :class="'flex flex-row gap-1 justify-between'">
              <span>Преподаватель П. П.</span>
              <span> проставил Студенту С. С. База Данных 3</span>
              <span>1 час назад</span>
            </ItemDescription>
          </ItemContent>
        </Item>
      </ItemGroup>
    </div>
  </section>
</template>
