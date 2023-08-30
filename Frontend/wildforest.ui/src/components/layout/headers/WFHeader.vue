<script setup>
import WFInput from '@/components/inputs/WFInput.vue'
import WFButton from '@/components/buttons/WFButton.vue'
</script>

<template>
    <header class="header">
        <div class="header-link">
            <div class="header-bar" @click="toggleSidebar">
                <font-awesome-icon icon="fa-solid fa-bars" class="icon" />
            </div>
            <span class="header-image">
                <img src="../../../assets/images/logo.ico" alt="logo">
                <h2>Wild forest</h2>
            </span>
        </div>
        <form action="https://localhost:8080" method="get" class="search">
            <WFInput label="City" name="cityName" placeholder="Input your city" />
            <WFButton icon="magnifying-glass" outlined />
        </form>
        <div class="header-profile">
            <img src="../../../assets/images/logo.ico" alt="userImage">
            <h2>Test User</h2>
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

        img {
            margin-right: 10px;
        }
    }
}
</style>