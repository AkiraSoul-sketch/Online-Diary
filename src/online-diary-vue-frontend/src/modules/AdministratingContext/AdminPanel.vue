<script setup lang="ts">
import Label from "@/components/ui/label/Label.vue";
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import {
  Item,
  ItemGroup,
  ItemContent,
  ItemDescription,
} from "@/components/ui/item";

import ODAdminPanelRecentActionDiagram from "./component/OD-AdminPanelRecentActionDiagram.vue";
import ODAdminPanelSectionItem from "./component/OD-AdminPanelSectionItem.vue";
import ODAdminPanelRecentActionListItem from "./component/OD-AdminPanelRecentActionListItem.vue";
import ODAdminPanelRecentActionsListContainer from "./component/OD-AdminPanelRecentActionsListContainer.vue";

const array = [1, 2, 3, 4, 5];
</script>

<template>
  <!-- page begin -->
  <section class="gap-0 mx-2 w-full h-screen p-2">
    <Card :class="'w-full h-full gap-2 grow'">
      <CardTitle :class="'p-2'">
        <Label :class="'text-2xl'">Страница Администратора</Label>
      </CardTitle>
      <div class="flex flex-col gap-2 h-full">
        <section class="grid grid-cols-2 gap-2 p-0 mx-2 h-full">
          <!-- users activity section -->
          <ODAdminPanelRecentActionDiagram></ODAdminPanelRecentActionDiagram>
          <ODAdminPanelRecentActionsListContainer>
            <section v-for="a of [1, 2, 3, 4, 5]">
              <ODAdminPanelRecentActionListItem></ODAdminPanelRecentActionListItem>
            </section>
          </ODAdminPanelRecentActionsListContainer>
        </section>

        <!--administration section-->
        <section class="gap-2 p-2 h-full grow">
          <CardTitle :class="'p-2'">
            <Label :class="'text-2xl'">Секция администрирования</Label>
          </CardTitle>
          <div class="grid grid-cols-3 gap-2 w-full grow">
            <section v-for="a of [1, 2, 3, 4, 5, 6]" :class="'grow min-h-42.5'">
              <!--settings-->
              <ODAdminPanelSectionItem></ODAdminPanelSectionItem>
            </section>
          </div>
        </section>
      </div>
    </Card>
  </section>
</template>
