<script setup lang="ts">
import { Field, FieldLabel } from '@/components/ui/field';
import { Input } from '@/components/ui/input';

const props = defineProps<{
    label?: string;
    placeHolder?: string;
    isRequired: boolean;
    fieldId: string;
    type: string;
}>();

</script>

<template>
    <Field>
        <FieldLabel :for="props.fieldId"> {{ props.label }} </FieldLabel>
        <Input :id="props.fieldId" :type="props.type" :placeholder="props.placeHolder" :required="props.isRequired" />
    </Field>
</template>