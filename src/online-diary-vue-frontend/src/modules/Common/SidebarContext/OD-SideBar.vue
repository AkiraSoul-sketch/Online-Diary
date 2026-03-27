<script lang="ts">
import {
  SidebarContent,
  SidebarGroup,
  SidebarHeader,
  SidebarMenu,
  SidebarProvider,
} from "@/components/ui/sidebar";
import ODSidebarMenuContent from "./components/OD-SidebarMenuContent.vue";
import ODSidebarAvatar from "./components/OD-SidebarAvatar.vue";
import ODSidebarHeaderContent from "./components/OD-SidebarHeaderContent.vue";

export default {
  components: {
    SidebarProvider,
    SidebarHeader,
    ODSidebarHeaderContent,
    SidebarContent,
    SidebarGroup,
    ODSidebarAvatar,
    SidebarMenu,
    ODSidebarMenuContent,
  },
};
</script>
<template>
  <SidebarProvider
    :class="'flex flex-col h-full min-h-0 justify-start items-center text-dark gap-5 md:gap-6 lg:gap-7 xl:gap-8'"
  >
    <!-- хедер сайдбара -->
    <SidebarHeader :class="'p-0 w-full'">
      <ODSidebarHeaderContent />
    </SidebarHeader>
    <!-- содержимое сайдбара -->
    <SidebarContent :class="'w-full gap-0'">
      <!-- аватарка сайдбара -->
      <SidebarGroup :class="'p-0'">
        <ODSidebarAvatar />
      </SidebarGroup>
      <!-- меню сайдбара -->
      <SidebarGroup :class="'flex p-0'">
        <SidebarMenu :class="'px-0 rounded-none flex'">
          <ODSidebarMenuContent />
        </SidebarMenu>
      </SidebarGroup>
    </SidebarContent>
  </SidebarProvider>
</template>
