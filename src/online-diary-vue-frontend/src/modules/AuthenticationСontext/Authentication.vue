<script setup lang="ts">
import { Card } from "@/components/ui/card";
import AuthBody from "./components/AuthBody.vue";
import AuthFooter from "./components/AuthFooter.vue";
import AuthHeader from "./components/AuthHeader.vue";
import AuthAlert from "./components/AuthAlert.vue";
import { ref } from "vue";
import { useAuthenticationStatusStore } from "../Common/Authentication/authentication.status.store";

// страница аутентификации, 
// которая содержит в себе форму для ввода логина и пароля, 
// а также отображает ошибки при неправильном вводе данных

const { authenticate } = useAuthenticationStatusStore();

export type AuthInputs = {
	login: string;
	password: string;
};

const authenticationData = ref<AuthInputs>({
	login: "",
	password: "",
});

const isError = ref(false);

function onAuthenticationConfirm(data: AuthInputs): void {
	const isError: boolean = checkError(data);
	if (isError) return;
	authenticate({ login: data.login, password: data.password });
}

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
