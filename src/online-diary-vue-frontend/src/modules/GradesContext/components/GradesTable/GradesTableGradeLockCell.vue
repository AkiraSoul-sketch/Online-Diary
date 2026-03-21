<script lang="ts">
import { Button } from "@/components/ui/button";
import GradesTableCell from "./GradesTableCell.vue";
import { LockIcon } from "lucide-vue-next";

export default {
  components: {
    GradesTableCell,
    Button,
    LockIcon,
  },
  props: {
    cellClass: {
      type: String,
      required: false,
    },
    cellWidth: {
      type: Number,
      required: false,
    },
  },
};
</script>

<template>
  <GradesTableCell
    :cell-class="cellClass"
    :show-text="false"
    :cell-width="cellWidth"
  >
    <div :class="'flex justify-center items-center p-1'">
      <Button :size="'sm'">
        <LockIcon />
      </Button>
    </div>
  </GradesTableCell>
</template>
