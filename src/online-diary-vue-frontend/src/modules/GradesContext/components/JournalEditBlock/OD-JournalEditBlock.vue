<script lang="ts">
import Card from "@/components/ui/card/Card.vue";
import CardContent from "@/components/ui/card/CardContent.vue";
import CardDescription from "@/components/ui/card/CardDescription.vue";
import CardTitle from "@/components/ui/card/CardTitle.vue";
import Label from "@/components/ui/label/Label.vue";
import { BookOpenTextIcon } from "lucide-vue-next";
import ODStudentGroupsSelect from "./OD-StudentGroupsSelect.vue";
import ODGroupDisciplinesSelect from "./OD-GroupDisciplinesSelect.vue";

export default {
  components: {
    Card,
    CardContent,
    CardDescription,
    CardTitle,
    Label,
    BookOpenTextIcon,
    ODStudentGroupsSelect,
    ODGroupDisciplinesSelect,
  },
};
</script>

<template>
  <Card :class="'gap-0 p-2 col-span-full'">
    <CardTitle>
      <Label :class="'text-2xl'">
        <BookOpenTextIcon> </BookOpenTextIcon>
        Выбрать журнал
      </Label>
    </CardTitle>
    <CardDescription :class="'my-2'">
      Вы можете изменить группу и дисциплину, выбрав другую из списка.
    </CardDescription>
    <CardContent :class="'p-0'">
      <!-- group/disciplines selects -->
      <section class="grid grid-cols-2 gap-2 w-full h-full">
        <!-- group -->
        <ODStudentGroupsSelect />
        <!-- discipline -->
        <ODGroupDisciplinesSelect />
      </section>
    </CardContent>
  </Card>
</template>
