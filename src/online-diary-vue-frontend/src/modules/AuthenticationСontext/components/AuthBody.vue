<script setup lang="js">
import { Button } from "@/components/ui/button";
import { CardTitle, CardContent } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
</script>

<template>
  <CardContent :class="'flex flex-col justify-center items-center gap-2'">
    <Input></Input>
    <Input type="password"></Input>
    <Button variant="outline">
      <div>Войти</div>
    </Button>
  </CardContent>
</template>
