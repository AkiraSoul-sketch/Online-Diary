<script lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import CardDescription from "@/components/ui/card/CardDescription.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Label from "@/components/ui/label/Label.vue";
import { BookOpenTextIcon } from "lucide-vue-next";
import ODStudentGroupsSelect from "./OD-StudentGroupsSelect.vue";
import ODGroupDisciplinesSelect from "./OD-GroupDisciplinesSelect.vue";
import { CardAction } from "@/components/ui/card";

export default {
  components: {
    Card,
    CardContent,
    CardDescription,
    CardTitle,
    Label,
    CardAction,
    BookOpenTextIcon,
    ODStudentGroupsSelect,
    ODGroupDisciplinesSelect,
  },
};
</script>

<template>
  <Card :class="'card-primary gap-0 p-1 col-span-full'">
    <CardTitle :class="'p-1'">
      <div :class="'inline-flex gap-2 items-center text-responsive-primary'">
        <BookOpenTextIcon />
        Выбрать журнал
      </div>
    </CardTitle>
    <CardContent :class="'p-0'">
      <section class="grid grid-cols-2 gap-2 w-full h-full">
        <ODStudentGroupsSelect />
        <ODGroupDisciplinesSelect />
      </section>
    </CardContent>
  </Card>
</template>
