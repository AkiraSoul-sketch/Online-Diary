<script setup lang="ts">
import { Drawer, DrawerContent, DrawerTitle } from '@/components/ui/drawer';
import DrawerHeader from '@/components/ui/drawer/DrawerHeader.vue';
import { FieldDescription, FieldGroup, FieldLegend } from '@/components/ui/field';
import FieldSet from '@/components/ui/field/FieldSet.vue';
import AccentSelect from '../Common/Components/AccentSelect.vue';
import InputField from '../Common/Components/InputField.vue';
import CloseButton from '../Common/Components/CloseButton.vue';
import { inject } from 'vue';
import type { DisciplinesViewProps } from './disciplines.view.props';

const props: DisciplinesViewProps | undefined = inject<DisciplinesViewProps>("disciplinesViewManager");

</script>

<template>
    <Drawer :open="props?.isCreateDrawerOpen()" :dismissible="false">
        <DrawerContent @interact-outside="() => props?.toggleCreateDrawer()">
            <DrawerHeader :class="'create-drawer-header'">
                <DrawerTitle>
                    Создание дисциплины
                </DrawerTitle>
                <CloseButton :onClick="() => props?.toggleCreateDrawer()" />
            </DrawerHeader>
            <FieldSet :class="'form-container'">
                <FieldGroup :class="'form-content'">
                    <FieldLegend :class="'form-item'">Основная информация</FieldLegend>
                    <FieldDescription :class="'form-item'">Поля с * обязательны для заполнения</FieldDescription>
                    <InputField :class="'form-item'" :label="'Название *'" :placeHolder="'Введите название дисциплины'"
                        :isRequired="true" :fieldId="'name'" :type="'text'" />
                    <FieldLegend :class="'form-item'">Дополнительная информация</FieldLegend>
                    <AccentSelect :class="'form-item'" :placeholder="'Выбрать группу'" :label="'Группа'" :items="[]"
                        :item-key="() => 1" :item-display-text="() => 'placeholder'" />
                    <AccentSelect :class="'form-item'" :placeholder="'Выбрать семестр'" :label="'Семестр'" :items="[]"
                        :item-key="() => 1" :item-display-text="() => 'placeholder'" />
                    <AccentSelect :class="'form-item'" :placeholder="'Выбрать преподавателя'" :label="'Преподаватель'"
                        :items="[]" :item-key="() => 1" :item-display-text="() => 'placeholder'" />
                </FieldGroup>
            </FieldSet>
        </DrawerContent>
    </Drawer>
</template>

<style lang="css" scoped>
.create-drawer-header {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.form-container {
    display: flex;
    flex-direction: column;
}

.form-content {
    padding: 1em;
    align-self: center;
    width: clamp(300px, 50%, 500px);
}
</style>