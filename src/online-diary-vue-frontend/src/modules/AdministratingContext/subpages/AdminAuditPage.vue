<script setup lang="ts">
import AdminActivityChart from "../components/AdminActivityChart.vue";
import AdminActivityList from "../components/AdminActivityList.vue";
</script>

<template>
	<section :class="'admin-audit-content-container'">
		<AdminActivityChart />
		<AdminActivityList />
	</section>
</template>

<style scoped lang="css">
.admin-audit-content-container {
	min-height: 0;
	min-width: 0;
	display: grid;
	grid-template-columns: 1fr;
	gap: 1em;
	padding: 1em;
}

@media (width > 1100px) {
	.admin-audit-content-container {
		grid-template-columns: 1fr 1fr;
	}
}
</style>
