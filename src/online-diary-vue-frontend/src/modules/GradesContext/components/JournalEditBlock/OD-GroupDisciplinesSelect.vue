<script lang="ts">
import Select from "@/components/ui/select/Select.vue";
import SelectContent from "@/components/ui/select/SelectContent.vue";
import SelectGroup from "@/components/ui/select/SelectGroup.vue";
import SelectItem from "@/components/ui/select/SelectItem.vue";
import SelectTrigger from "@/components/ui/select/SelectTrigger.vue";
import SelectValue from "@/components/ui/select/SelectValue.vue";

export default {
  components: {
    Select,
    SelectContent,
    SelectGroup,
    SelectItem,
    SelectTrigger,
    SelectValue,
  },
};
</script>

<template>
  <Select>
    <SelectTrigger :class="'w-full'">
      <SelectValue :placeholder="'Дисциплины'"> </SelectValue>
    </SelectTrigger>
    <SelectContent>
      <SelectGroup>
        <SelectItem value="Дисциплина 1">Дисциплина 1</SelectItem>
        <SelectItem value="Дисциплина 2">Дисциплина 2</SelectItem>
        <SelectItem value="Дисциплина 3">Дисциплина 3</SelectItem>
      </SelectGroup>
    </SelectContent>
  </Select>
</template>
