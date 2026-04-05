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
    <SelectTrigger :class="'item-bg-primary-accent-2 w-full'">
      <SelectValue :class="'text-responsive-tertiary'" :placeholder="'Группы'" />
    </SelectTrigger>
    <SelectContent>
      <SelectGroup>
        <SelectItem :class="'text-responsive-tertiary'" value="ПСК-3-25">ПСК-3-25</SelectItem>
        <SelectItem :class="'text-responsive-tertiary'" value="ПСК-3-26">ПСК-3-26</SelectItem>
        <SelectItem :class="'text-responsive-tertiary'" value="ПСК-3-27">ПСК-3-27</SelectItem>
      </SelectGroup>
    </SelectContent>
  </Select>
</template>
