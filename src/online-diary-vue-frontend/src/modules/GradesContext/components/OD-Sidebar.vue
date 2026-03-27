<script lang="ts">
import { Button } from "@/components/ui/button";
import {
  Sidebar,
  SidebarContent,
  SidebarHeader,
  useSidebar,
} from "@/components/ui/sidebar";
import ODSidebarTitle from "./OD-SidebarTitle.vue";
export default {
  components: {
    Sidebar,
    SidebarHeader,
    SidebarContent,
    ODSidebarTitle,
    Button,
  },
  setup() {
    useSidebar();
  },
};
</script>

<template>
  <Sidebar>
    <SidebarHeader>
      <ODSidebarTitle> </ODSidebarTitle>
    </SidebarHeader>
    <SidebarContent>
      <Button>Here goes sidebar content</Button>
    </SidebarContent>
  </Sidebar>
</template>
