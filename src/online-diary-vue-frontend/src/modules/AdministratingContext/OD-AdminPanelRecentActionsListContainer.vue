<script setup lang="ts">
import Card from "../../components/ui/card/Card.vue";
import CardTitle from "../../components/ui/card/CardTitle.vue";
import CardFooter from "../../components/ui/card/CardFooter.vue";
import CardContent from "../../components/ui/card/CardContent.vue";
import CardDescription from "../../components/ui/card/CardDescription.vue";
import { Button } from "@/components/ui/button"
import {
  Alert,
  AlertDescription,
  AlertTitle,
} from "@/components/ui/alert"
</script>

<template>
    <section>
        <Card :class="'w-fit h-fit gap-2 my-0'">
            <CardTitle :class = "'mx-2'">
                <Label :class="'text-2xl'">Недавние действия</Label>
            </CardTitle>
            <section>
                <Alert>
                    <AlertTitle> Update! </AlertTitle>
                    <AlertDescription> Преподаватель П. П. проставил Студенту С. С. База данных 3</AlertDescription>
                </Alert>
            </section>
            <section>
                <Alert>
                    <AlertTitle> Update! </AlertTitle>
                    <AlertDescription> Преподаватель П. П. проставил Студенту С. С. База данных 4</AlertDescription>
                </Alert>
            </section>
            <section>
                <Alert>
                    <AlertTitle> Update! </AlertTitle>
                    <AlertDescription> Преподаватель П. П. проставил Студенту С. С. База данных 5</AlertDescription>
                </Alert>
            </section>
            <section>
                <Alert>
                    <AlertTitle> Update! </AlertTitle>
                    <AlertDescription> Преподаватель П. П. проставил Студенту С. С. База данных 4</AlertDescription>
                </Alert>
            </section>
            <section>
                <Alert>
                    <AlertTitle> Update! </AlertTitle>
                    <AlertDescription> Преподаватель П. П. проставил Студенту С. С. База данных 3</AlertDescription>
                </Alert>
            </section>
        </Card>
    </section>
</template>