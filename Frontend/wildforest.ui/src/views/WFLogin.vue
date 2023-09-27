<script setup>
import WFInput from "@/components/inputs/WFInput.vue"
import WFButton from "@/components/buttons/WFButton.vue"
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" class="left-block-name-heart" />
                <h1>Login</h1>
            </div>
            <form action="" method="post" class="left-block-content">
                <WFInput label="Email" name="email" placeholder="Input your email" />
                <WFInput label="Password" type="password" name="password" placeholder="Input your password" />
                <div class="left-block-content-btn">
                    <WFButton label="Login" size="large" />
                    <WFEmptyLink to="/" text="Have not an account?" title="Register" />
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
</style>