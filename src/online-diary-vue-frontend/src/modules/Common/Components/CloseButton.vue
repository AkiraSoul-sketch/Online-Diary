<script setup lang="ts">
import { Button } from '@/components/ui/button';
import { XIcon } from 'lucide-vue-next';

const props = defineProps<{
    onClick: () => void;
}>();

</script>

<template>
    <Button :variant="'outline'" :size="'icon'" :class="'rounded-full'" @click="props.onClick">
        <XIcon />
    </Button>
</template>