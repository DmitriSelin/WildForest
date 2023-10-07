<script setup>
const emit = defineEmits(['update:value'])
const props = defineProps({
  errors: {
    type: Array,
    required: false
  },
  value: {
    type: String,
    default: ''
  },
  name: {
    type: String,
    required: true
  },
  type: {
    type: String,
    default: 'text'
  },
  placeholder: {
    type: String,
    required: true
  },
  label: {
    type: String,
    required: true
  },
  autocomplete: {
    type: String,
    default: ''
  },
  width: {
    type: String,
    default: '300px'
  }
})

const updateValue = (e) => {
  emit('update:value', e.target.value)
}
</script>

<template>
  <div class="form-input" :style="{ width: width }">
    <input :autocomplete="autocomplete" class="input-text" :type="type" :name="name" :id="name"
      :placeholder="placeholder" :value="value" @input="updateValue" />
    <label :for="name" class="input-label">{{ label }}</label>
    <TransitionGroup>
      <div class="form-error" v-for="element of errors" :key="element.$uid">
        <div class="form-error__message">{{ element.$message }}</div>
      </div>
    </TransitionGroup>
  </div>
</template>

<style lang="scss" scoped>
.form {
  &-input {
    position: relative;
    margin-right: 5px;
  }

  &-error {
    background: var(--danger);
    margin-top: 4px;
    border-radius: 7px;
    font-size: 13px;
    color: #fff;
    padding: 5px;
  }
}

.input {
  &-text {
    &::placeholder {
      color: var(--gray);
    }

    border: 1px solid var(--violet);
    background-color: var(--light-gray);
    color: var(--white);
    padding: 0 10px;
    height: 40px;
    border-radius: 7px;
    font-size: 15px;
    width: 100%;
    position: relative;
    z-index: 1;

    &:focus {
      transition: all 0.3s ease;

      &+.input-label {
        z-index: 1;
        opacity: 1;
        top: -20px;
      }
    }

    &:not(:placeholder-shown) {
      &+.input-label {
        z-index: 1;
        opacity: 1;
        top: -20px;
      }
    }
  }

  &-label {
    font-weight: bold;
    display: block;
    position: absolute;
    top: 20px;
    opacity: 0;
    z-index: -1;
    transition: 0.3s;
    font-size: 13px;
    color: var(--white);
  }
}

.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
</style>
