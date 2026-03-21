<script setup lang = ts>
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import CardContent from "../../components/ui/card/CardContent.vue";
import CardDescription from "../../components/ui/card/CardDescription.vue";
import { Button } from "@/components/ui/button"


</script>
<template>
    <section>
        <Card :class="'w-fit h-full gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Управление студентами</Label>
            </CardTitle>
            <CardDescription>Настройка и редактирования страницы студентов</CardDescription>
            <CardContent>
                <Button variant = "outline">Создание</Button>
                <Button variant = "outline"> Редактирование</Button>
                <Button variant = "outline"> Архивация</Button>
                <section>
                    <Button> Открыть</Button>
                </section>
            </CardContent>
        </Card>
    </section>
    <section>
        <Card :class="'w-fit h-full gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Управление дисциплинами</Label>
            </CardTitle>
            <CardContent>
                <Button variant = "outline">Создание</Button>
                <Button variant = "outline"> Редактирование</Button>
                <Button variant = "outline"> Архивация</Button>
                <section>
                    <Button> Открыть</Button>
                </section>
            </CardContent>
        </Card>
    </section>
    <section>
        <Card :class="'w-fit h-full gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Управление преподавателями</Label>
            </CardTitle>
            <CardContent>
                <Button variant = "outline">Создание</Button>
                <Button variant = "outline"> Редактирование</Button>
                <Button variant = "outline"> Архивация</Button>
                <section>
                    <Button> Открыть</Button>
                </section>
            </CardContent>
        </Card>
    </section>
    <section>
        <Card :class="'w-fit h-full gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Просмотр журнала</Label>
            </CardTitle>
            <CardContent>
                <Button variant = "outline"> Редактирование</Button>
                <section>
                    <Button> Открыть</Button>
                </section>
            </CardContent>
        </Card>
    </section>
</template>
<style></style>