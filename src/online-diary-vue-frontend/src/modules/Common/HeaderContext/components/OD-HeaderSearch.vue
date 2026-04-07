<script setup lang="ts">
import { Field } from '@/components/ui/field';
import { InputGroup, InputGroupAddon, InputGroupInput } from '@/components/ui/input-group';
import { SearchIcon } from 'lucide-vue-next';


</script>

<template>
    <Field :class="'p-1 w-1/2 flex'">
        <InputGroup :class="'border-none !bg-[var(--bg-sixth-hover)] dark:border'">
            <InputGroupInput :class="' text-responsive-secondary'" :placeholder="'поиск'" />
            <InputGroupAddon :align="'inline-end'">
                <SearchIcon />
            </InputGroupAddon>
        </InputGroup>
    </Field>
</template>