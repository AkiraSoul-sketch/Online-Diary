<script lang="ts">
export default {
  data() {
    const sectionStyle: string =
      "flex flex-col w-full h-screen justify-center items-center ";
    return {
      sectionStyle,
    };
  },
};
</script>

<template>
  <section :class="sectionStyle">
    <div :class="'w-1/12 h-1/8 bg-(--bg-neutral-95)'"></div>
  </section>
</template>
