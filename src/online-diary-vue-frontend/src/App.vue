<script lang="ts">
import GradesPage from "./modules/GradesContext/GradesPage.vue";

export default {
  components: {
    ODSideBar,
    AdminPanel,
    GradesPage,
  },
  setup() {
    // useColorMode();
  },
};
import AdminPanel from "./modules/AdministratingContext/AdminPanel.vue";
import ODSideBar from "./modules/SidebarContext/OD-SideBar.vue";
</script>

<template>
  <section :class="'flex flex-col h-screen'">
    <header :class="'block w-full bg-sky-300 p-2'">
      <label>Header zone</label>
    </header>
    <section :class="'flex flex-1'">
      <aside :class="'basis-2/10'">
        <ODSideBar></ODSideBar>
      </aside>
      <main :class="'bg-violet-400 flex w-full min-h-0 overflow-auto'">
        <label>content</label>
      </main>
    </section>
    <header :class="'block w-full bg-sky-300 p-2'">
      <label>Footer Zone</label>
    </header>
  </section>
</template>
