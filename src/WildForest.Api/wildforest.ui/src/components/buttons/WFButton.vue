<script setup>
const props = defineProps({
  label: {
    type: String,
    default: 'Button'
  },
  type: {
    type: String,
    default: 'submit'
  },
  color: {
    type: String,
    default: 'accent'
  },
  disabled: {
    type: Boolean,
    required: false
  },
  rounded: {
    type: Boolean,
    required: false
  },
  outlined: {
    type: Boolean,
    required: false
  },
  icon: {
    type: String,
    required: false
  },
  iconPackName: {
    type: String,
    default: 'solid'
  },
  size: {
    type: String,
    default: 'normal'
  }
})

const emit = defineEmits(['click'])

const clickOnButton = () => {
  emit('click')
}
</script>

<template>
  <button :class="[
    'btn',
    `btn_${color}`,
    { btn_rounded: rounded },
    { btn_outlined: outlined },
    { btn_icon: icon },
    { btn_large: size === 'large' }
  ]" :type="type" :disabled="disabled" @click="clickOnButton">
    <span v-if="icon">
      <font-awesome-icon :icon="`fa-${iconPackName} fa-${icon}`" />
    </span>
    <span v-else>{{ label }}</span>
  </button>
</template>

<style lang="scss" scoped>
.btn {
  margin-right: 10px;
  color: var(--white);
  font-weight: bold;
  cursor: pointer;
  padding: 5px 20px;
  transition: 0.2s;
  border-radius: 7px;
  font-size: 15px;

  &_accent {
    background-color: var(--accent);
    border: 1px solid var(--accent);

    &:enabled:hover {
      background-color: var(--accent-hover);
    }
  }

  &_success {
    background-color: var(--success);
    border: 1px solid var(--success);

    &:enabled:hover {
      background-color: var(--success-hover);
    }
  }

  &_warning {
    background-color: var(--warning);
    border: 1px solid var(--warning);

    &:enabled:hover {
      background-color: var(--warning-hover);
    }
  }

  &_danger {
    background-color: var(--danger);
    border: 1px solid var(--danger);

    &:enabled:hover {
      background-color: var(--danger-hover);
    }
  }

  &:disabled {
    opacity: 0.6;
    cursor: default;
  }

  &_rounded {
    border-radius: 15px;
  }

  &_outlined {
    background-color: transparent;
    color: var(--white);
  }

  &_icon {
    padding: 0;
    height: 40px;
    width: 40px;
    border-radius: 50%;
  }

  &_large {
    height: 48px;
    padding: 10px 30px;
  }
}
</style>
