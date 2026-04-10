import { computed, ref, type Ref } from "vue";

const useMobileDisciplines = () => {
  const filtersOpen: Ref<boolean> = ref(false);
  const isOpen = computed(() => filtersOpen.value);

  function toggleMobileFilters(): void {
    filtersOpen.value = !filtersOpen.value;
  }

  function closeMobileFilters(): void {
    filtersOpen.value = false;
  }

  function openMobileFilters(): void {
    filtersOpen.value = true;
  }

  return {
    isOpen,
    toggleMobileFilters,
    closeMobileFilters,
    openMobileFilters,
  };
};

export { useMobileDisciplines };
