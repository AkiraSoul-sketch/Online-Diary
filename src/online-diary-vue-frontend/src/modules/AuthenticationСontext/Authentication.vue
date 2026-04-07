<script setup lang="ts">
import { Card } from "@/components/ui/card";
import AuthBody from "./components/AuthBody.vue";
import AuthFooter from "./components/AuthFooter.vue";
import AuthHeader from "./components/AuthHeader.vue";
import AuthAlert from "./components/AuthAlert.vue";
import { ref } from "vue";
import { useAuthenticationStatusStore } from "../Common/Authentication/authentication.status.store";
import { useRouter } from "vue-router";
import { useCommonStore } from "../Common/Stores/common.store";

// страница аутентификации, 
// которая содержит в себе форму для ввода логина и пароля, 
// а также отображает ошибки при неправильном вводе данных

const { authenticate } = useAuthenticationStatusStore();

/**
 * Тип входных данных для аутентификации пользователя.
 *
 * @property {string} login Логин пользователя (имя учетной записи).
 * @property {string} password Пароль пользователя.
 */
export type AuthInputs = {
	login: string;
	password: string;
};

const router = useRouter();
const commonStore = useCommonStore();

const authenticationData = ref<AuthInputs>({
	login: "",
	password: "",
});

const isError = ref(false);

/**
 * Обрабатывает подтверждение аутентификации пользователя.
 *
 * Выполняет валидацию входных данных через `checkError`.
 * Если обнаружена ошибка, завершает выполнение без отправки запроса.
 * При отсутствии ошибок запускает процесс аутентификации,
 * передавая `login` и `password` в `authenticate`.
 *
 * @param data - Введённые пользователем данные для входа (`login`, `password`).
 * @returns Ничего не возвращает.
 */
function onAuthenticationConfirm(data: AuthInputs): void {
	const isError: boolean = checkError(data);
	if (isError) return;
	authenticate({ login: data.login, password: data.password });
	navigateMainPage();
}

/**
 * Выполняет переход на главную страницу после успешной аутентификации.
 *
 * Устанавливает флаг `isAfterLogin` в состояние `true`, чтобы зафиксировать
 * факт входа пользователя, затем инициирует навигацию на маршрут `/`.
 */
function navigateMainPage(): void {
	commonStore.setIsAfterLoginV2(true)
	router.push("/");
}

/**
 * Проверяет поля авторизации на наличие пустых значений.
 *
 * Если `login` или `password` пустые, устанавливает флаг `isError.value`
 * в `true` и возвращает это значение. В противном случае сбрасывает
 * флаг ошибки в `false` и возвращает его.
 *
 * @param data Объект с данными авторизации.
 * @returns `true`, если одно из обязательных полей пустое, иначе `false`.
 */
function checkError(data: AuthInputs): boolean {
	if (
		data.login === "" ||
		data.password === ""
	) {
		isError.value = true;
		return isError.value;
	} else {
		isError.value = false;
		return isError.value;
	}
}

</script>

<template>
	<div :class="'flex-column-layout flex-constrained full-size justify-center items-center'">
		<AuthAlert :is-error="isError" />
		<Card :class="'gap-4 card-primary'">

			<AuthHeader />
			<AuthBody :authenticationData="authenticationData" @on-authentication-confirm="onAuthenticationConfirm" />
			<AuthFooter />

		</Card>
	</div>
</template>
