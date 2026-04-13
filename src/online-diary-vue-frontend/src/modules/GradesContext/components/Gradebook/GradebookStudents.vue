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
  border: 1px solid hsl(0 0% 100% / 0.06);
  border-radius: 0.88rem;
  background:
    linear-gradient(180deg, hsl(220 12% 25% / 0.96), hsl(220 12% 21% / 0.98)),
    var(--bg-primary-accent-2);
  color: var(--fg-primary);
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
