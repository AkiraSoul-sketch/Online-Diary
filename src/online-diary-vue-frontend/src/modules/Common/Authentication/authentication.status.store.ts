import { defineStore } from "pinia";
import { authenticationLogic } from "./authentication.logic";
import type { AuthenticationInformation } from "./authentication.models";
import { computed, onMounted, ref, type Ref } from "vue";

/**
 * Создаёт и возвращает Pinia store для управления статусом аутентификации пользователя.
 *
 * Store инкапсулирует:
 * - текущее состояние авторизации (через внутренний `state`);
 * - вычисляемый признак входа пользователя в систему (`isLoggedIn`);
 * - операции входа (`authenticate`) и выхода (`logout`);
 * - инициализацию состояния из cookie при монтировании.
 *
 * Внутри используется модуль бизнес-логики аутентификации, который отвечает
 * за фактическую установку/сброс состояния и чтение данных авторизации.
 *
 * @returns Объект store с публичным API:
 * - `isLoggedIn` — вычисляемый флаг, показывающий, авторизован ли пользователь;
 * - `authenticate(input)` — выполняет аутентификацию и обновляет состояние;
 * - `logout()` — завершает сессию пользователя и очищает состояние.
 */
const useAuthenticationStatusStore = defineStore("authenticationStatus", () => {
  /**
   * Логика аутентификации.
   *
   * Получаем модуль с функциями, реализующими фактические операции
   * по установке/сбросу состояния аутентификации.
   */
  const logic = authenticationLogic();

  /**
   * Текущее состояние аутентификации приложения.
   *
   * - null — пользователь не аутентифицирован;
   * - AuthenticationInformation — объект с информацией об авторизованном пользователе.
   */
  const state: Ref<AuthenticationInformation | null> = ref(null);

  /**
   * Упрощённое поле `login`.
   *
   * Хранит только строковый логин текущего пользователя или `null`.
   * Используется для быстрого вычисления `isLoggedIn` (наличие логина),
   * отдельно от полного объекта `state`. Обновляется при `authenticate`
   * и очищается при `logout`. Инициализируется из cookie в `onMounted()`
   * через `logic.getAuthenticationCookie()`.
   */
  const login: Ref<string | null> = ref(null);

  /**
   * Признак того, что пользователь в настоящий момент залогинен.
   * Возвращает true, если state содержит объект пользователя, иначе false.
   */
  const isLoggedIn = computed(() => {
    return login.value !== null;
  });

  /**
   * При монтировании инициализируем `state` из cookie.
   *
   * Получаем сохранённую информацию об аутентификации (если есть)
   * через `logic.getAuthenticationCookie()` и присваиваем её `login.value`.
   * Это обеспечивает корректное значение `isLoggedIn` при загрузке.
   */
  onMounted(() => {
    login.value = logic.getAuthenticationCookie();
  });

  /**
   * Выполняет аутентификацию пользователя.
   *
   * Делегирует непосредственную работу модулю logic, обновляет state
   * и выводит в консоль информацию о входе.
   *
   * @param input - объект с данными аутентификации (login, токен и т.д.)
   */
  function authenticate(input: AuthenticationInformation): void {
    logic.authenticate(state, input);
    login.value = input.login;
  }

  /**
   * Выполняет выход пользователя (логаут).
   *
   * Делегирует операцию модулю logic, который сбрасывает state.
   */
  function logout(): void {
    logic.logout(state);
    login.value = null;
  }

  return { login, isLoggedIn, authenticate, logout };
});

export { useAuthenticationStatusStore };
