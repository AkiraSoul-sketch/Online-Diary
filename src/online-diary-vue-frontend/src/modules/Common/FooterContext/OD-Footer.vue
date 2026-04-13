<script setup lang="ts">
import { watch } from 'vue';
import { useCommonStore } from '../Stores/common.store';
import { useElementSizeObservabilityV2 } from '../Composables/useElementSizeObservabilityV2';

const footerSizeObserver = useElementSizeObservabilityV2();
const commonStore = useCommonStore();

watch(() => footerSizeObserver.height.value, ($height) => {
  const value: number = $height;
  commonStore.adjustFooterHeight(value);
}, { immediate: true })

</script>

<template>
  <footer :class="'flex w-full bg-zinc-500'" :ref="footerSizeObserver.element">
    <pre :class="'text-white font-semibold self-center'">Заглушка для футера. Компонент: OD-Footer.vue</pre>
  </footer>
</template>
