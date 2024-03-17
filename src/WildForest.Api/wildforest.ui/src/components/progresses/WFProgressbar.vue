<script setup>
const props = defineProps({
    maxWidth: {
        type: String,
        default: '300px'
    },
    percent: {
        type: Number,
        required: true
    },
    lineColor: {
        type: String,
        default: 'accent'
    },
    backgroundColor: {
        type: String,
        default: 'gray'
    },
    textColor: {
        type: String,
        default: 'white'
    }
})
</script>

<template>
  <div class="progress-container" :style="[{'max-width': maxWidth}]">
    <span class="progress-percent" :style="[{'color': `var(--${textColor})`}]">{{ percent }}%</span>
    <div class="progress" :style="[{'background': `var(--${backgroundColor})`}]">
      <div class="progress-bar" :style="[{'width': `${percent}%`}, {'background': `var(--${lineColor})`}]"></div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.progress {
  height: 10px;
  border-radius: 4px;
  &-container {
    margin-bottom: 20px;
    overflow: hidden;
  }
  &-bar {
    height: 100%;
    border-radius: 4px;
    transition: 0.5s;
  }
  &-percent {
    display: block;
    text-align: center;
    font-weight: bold;
    font-size: 17px;
    margin-bottom: 10px;
  }
}
</style>
