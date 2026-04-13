<script setup lang="ts">
import type { StudentInfo } from "../gradebook.models";

const props = defineProps<{
  student: StudentInfo;
}>();

function studentNameText(student: StudentInfo): string {
  const parts = student.name.split(" ");
  if (parts.length < 3) return student.name;
  return `${parts[0]} ${parts[1][0]}. ${parts[2][0]}.`;
}
</script>

<template>
  <div class="student-block" :title="props.student.name">
    <span class="student-block__name">{{ studentNameText(props.student) }}</span>
  </div>
</template>

<style scoped lang="css">
.student-block {
  display: flex;
  align-items: center;
  height: 100%;
  min-width: 0;
  padding-inline: 0.85rem;
  border: 1px solid var(--panel-border-color);
  border-radius: 0.88rem;
  background: var(--panel-bg-gradient), var(--panel-bg);
  color: var(--panel-text-color);
  box-shadow: var(--shadow-primary);
}

.student-block__name {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  font-size: 0.9rem;
  line-height: 1.15;
}
</style>
