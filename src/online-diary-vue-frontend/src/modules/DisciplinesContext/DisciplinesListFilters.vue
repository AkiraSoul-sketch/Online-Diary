<script setup lang="ts">
import { SearchIcon } from "lucide-vue-next";
import InputWithIcon from "../Common/Components/InputWithIcon.vue";
import Button from "@/components/ui/button/Button.vue";
import Label from "@/components/ui/label/Label.vue";
import Field from "@/components/ui/field/Field.vue";
import Checkbox from "@/components/ui/checkbox/Checkbox.vue";
import FieldContent from "@/components/ui/field/FieldContent.vue";
import FieldLabel from "@/components/ui/field/FieldLabel.vue";
import Select from "@/components/ui/select/Select.vue";
import SelectTrigger from "@/components/ui/select/SelectTrigger.vue";
import SelectValue from "@/components/ui/select/SelectValue.vue";
import SelectContent from "@/components/ui/select/SelectContent.vue";
import SelectGroup from "@/components/ui/select/SelectGroup.vue";
import SelectItem from "@/components/ui/select/SelectItem.vue";
import { useDisciplinesStore } from "./discipline.store";
import SelectLabel from "@/components/ui/select/SelectLabel.vue";
import type { DisciplineTeacher } from "./discipline.models";
import { onMounted } from "vue";

// форма для фильтрации списка дисциплин

const store = useDisciplinesStore();
const uniqueTeachers: DisciplineTeacher[] = [];

onMounted(() => {
  store.disciplines
    .map((d) => d.teacher)
    .filter((t) => t !== null)
    .forEach((teacher) => {
      if (!uniqueTeachers.some((t) => t.name === teacher?.name)) {
        uniqueTeachers.push(teacher);
      }
    });
});
</script>

<template>
  <div class="card-primary rounded-md h-max border shrink-0">
    <div
      class="p-4 bg-apricat-cream text-responsive-secondary font-semibold h-25 rounded-t-md"
    >
      Фильтрация
    </div>
    <div class="p-4">
      <div class="text-responsive-tertiary mt-2 flex-column-layout gap-2">
        <InputWithIcon
          :class="'text-responsive-tertiary'"
          :place-holder="'название'"
          :icon="SearchIcon"
        />
        <InputWithIcon
          :class="'text-responsive-tertiary'"
          :place-holder="'группа'"
          :icon="SearchIcon"
        />
        <InputWithIcon
          :class="'text-responsive-tertiary'"
          :place-holder="'семестр'"
          :icon="SearchIcon"
        />

        <Select :class="'w-full'">
          <SelectTrigger
            :class="'item-bg-primary-accent-2 text-responsive-tertiary w-full'"
          >
            <SelectValue
              :class="'text-responsive-tertiary'"
              :placeholder="'преподаватель'"
            />
          </SelectTrigger>
          <SelectContent>
            <SelectGroup>
              <SelectLabel :class="'text-responsive-tertiary'"
                >преподаватели</SelectLabel
              >
              <SelectItem
                v-for="teacher of uniqueTeachers"
                :key="teacher.id"
                :value="teacher.name"
              >
                {{ teacher.name }}
              </SelectItem>
            </SelectGroup>
          </SelectContent>
        </Select>

        <Field :orientation="'horizontal'">
          <Checkbox :id="'archived-filter'" :default-value="false" />
          <FieldContent :class="'items-center'">
            <FieldLabel
              :class="'text-responsive-tertiary'"
              for="archived-filter"
            >
              Архивные дисциплины
            </FieldLabel>
          </FieldContent>
        </Field>

        <Button :variant="'secondary'" :class="'mx-auto'">
          <Label :class="'text-responsive-tertiary font-prussian-blue'"
            >Сбросить</Label
          >
        </Button>
      </div>
    </div>
  </div>
</template>
