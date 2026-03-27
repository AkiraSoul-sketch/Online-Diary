<script lang="ts">
import Label from "@/components/ui/label/Label.vue";
import SidebarMenuItem from "@/components/ui/sidebar/SidebarMenuItem.vue";

export default {
  props: {
    sectionName: {
      type: String,
      required: true,
    },
  },
  components: {
    Label,
    SidebarMenuItem,
  },
};
</script>
<template>
  <SidebarMenuItem :class="'flex flex-col gap-0'">
    <Label :class="'p-1 m-0 text-responsive mx-2'">{{ sectionName }}</Label>
    <section :class="'flex flex-col gap-0.5'">
      <slot></slot>
    </section>
  </SidebarMenuItem>
</template>
