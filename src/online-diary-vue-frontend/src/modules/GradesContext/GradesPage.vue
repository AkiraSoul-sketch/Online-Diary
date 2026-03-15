<script lang="ts">
import ODSidebar from "./components/OD-Sidebar.vue";
import { Label } from "@/components/ui/label";
import { Button } from "@/components/ui/button";
import {
  Card,
  CardTitle,
  CardContent,
  CardDescription,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import SidebarProvider from "@/components/ui/sidebar/SidebarProvider.vue";
import ODGradesPagePageTitle from "./components/OD-GradesPage-PageTitle.vue";
import ODJournalEditBlock from "./components/JournalEditBlock/OD-JournalEditBlock.vue";
import ODJournalDateBlock from "./components/OD-JournalDateBlock.vue";
import ODStudentsSearch from "./components/OD-StudentsSearch.vue";
import ODGradedThemesList from "./components/GradedThemesList/OD-GradedThemesList.vue";

export default {
  components: {
    Label,
    Button,
    Card,
    CardTitle,
    CardContent,
    CardDescription,
    Input,
    ODSidebar,
    ODGradesPagePageTitle,
    ODJournalEditBlock,
    ODJournalDateBlock,
    SidebarProvider,
    ODStudentsSearch,
    ODGradedThemesList,
  },
};
</script>

<template>
  <SidebarProvider>
    <ODSidebar />
    <section class="gap-0 p-0 mx-2 w-full">
      <!-- // page container -->
      <Card :class="'w-full h-full gap-2 my-0'">
        <!-- page title -->
        <ODGradesPagePageTitle />
        <CardContent :class="'px-2'">
          <section class="grid grid-cols-2 gap-2">
            <!-- edit journal items here -->
            <div :class="'grid grid-cols-2 gap-2'">
              <ODJournalEditBlock />
              <!-- мини информация -->
              <!-- Дата -->
              <ODJournalDateBlock />
              <!-- Placeholder временно -->
              <Card :class="'inner-card-1 gap-0 p-2 h-fit'">
                <CardTitle>
                  <Label :class="'text-xl'">Placeholder</Label>
                  <Label :class="'text-xl'"></Label>
                </CardTitle>
              </Card>
              <!-- поиск студента -->
              <ODStudentsSearch />
            </div>
            <!-- темы за которые выставлены оценки -->
            <ODGradedThemesList />
            <section class="col-span-full">
              <!-- Таблица оценок -->
              <Card :class="'inner-card-1 gap-0 p-2'">
                <CardTitle>
                  <Label :class="'text-2xl'">Оценки:</Label>
                </CardTitle>
              </Card>
            </section>
          </section>
        </CardContent>
      </Card>
    </section>
  </SidebarProvider>
</template>
