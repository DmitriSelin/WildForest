<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFButton from '@/components/buttons/WFButton.vue'
import { ref } from "vue";

const comment = ref("");

const sendComment = () => {
    alert("");
}
</script>

<template>
    <main class="main">
        <div class="comments">

        </div>
        <form @submit.prevent="sendComment" class="form">
            <WFInput label="Comment" name="comment" placeholder="Input your comment" v-model:value="comment"
                autocomplete="off" />
            <WFButton label="Send" />
        </form>
    </main>
</template>

<style lang="scss" scoped>
.main {
    display: flex;
    flex-direction: column;
    align-items: center;
    height: 100%;

    .comments {
        height: 90%;
    }

    .form {
        display: flex;
        gap: 10px;
    }
}
</style>