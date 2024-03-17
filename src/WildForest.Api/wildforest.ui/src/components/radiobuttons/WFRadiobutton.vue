<script setup>
const emit = defineEmits(['update:checkedValue'])

const props = defineProps({
  name: {
    type: String,
    default: ''
  },
  id: {
    type: String,
    default: ''
  },
  value: {
    type: String,
    default: ''
  },
  label: {
    type: String,
    default: ''
  },
  checked: {
    type: Boolean,
    default: false
  },
  disabled: {
    type: Boolean,
    default: false
  },
  checkedValue: {
    type: String,
    required: true
  },
  icon: {
    type: String,
    default: ''
  }
})

const click = (event) => {
  emit('update:checkedValue', event.target.value)
}
</script>

<template>
  <input :class="[{ 'radiobutton': icon === '' }, { 'radiobutton__vote': icon !== '' }]" class="radio" type="radio"
    :name="name" :id="id" :value="value" :checked="checked" :disabled="disabled" @input="click($event)" />
  <label :for="id">{{ label }}</label>
</template>

<style lang="scss" scoped>
.radio {
  position: absolute;
  opacity: 0;
  z-index: -1;
}

.radiobutton {

  &+label {
    display: inline-flex;
    align-items: center;
    user-select: none;
    cursor: pointer;
    color: var(--white);
  }

  &+label::before {
    content: '';
    display: inline-block;
    width: 24px;
    height: 24px;
    flex-shrink: 0;
    flex-grow: 0;
    border: 1px solid var(--gray);
    border-radius: 6px;
    margin-right: 10px;
    background-repeat: no-repeat;
    background-position: center center;
    background-size: 50% 50%;
    border-radius: 50%;
  }

  &:checked+label::before {
    border-color: var(--accent);
    background-color: var(--accent-hover);
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 8 8'%3e%3cpath fill='%23fff' d='M6.564.75l-3.59 3.612-1.538-1.55L0 4.26 2.974 7.25 8 2.193z'/%3e%3c/svg%3e");
  }

  &:not(:disabled):not(:checked)+label:hover::before {
    border-color: var(--accent-hover);
  }

  &:not(:disabled):active+label::before {
    background-color: var(--accent);
    border: 1px solid var(--white);
  }

  &:focus+label::before {
    box-shadow: 0px 7px 20px rgba(0, 0, 0, 0.07);
  }

  &:focus:not(:checked)+label::before {
    border-color: var(--accent);
  }

  $disabledBackColor: #e9ecef;
  $disabledBorder: 1px solid #ecebed;

  &:disabled:not(:checked)+label::before {
    background-color: $disabledBackColor;
    border: $disabledBorder;
  }

  &:disabled:is(:checked)+label::before {
    background-color: $disabledBackColor;
    border: $disabledBorder;
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 8 8'%3e%3cpath fill='%23fff' d='M6.564.75l-3.59 3.612-1.538-1.55L0 4.26 2.974 7.25 8 2.193z'/%3e%3c/svg%3e");
  }
}

.radiobutton__vote {
  &+label {
    position: relative;
    cursor: pointer;
    padding-left: 25px;
  }

  &+label:before {
    position: absolute;
    left: -10px;
    top: -10px;
    width: 20px;
    height: 20px;
  }
}

input#up+label:before {
  content: url("../../assets/icons/arrows/black_up.svg");
}

input#down+label:before {
  content: url("../../assets/icons/arrows/black_down.svg");
}

input#up:checked+label:before {
  content: url("../../assets/icons/arrows/blue_up.svg");
}

input#down:checked+label:before {
  content: url("../../assets/icons/arrows/blue_down.svg");
}
</style>