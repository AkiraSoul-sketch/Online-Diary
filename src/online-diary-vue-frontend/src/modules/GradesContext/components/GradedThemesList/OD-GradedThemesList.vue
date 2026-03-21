<script lang="ts">
import { Card, CardContent } from "@/components/ui/card";
import ODGradedThemesListTitle from "./OD-GradedThemesListTitle.vue";
import ODGradedThemesListTable from "./OD-GradedThemesListTable.vue";

export default {
  components: {
    Card,
    CardContent,
    ODGradedThemesListTitle,
    ODGradedThemesListTable,
  },
};
</script>

<template>
  <Card :class="'inner-card-1 gap-0 p-2 h-full'">
    <ODGradedThemesListTitle />
    <CardContent>
      <ODGradedThemesListTable />
    </CardContent>
  </Card>
</template>
