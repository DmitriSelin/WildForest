<script setup>
import WFInput from "@/components/inputs/WFInput.vue"
import WFButton from "@/components/buttons/WFButton.vue"
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";

import { ref } from "vue";

const selectedCity = ref();
const cities = ref([
    { name: 'New York', code: 'NY' },
    { name: 'Rome', code: 'RM' },
    { name: 'London', code: 'LDN' },
    { name: 'London1', code: 'LDN1' },
    { name: 'London2', code: 'LDN2' },
    { name: 'London3', code: 'LDN3' },
    { name: 'London4', code: 'LDN4' },
    { name: 'London5', code: 'LDN5' },
    { name: 'London7', code: 'LDN7' },
    { name: 'London8', code: 'LDN8' },
    { name: 'London6', code: 'LDN6' },
    { name: 'Istanbul', code: 'IST' },
    { name: 'Paris', code: 'PRS' }
]);

const register = () => {

}
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form class="left-block-content">
                <WFInput label="Name" name="name" placeholder="Input your name" />
                <WFInput label="Surname" name="secondName" placeholder="Input your surname" />
                <WFInput label="Email" name="email" placeholder="Input your email" />
                <Dropdown v-model="selectedCity" editable :options="cities" optionLabel="name" placeholder="Select a City" class="combobox"/>
                <WFInput label="Password" type="password" name="password" placeholder="Input your password" />
                <WFInput label="Password" type="password" name="samePassword" placeholder="Input the same password" />
                <div class="left-block-content-btn">
                    <WFButton label="Register" size="large" @click="register"/>
                    <WFEmptyLink to="login" text="Already have an account?" title="Login" />
                </div>
                <span class="left-block-content-txt">or</span>
                <WFButton iconPackName="brands" icon="google" />
            </form>
        </div>
        <div class="right-block">
            <img src="../assets/images/appLogo.svg" alt="logo" class="right-block-image">
            <h1 class="right-block-name">Wild forest</h1>
        </div>
    </main>
</template>

<style lang="scss" scoped>
@import "./styles/views.scss";
@import "../components/styles/combobox.scss";
</style>