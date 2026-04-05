<script setup lang="ts">
import { onMounted, } from "vue";
import { useTeacherLogic } from "./teacher.logic";
import { useTeacherStore } from "./teacher.store";
import type { TeachingGroup } from "./teacher.models";
import { ItemTitle } from "@/components/ui/item";
import { Card } from "@/components/ui/card";
import Separator from "@/components/ui/separator/Separator.vue";
import ItemContent from "@/components/ui/item/ItemContent.vue";
import { Button } from "@/components/ui/button";


const { generateRandomGroups } = useTeacherLogic();
const store = useTeacherStore();

onMounted(() => {
  const groups: TeachingGroup[] = generateRandomGroups(3, 10);
  store.groups = groups;
})

</script>

<template>
  <section :class="'flex-column-layout h-full overflow-auto'">
    <div :class="'p-4 flex-column-layout'">
      <template v-for="group of store.groups" :key="group.id">
        <ItemTitle :class="'p-4 text-responsive-primary'">
          Группа: {{ group.name }}
        </ItemTitle>
        <Separator :orientation="'horizontal'" />
        <div :class="'p-4 flex gap-4 flex-row flex-wrap'">
          <Card v-for="discipline of group.disciplines" :class="'p-6 item-bg-primary'">
            <ItemTitle :class="'text-responsive-secondary text-wrap h-20  w-45 sm:w-50 md:w-70 lg:w-80'">
              {{ discipline.name }}
            </ItemTitle>
            <ItemContent :class="'m-auto'">
              <Button :variant="'quaternary'" :class="'text-responsive-secondary'">Открыть журнал</Button>
            </ItemContent>
          </Card>
        </div>
      </template>
    </div>
  </section>
</template>
