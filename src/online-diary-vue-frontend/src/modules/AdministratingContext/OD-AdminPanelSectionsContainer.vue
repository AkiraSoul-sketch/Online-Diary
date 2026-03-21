<script lang="ts">
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import CardFooter from "../../components/ui/card/CardFooter.vue";
import CardContent from "../../components/ui/card/CardContent.vue";
import CardDescription from "../../components/ui/card/CardDescription.vue";
import { Button } from "@/components/ui/button"

import ODAdminPanelSectionItem from "./OD-AdminPanelSectionItem.vue";
export default {
    components: {
        
    }
}
</script>
<template>
    <section>
        <Card :class="'w-fit h-full gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Секция администрирования</Label>
            </CardTitle>
        </Card>
    </section>
</template>
<style></style>