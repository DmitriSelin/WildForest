<script setup>
import WFInput from "@/components/inputs/WFInput.vue"
import WFButton from "@/components/buttons/WFButton.vue"
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
import { ref } from "vue";
import { useUserStore } from "@/stores/UserStore"
import { useToast } from "primevue/usetoast";
import { getLoginFormData } from "@/infrastructure/formProvider";
import { goTo } from "@/api/api";

const userStore = useUserStore();
const formData = ref(getLoginFormData());

const login = async () => {
    const result = await userStore.login({ email: formData.value.email, password: formData.value.password });

    if (result === true) {
        goTo("Home");
    }
    else if (result === false) {
        toast.add({ severity: 'error', summary: 'Error', detail: userStore.errorMessage, life: 10000 });
    }
}

const toast = useToast();
const loginWithGoogle = () => {
    toast.add({ severity: 'info', summary: 'Info', detail: 'This function is still in development', life: 3000 });
}
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Login</h1>
            </div>
            <form @submit.prevent="login" class="left-block-content">
                <WFInput label="Email" type="Email" name="email" placeholder="Input your email"
                    v-model:value="formData.email" autocomplete="email" />
                <WFInput label="Password" type="password" minLength="6" name="password" placeholder="Input your password"
                    v-model:value="formData.password" autocomplete="current-password" />
                <div class="left-block-content-btn">
                    <WFButton label="Login" size="large" />
                    <WFEmptyLink to="credentials" text="Have not an account?" title="Register" />
                </div>
                <span class="left-block-content-txt">or</span>
                <WFButton iconPackName="brands" icon="google" type="button" @click="loginWithGoogle" />
                <Toast />
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
</style>