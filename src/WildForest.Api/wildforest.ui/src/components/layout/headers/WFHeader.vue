<script setup>
import WFInput from '@/components/inputs/WFInput.vue'
import WFButton from '@/components/buttons/WFButton.vue'
import { useUserStore } from "@/stores/UserStore";
import { ref, onMounted } from 'vue';

const userStore = useUserStore();
const fullName = ref("Unknown user");
const imageData = ref("");

onMounted(() => {
    fullName.value = userStore.authResponse.lastName + ' ' + userStore.authResponse.firstName;

    if (userStore.authResponse.image) {
        imageData.value = 'data:image;base64,' + userStore.authResponse.image;
    }
});

const emit = defineEmits(['barClick'])

const clickOnBar = () => {
    emit('barClick');
}
</script>

<template>
    <header class="header">
        <div class="header-link">
            <div class="header-bar" @click="clickOnBar">
                <font-awesome-icon icon="fa-solid fa-bars" class="icon" />
            </div>
            <span class="header-image" @click="$router.push({ name: 'Home' })">
                <img src="../../../assets/images/logo.ico" alt="logo">
                <h3>Wild forest</h3>
            </span>
        </div>
        <form class="search">
            <WFInput label="City" name="cityName" placeholder="Input your city" autocomplete="street-address" />
            <WFButton icon="magnifying-glass" outlined disabled />
        </form>
        <div class="header-profile">
            <Avatar :image="imageData" size="large" shape="circle"/>
            <h3>{{ fullName }}</h3>
        </div>
    </header>
</template>

<style lang="scss" scoped>
.header {
    position: absolute;
    top: 0;
    background-color: var(--light-gray);
    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: nowrap;
    align-items: center;
    padding: 3px;
    justify-content: space-between;

    &-link {
        display: flex;
        flex-direction: row;
        align-items: center;
    }

    &-bar {
        margin: 15px;
        border-radius: 50%;
        cursor: pointer;
        padding: 5px 7px 4px 7px;

        &:hover {
            background-color: var(--violet);
        }

        .icon {
            color: var(--white);
            height: 30px;
        }
    }

    &-image {
        display: flex;
        flex-direction: row;
        align-items: center;
        color: var(--white);
        margin: 0 25px 0 15px;
        cursor: pointer;

        img {
            margin-right: 10px;
        }
    }

    .search {
        margin-top: 5px;
        display: flex;
        justify-content: space-between;

        @media screen and (max-width: 600px) {
            display: none;
        }
    }

    &-profile {
        display: flex;
        flex-direction: row;
        align-items: center;
        color: var(--white);
        margin-right: 20px;

        @media screen and (max-width: 768px) {
            display: none;
        }
    }
}
</style>