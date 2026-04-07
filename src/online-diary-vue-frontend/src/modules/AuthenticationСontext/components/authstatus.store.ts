import { ref, type Ref } from "vue";
import type { AuthStatus } from "./authstatus.moduls";
import { defineStore } from "pinia";

const UserAuthStatus = defineStore("authStatus", () => {
  const authStatus: Ref<AuthStatus | null> = ref(null);

  function AuthLogin(inputAuthStatus: AuthStatus): void {
    authStatus.value = inputAuthStatus;
    PutInCookie(inputAuthStatus);
  }

  return { authStatus, AuthLogin, AuthLogout };
});

export { UserAuthStatus };

function AuthLogout(authStatus: Ref<AuthStatus | null>): void {
  authStatus.value = null;
}

function PutInCookie(authStatus: AuthStatus): void {
  document.cookie = `username=${authStatus.login}; path=/`;
  console.log("PutInCookie", authStatus);
}
