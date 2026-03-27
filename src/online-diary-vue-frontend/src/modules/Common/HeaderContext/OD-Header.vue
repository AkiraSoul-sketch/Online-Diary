<script lang="ts">
import { defineComponent } from "vue";
import ODHeaderSearch from "./components/OD-HeaderSearch.vue";
import ODSidebarToggle from "./components/OD-SidebarToggle.vue";
import ODSidebarMenuButtons from "./components/OD-SidebarMenuButtons.vue";

export default defineComponent({
  props: {
    sideBarPanelWidth: {
      type: Number,
      required: true,
    },
  },
  watch: {
    sideBarPanelWidth(newWidth: number): void {
      console.log("Header: sidebar width updated:", newWidth);
    },
  },
  components: {
    ODHeaderSearch,
    ODSidebarToggle,
    ODSidebarMenuButtons,
  },
});
</script>

<template>
  <header
    :class="'flex flex-row items-center justify-between w-full bg-(--header-background-color)'"
  >
    <ODSidebarToggle :sideBarPanelWidth="sideBarPanelWidth" />
    <section :class="'flex-1 px-1 max-w-80'">
      <ODHeaderSearch />
    </section>
    <ODSidebarMenuButtons />
  </header>
</template>
