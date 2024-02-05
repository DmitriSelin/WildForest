<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFButton from '@/components/buttons/WFButton.vue'
import WFComment from "@/components/bl/comments/WFComment.vue";
import { ref } from "vue";

const comment = ref("");
const arr = ref([{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }, { id: 5 }, { id: 6 }, { id: 7 }, { id: 8 }, { id: 9 }, { id: 10 }]);

const sendComment = () => {
    alert("");
}
</script>

<template>
    <main class="main">
        <WFComment :comments="arr" />
        <form @submit.prevent="sendComment" class="form">
            <WFInput label="Comment" name="comment" placeholder="Input your comment" v-model:value="comment"
                minLength="1" maxLength="200"/>
            <WFButton label="Send" icon="paper-plane" />
        </form>
    </main>
</template>

<style lang="scss" scoped>
.main {
    display: flex;
    flex-direction: column;
    align-items: center;
    height: 100%;

    .form {
        display: flex;
        gap: 10px;
    }
}
</style>