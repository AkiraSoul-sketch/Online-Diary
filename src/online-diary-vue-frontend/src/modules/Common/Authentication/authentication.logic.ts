import type { Ref } from "vue";
import type { AuthenticationInformation } from "./authentication.models";
import { cookieLogic } from "../Cookies/cookies.logic";

/**
 * Аутентифицирует пользователя, обновляя переданное реактивное состояние аутентификации.
 *
 * Функция записывает переданный объект AuthenticationInformation в указанный
 * Vue Ref, чтобы компоненты и наблюдатели могли отреагировать на изменение.
 *
 * @param state - Vue Ref, содержащий текущую AuthenticationInformation или null.
 * @param input - AuthenticationInformation, сохраняемая как данные аутентифицированного пользователя.
 * @returns void
 */
function authenticate(
  state: Ref<AuthenticationInformation | null>,
  input: AuthenticationInformation,
): void {
  state.value = input;
  addAuthenticationCookie(input);
}

/**
 * Сериализует данные аутентификации в JSON и сохраняет их в cookie `user_info`.
 *
 * Cookie создаётся/обновляется со временем жизни 24 часа (86400 секунд).
 *
 * @param info Объект с информацией аутентификации, который будет сохранён в cookie.
 * @returns Ничего не возвращает.
 */
function addAuthenticationCookie(info: AuthenticationInformation): void {
  const json: string = JSON.stringify(info);
  const maxAge: number = 24 * 60 * 60;
  const cookies = cookieLogic();
  cookies.addOrUpdateCookie("user_info", json, maxAge);
}

/**
 * Получает информацию об аутентификации пользователя из cookie.
 *
 * @remarks
 * Функция обращается к хранилищу cookie и пытается получить данные
 * об аутентификации по ключу `user_info`.
 *
 * @returns {AuthenticationInformation | null} Возвращает объект с информацией
 * об аутентификации пользователя, если cookie существует и содержит валидные данные,
 * иначе возвращает `null`.
 */
function getAuthenticationCookie(): AuthenticationInformation | null {
  const key: string = "user_info";
  const cookies = cookieLogic();
  return cookies.getJsonCookie<AuthenticationInformation>(key);
}

/**
 * Выполняет выход пользователя, устанавливая переданное реактивное состояние аутентификации в null.
 *
 * Функция обновляет переданный Vue Ref, очищая информацию об аутентификации.
 * Это позволит компонентам и наблюдателям отреагировать на выход пользователя.
 *
 * @param state - Vue Ref, содержащий текущую AuthenticationInformation или null.
 * @returns void
 */
function logout(state: Ref<AuthenticationInformation | null>): void {
  state.value = null;
}

/**
 * Возвращает объект с функциями управления аутентификацией.
 *
 * Эта helper-функция группирует доступные операции аутентификации
 * (authenticate и logout) и может быть импортирована в компоненты
 * или composables для удобного доступа к этим методам.
 *
 * Примечание: сама функция не хранит состояние — она предоставляет
 * только ссылки на функции, работающие с Vue Ref, передаваемым
 * в них извне.
 *
 * @returns Объект с методами:
 *  - authenticate(state, input): сохраняет AuthenticationInformation в Ref.
 *  - logout(state): очищает Ref, устанавливая значение null.
 */
export const authenticationLogic = () => {
  return { authenticate, logout, getAuthenticationCookie };
};
