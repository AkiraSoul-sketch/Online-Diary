<script setup lang="ts">
import { Avatar, AvatarFallback } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { LogInIcon, MenuIcon } from "lucide-vue-next";
import { computed, watch } from "vue";
import { useRoute } from "vue-router";
import { useCommonStore } from "@/modules/Common/Stores/common.store";
import { useElementSizeObservabilityV2 } from "../Composables/useElementSizeObservabilityV2";
import { useAuthenticationStatusStore } from "../Authentication/authentication.status.store";
import ODHeaderSearch from "./components/OD-HeaderSearch.vue";

const common = useCommonStore();
const header = useElementSizeObservabilityV2();
const auth = useAuthenticationStatusStore();
const route = useRoute();

const userInitial = computed(() => auth.login?.charAt(0).toUpperCase() ?? "?");
const sidebarToggleLabel = computed(() =>
  common.sideBarHidden ? "Закрыть меню" : "Открыть меню",
);
const currentSection = computed(() => resolveSectionTitle(route.path));

watch(
  () => header.height.value,
  ($height) => {
    common.adjustHeaderHeight($height);
  },
  {
    immediate: true,
  },
);

function resolveSectionTitle(path: string): string {
  if (path === "/") return "Главная";
  if (matchesNestedRoute(path, "/admin")) return "Панель администратора";
  if (matchesNestedRoute(path, "/grades")) return "Журнал оценок";
  if (matchesNestedRoute(path, "/disciplines")) return "Дисциплины";
  if (matchesNestedRoute(path, "/teacher")) return "Кабинет преподавателя";
  if (matchesNestedRoute(path, "/auth")) return "Авторизация";
  return "Рабочее пространство";
}

function matchesNestedRoute(path: string, routePrefix: string): boolean {
  return path === routePrefix || path.startsWith(`${routePrefix}/`);
}
</script>

<template>
  <header :ref="header.element" :class="'od-header'">
    <div class="od-header__brand">
      <Button type="button" variant="outline" size="icon" class="od-header__menu-button"
        :aria-label="sidebarToggleLabel" @click="common.toggleSideBar">
        <MenuIcon />
      </Button>

      <RouterLink to="/" class="od-header__brand-link" aria-label="Перейти на главную страницу">
        <span class="od-header__logo-shell">
          <img src="/main_logo.svg" alt="Логотип Online Diary" class="od-header__logo" />
        </span>

        <span class="od-header__brand-copy ">
          <span :class="'od-header__brand-kicker'">{{ currentSection }}</span>
        </span>
      </RouterLink>
    </div>

    <div class="od-header__search">
      <ODHeaderSearch />
    </div>

    <div class="od-header__actions">
      <template v-if="auth.isLoggedIn && auth.login">
        <div class="od-header__user-card">
          <div class="od-header__user-copy">
            <span class="od-header__user-status">В системе</span>
            <span class="od-header__user-name">{{ auth.login }}</span>
          </div>

          <Avatar class="od-header__avatar">
            <AvatarFallback class="od-header__avatar-fallback text-responsive-secondary">
              {{ userInitial }}
            </AvatarFallback>
          </Avatar>
        </div>
      </template>

      <template v-else>
        <RouterLink to="/auth" class="od-header__auth-link">
          <Button variant="sixth" class="od-header__login-button">
            <LogInIcon />
            <span>Войти</span>
          </Button>
        </RouterLink>
      </template>
    </div>
  </header>
</template>

<style scoped lang="css">
.od-header {
  position: relative;
  display: grid;
  grid-template-areas: "brand search actions";
  grid-template-columns: auto minmax(14rem, 1fr) auto;
  align-items: center;
  gap: clamp(0.75rem, 1.8vw, 1.4rem);
  padding: clamp(0.75rem, 1.4vw, 1rem) clamp(0.85rem, 1.8vw, 1.35rem);
  border-bottom: 1px solid hsl(104 44% 55% / 0.24);
  background: var(--bg-sixth);
  color: var(--fg-primary);
  box-shadow: 0 10px 30px hsl(240 25% 8% / 0.08);
  backdrop-filter: blur(18px);
  isolation: isolate;
}

.od-header__brand {
  grid-area: brand;
  display: flex;
  min-width: 0;
  align-items: center;
  gap: 0.85rem;
}

.od-header__menu-button {
  flex-shrink: 0;
  height: 2.85rem;
  width: 2.85rem;
  border-radius: 1rem;
  border-color: transparent;
  background: var(--bg-sixth) !important;
  color: var(--fg-primary);
  box-shadow: none;
}

.od-header__menu-button:hover {
  background: var(--bg-sixth-hover) !important;
}

.od-header__brand-link {
  display: flex;
  min-width: 0;
  align-items: center;
  gap: 0.85rem;
  color: inherit;
  text-decoration: none;
}

.od-header__logo-shell {
  display: flex;
  height: clamp(2.85rem, 4vw, 3.4rem);
  width: clamp(2.85rem, 4vw, 3.4rem);
  flex-shrink: 0;
  align-items: center;
  justify-content: center;
  border: 1px solid hsl(104 44% 55% / 0.2);
  border-radius: 1rem;
  background: var(--bg-sixth-gradient);
  box-shadow: inset 0 1px 0 hsl(0 0% 100% / 0.22);
}

.od-header__logo {
  height: 70%;
  width: 70%;
  object-fit: contain;
  filter: brightness(0) saturate(100%);
}

.od-header__brand-copy {
  display: flex;
  min-width: 0;
  flex-direction: column;
}

.od-header__brand-kicker {
  font-size: 0.72rem;
  font-weight: 700;
  line-height: 1;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: var(--fg-primary) !important;
}

.od-header__brand-title {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: clamp(1rem, 1vw + 0.7rem, 1.35rem);
  font-weight: 800;
  line-height: 1.1;
}

.od-header__brand-note {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.85rem;
  color: var(--fg-reversed);
}

.od-header__search {
  grid-area: search;
  min-width: 0;
}

.od-header__actions {
  grid-area: actions;
  display: flex;
  min-width: 0;
  justify-self: end;
}

.od-header__user-card {
  display: flex;
  min-width: 0;
  align-items: center;
  gap: 0.75rem;
  border: 1px solid hsl(0 0% 100% / 0.12);
  border-radius: 1.1rem;
  background: transparent;
  padding: 0.35rem 0.4rem 0.35rem 0.85rem;
  box-shadow: none;
}

.od-header__user-copy {
  display: flex;
  min-width: 0;
  flex-direction: column;
}

.od-header__user-status {
  display: inline-flex;
  align-items: center;
  gap: 0.4rem;
  font-size: 0.7rem;
  font-weight: 700;
  line-height: 1;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  color: var(--fg-reversed);
}

.od-header__user-status::before {
  content: "";
  display: inline-block;
  height: 0.45rem;
  width: 0.45rem;
  border-radius: 999px;
  background: var(--bg-sixth);
  box-shadow: 0 0 0 0.25rem hsl(104 44% 55% / 0.14);
}

.od-header__user-name {
  max-width: 16ch;
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.96rem;
  font-weight: 700;
}

.od-header__avatar {
  height: 2.65rem;
  width: 2.65rem;
  border: 2px solid hsl(0 0% 100% / 0.84);
  background: var(--bg-sixth-hover);
}

.od-header__avatar-fallback {
  background: transparent;
  color: var(--fg-reversed);
  font-weight: 800;
}

.od-header__auth-link {
  display: flex;
}

.od-header__login-button {
  height: 2.85rem;
  border-radius: 1rem;
  padding-inline: 1rem;
  background: var(--bg-sixth) !important;
  box-shadow: 0 10px 20px hsl(104 44% 35% / 0.18);
}

.od-header__login-button:hover {
  background: var(--bg-sixth-hover) !important;
}

.dark .od-header {
  border-bottom-color: hsl(104 44% 45% / 0.32);
  background: var(--bg-sixth);
  color: var(--fg-reversed);
  box-shadow: 0 12px 28px hsl(220 30% 4% / 0.35);
}

.dark .od-header__menu-button {
  border-color: transparent;
  background: var(--bg-sixth-hover);
  color: var(--fg-reversed);
  box-shadow: none;
}

.dark .od-header__menu-button:hover {
  background: var(--bg-sixth);
}

.dark .od-header__logo-shell {
  border-color: hsl(0 0% 100% / 0.08);
  background: var(--bg-sixth-gradient);
  box-shadow: inset 0 1px 0 hsl(0 0% 100% / 0.06);
}

.dark .od-header__logo {
  filter: brightness(0) invert(1);
}

.dark .od-header__brand-kicker,
.dark .od-header__brand-note {
  color: var(--fg-reversed);
}

.dark .od-header__user-card {
  border-color: hsl(0 0% 100% / 0.08);
  background: transparent;
  box-shadow: none;
}

.dark .od-header__user-status {
  color: var(--fg-reversed);
}

.dark .od-header__avatar {
  border-color: hsl(0 0% 100% / 0.84);
}

@media (max-width: 960px) {
  .od-header {
    grid-template-areas:
      "brand actions"
      "search search";
    grid-template-columns: minmax(0, 1fr) auto;
    align-items: start;
  }

  .od-header__actions {
    align-self: center;
  }
}

@media (max-width: 720px) {
  .od-header {
    gap: 0.75rem;
    padding-inline: 0.75rem;
  }

  .od-header__brand {
    gap: 0.65rem;
  }

  .od-header__brand-note,
  .od-header__user-status {
    display: none;
  }

  .od-header__menu-button,
  .od-header__logo-shell {
    border-radius: 0.9rem;
  }

  .od-header__user-card {
    gap: 0.55rem;
    padding-left: 0.65rem;
  }

  .od-header__user-name {
    max-width: 11ch;
    font-size: 0.9rem;
  }
}

@media (max-width: 480px) {
  .od-header__brand-link {
    gap: 0.65rem;
  }

  .od-header__brand-title {
    font-size: 0.98rem;
  }

  .od-header__brand-kicker {
    max-width: 16ch;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  .od-header__user-copy {
    display: none;
  }

  .od-header__user-card {
    padding: 0;
    border: none;
    background: transparent;
    box-shadow: none;
  }

  .od-header__login-button {
    min-width: 2.85rem;
    padding-inline: 0.8rem;
  }
}

@media (max-width: 380px) {

  .od-header__brand-note,
  .od-header__brand-kicker {
    display: none;
  }

  .od-header__login-button span {
    display: none;
  }

  .od-header__login-button {
    width: 2.85rem;
    padding-inline: 0;
  }
}
</style>
