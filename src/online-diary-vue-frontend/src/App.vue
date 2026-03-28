<script lang="ts">
import AdminPanel from "./modules/AdministratingContext/AdminPanel.vue";
import ODSideBar from "./modules/Common/SidebarContext/OD-SideBar.vue";
import { Components } from "./modules/Common/ComponentsLogic/Components";
import ODHeader from "./modules/Common/HeaderContext/OD-Header.vue";
import { defineComponent } from "vue";
import {
  ResizableHandle,
  ResizablePanel,
  ResizablePanelGroup,
} from "./components/ui/resizable";
import ODFooter from "./modules/Common/FooterContext/OD-Footer.vue";

export default defineComponent({
  components: {
    ODSideBar,
    AdminPanel,
    ODHeader,
    ResizablePanelGroup,
    ResizablePanel,
    ResizableHandle,
    ODFooter,
  },
  data() {
    return {
      sidebarObserver: null as ResizeObserver | null,
      sideBarWidth: 0 as number,
      maxSideBarWidth: 16.67 as number,
    };
  },
  mounted() {
    this.sidebarObserver = this.useSidebarResizeObserver();
  },
  unmounted() {
    Components.DisposeResizeObserver(this.sidebarObserver);
  },
  methods: {
    useSidebarResizeObserver(): ResizeObserver | null {
      const key: string = "sidebar";
      const element: HTMLElement | null = Components.HTMLElementByRef(
        this,
        key,
      );
      if (!element) return null;
      const observer: ResizeObserver = new ResizeObserver((entries) =>
        this.updateSidebarWidthByObserver(entries),
      );
      observer.observe(element);
      return observer;
    },
    updateSidebarWidthByObserver(entries: ResizeObserverEntry[]): void {
      const key: string = "sidebar";
      const element: HTMLElement | null = Components.HTMLElementByRef(
        this,
        key,
      );
      for (const entry of entries) {
        if (entry.target === element) {
          this.sideBarWidth = element.clientWidth;
        }
      }
    },
  },
});
</script>

<template>
  <section :class="'flex flex-col h-screen w-full'">
    <ODHeader :side-bar-panel-width="sideBarWidth" />
    <section :class="'flex flex-1 min-h-0'">
      <ResizablePanelGroup :direction="'horizontal'">
        <ResizablePanel :default-size="15" :class="'flex flex-1 min-h-0'">
          <aside :class="'sidebar-bg'" :ref="'sidebar'">
            <ODSideBar />
          </aside>
        </ResizablePanel>
        <ResizableHandle :class="'w-0 flex-none min-h-0 min-w-0'" />
        <ResizablePanel :class="'flex flex-1 min-h-0'">
          <main :class="'w-full'">
            <RouterView />
          </main>
        </ResizablePanel>
      </ResizablePanelGroup>
    </section>
    <footer>
      <ODFooter />
    </footer>
  </section>
</template>

<style lang="css" scoped>
.sidebar-bg {
  background: var(--sidebar-gradient), var(--sidebar-background);
  height: 100%;
}
</style>
