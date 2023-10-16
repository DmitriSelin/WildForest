<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFDropdown from "@/components/inputs/WFDropdown.vue";
import WFButton from "@/components/buttons/WFButton.vue";
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
import { ref, onMounted } from "vue";
import { useUserStore } from "@/stores/UserStore"
import { useToast } from "primevue/usetoast";

const toast = useToast();

const registerRequest = ref({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    cityId: "",
    languageId: ""
});
const samePassword = ref("");
const userStore = useUserStore();

onMounted(async () => {
    await userStore.getCitiesByCountry();
});

const register = () => {
    userStore.register(registerRequest.value);
}

const registerWithGoogle = () => {
    toast.add({ severity: 'info', summary: 'Info', detail: 'This function is still in development', life: 3000 });
}
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Registration</h1>
            </div>
            <form @submit.prevent="register" class="left-block-content">
                <WFInput label="Firstname" name="firstName" placeholder="Input your first name"
                    v-model:value="registerRequest.firstName" />
                <WFInput label="Lastname" name="lastName" placeholder="Input your lastname"
                    v-model:value="registerRequest.lastName" />
                <WFInput label="Email" name="email" placeholder="Input your email" v-model:value="registerRequest.email" />
                <WFDropdown :options="userStore.cities" placeholder="Select a City" id="cityDropdown"
                    error="This field is required" />
                <WFInput label="Password" type="password" name="password" placeholder="Input your password"
                    v-model:value="registerRequest.password" />
                <WFInput label="Password" type="password" name="samePassword" placeholder="Input the same password"
                    v-model:value="samePassword" />
                <div class="left-block-content-btn">
                    <WFButton label="Register" size="large" />
                    <WFEmptyLink to="login" text="Already have an account?" title="Login" />
                </div>
                <span class="left-block-content-txt">or</span>
                <Toast />
                <WFButton iconPackName="brands" icon="google" @click="registerWithGoogle" type="button" />
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