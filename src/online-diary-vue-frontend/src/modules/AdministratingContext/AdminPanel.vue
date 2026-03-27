<script setup lang="ts">
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import ODAdminPanelRecentActionDiagram from "./component/OD-AdminPanelRecentActionDiagram.vue";
import ODAdminPanelSectionItem from "./component/OD-AdminPanelSectionItem.vue";
import ODAdminPanelRecentActionListItem from "./component/OD-AdminPanelRecentActionListItem.vue";
import ODAdminPanelRecentActionsListContainer from "./component/OD-AdminPanelRecentActionsListContainer.vue";
import ODAdminPanelSectionsContainer from "./component/OD-AdminPanelSectionsContainer.vue";

const array = [1, 2, 3, 4, 5];
</script>

<template>
  <!-- page begin -->
  <section class="gap-0 mx-2 w-full h-screen p-2">
    <Card :class="'w-full h-full gap-2 grow'">
      <CardTitle :class="'p-2'" :text="'Страница Администратора'"> </CardTitle>
      <div class="flex flex-col gap-2 h-full">
        <section class="grid grid-cols-2 gap-2 p-0 mx-2 h-full">
          <!-- users activity section -->
          <ODAdminPanelRecentActionDiagram></ODAdminPanelRecentActionDiagram>
          <ODAdminPanelRecentActionsListContainer>
            <div>
              <section v-for="a of [1, 2, 3, 4, 5]">
                <ODAdminPanelRecentActionListItem></ODAdminPanelRecentActionListItem>
              </section>
            </div>
          </ODAdminPanelRecentActionsListContainer>
        </section>

        <!--administration section-->
        <section class="gap-2 p-2 h-full grow">
          <ODAdminPanelSectionsContainer>
            <div class="grid grid-cols-3 gap-2 w-full grow">
              <section
                v-for="a of [1, 2, 3, 4, 5, 6]"
                :class="'grow min-h-42.5'"
              >
                <!--settings-->
                <ODAdminPanelSectionItem></ODAdminPanelSectionItem>
              </section>
            </div>
          </ODAdminPanelSectionsContainer>
        </section>
      </div>
    </Card>
  </section>
</template>
