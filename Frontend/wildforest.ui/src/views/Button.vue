<!-- eslint-disable vue/multi-word-component-names -->
<script setup>
import Button from "@/components/buttons/WFButton.vue";

function go(){
    alert("hello!");
}
</script>

<template>
<h1>Buttons</h1>
<Button label="Go to top" size="large" @click="go"/>
</template>