<script setup lang="ts">
import { inject, onMounted } from 'vue';
import type { Discipline, DisciplineGroup, DisciplineSemester, DisciplineTeacher } from './discipline.models';
import { useDisciplinesStore } from './discipline.store';
import { Drawer, DrawerClose, DrawerContent, DrawerTrigger } from '@/components/ui/drawer';
import { CrossIcon, SearchIcon } from 'lucide-vue-next';
import AccentSelect from '../Common/Components/AccentSelect.vue';
import InputWithIcon from '../Common/Components/InputWithIcon.vue';
import Field from '@/components/ui/field/Field.vue';
import Checkbox from '@/components/ui/checkbox/Checkbox.vue';
import FieldContent from '@/components/ui/field/FieldContent.vue';
import { FieldLabel } from '@/components/ui/field';
import { Button } from '@/components/ui/button';
import { Label } from '@/components/ui/label';
import type { DisciplinesViewProps } from './disciplines.view.props';
import CloseButton from '../Common/Components/CloseButton.vue';


const store = useDisciplinesStore();
const teachers: DisciplineTeacher[] = [];
const semesters: DisciplineSemester[] = [];
const groups: DisciplineGroup[] = [];
const viewManager = inject<DisciplinesViewProps>("disciplinesViewManager");

onMounted(() => {
    const disciplines: Discipline[] = store.disciplines;

    disciplines
        .map((d) => d.teacher)
        .filter((t) => t !== null)
        .forEach((teacher) => {
            if (!teachers.some((t) => t.name === teacher?.name)) {
                teachers.push(teacher);
            }
        });

    disciplines
        .map((d) => d.semester)
        .filter((s) => s !== null)
        .forEach((sem) => {
            if (!semesters.some((s) => s.number === sem.number)) {
                semesters.push(sem);
            }
        });

    disciplines
        .map((d) => d.group)
        .filter((g) => g !== null)
        .forEach((grp) => {
            if (!groups.some((g) => g.name === grp.name)) {
                groups.push(grp);
            }
        });
});

</script>

<template>
    <Drawer :open="viewManager?.isFiltersDrawerOpen()">
        <DrawerContent class="card-primary rounded-md h-max border shrink-0">
            <div :class="'flex flex-row justify-between p-4'">
                <div class="bg-apricat-cream text-responsive-secondary font-semibold rounded-t-md">
                    Фильтрация
                </div>
                <CloseButton :onClick="() => viewManager?.toggleFiltersDrawer()" />
                <!-- <DrawerClose>
                    <DrawerTrigger>
                        <Button v-on:click="viewManager?.toggleFiltersDrawer" :variant="'primary'"
                            :class="'rounded-full'">
                            <CrossIcon :size="16" />
                        </Button>
                    </DrawerTrigger>
                </DrawerClose> -->
            </div>
            <div class="p-4">
                <div class="text-responsive-tertiary flex-column-layout gap-2">

                    <InputWithIcon :label="'Дисциплина'" :class="'text-responsive-tertiary'" :place-holder="'название'"
                        :icon="SearchIcon" />

                    <AccentSelect :placeholder="'выбрать группу'" :label="'Группы'" :items="groups"
                        :item-key="(g) => g.id" :item-display-text="(g) => g.name" />

                    <AccentSelect :placeholder="'выбрать семестр'" :label="'Семестр'" :items="semesters"
                        :item-key="(s) => s.id" :item-display-text="(s) => s.number" />

                    <AccentSelect :placeholder="'выбрать преподавателя'" :label="'Преподаватели'" :items="teachers"
                        :item-key="(t) => t.id" :item-display-text="(t) => t.name" />

                    <Field :orientation="'horizontal'">
                        <Checkbox :id="'archived-filter'" :default-value="false" />
                        <FieldContent>
                            <FieldLabel :class="'text-responsive-tertiary'" for="archived-filter">
                                Архивные дисциплины
                            </FieldLabel>
                        </FieldContent>
                    </Field>

                    <Button :variant="'secondary'" :class="'mx-auto'">
                        <Label :class="'text-responsive-tertiary font-prussian-blue'">Сбросить</Label>
                    </Button>
                </div>
            </div>
        </DrawerContent>
    </Drawer>
</template>