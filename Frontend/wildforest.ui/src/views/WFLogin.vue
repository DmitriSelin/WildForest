<script setup>
import WFInput from "@/components/inputs/WFInput.vue"
import WFButton from "@/components/buttons/WFButton.vue"
import WFEmptyLink from "@/components/buttons/WFEmptyLink.vue";
</script>

<template>
    <main class="main">
        <div class="left-block">
            <div class="left-block-name">
                <img src="../assets/images/heart.svg" alt="heart" />
                <h1>Login</h1>
            </div>
            <form action="" method="post" class="left-block-content">
                <WFInput label="Email" name="email" placeholder="Input your email" />
                <WFInput label="Password" type="password" name="password" placeholder="Input your password" />
                <div class="left-block-content-btn">
                    <WFButton label="Login" size="large"/>
                    <WFEmptyLink to="/" text="Have not an account?" title="Register" />
                </div>
                <span class="left-block-content-txt">or</span>
                <WFButton iconPackName="brands" icon="google"/>
            </form>
        </div>
        <div class="right-block">
            <img src="../assets/images/appLogo.svg" alt="logo">
            <h2>Wild forest</h2>
        </div>
    </main>
</template>

<style lang="scss" scoped>
.main {
    height: 100%;
    display: flex;
    flex-direction: row;
}

.left-block {
    height: 100%;
    width: 60%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;

    &-name {
        display: flex;
        gap: 20px;
        margin-bottom: 30px;

        h1 {
            padding-top: 10px;
            color: var(--white);
        }
    }

    &-content {
        display: flex;
        flex-direction: column;
        flex-wrap: nowrap;
        gap: 40px;

        &-btn {
            display: flex;
            flex-direction: column;
            gap: 20px;
            align-items: center;
        }

        &-txt {
            text-align: center;
            color: var(--white);
        }
    }
}

.right-block {
    width: 40%;
    height: 100%;
    background-color: var(--light-gray);
}
</style>