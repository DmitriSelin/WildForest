<script setup>
import WFInput from "@/components/inputs/WFInput.vue";
import WFButton from '@/components/buttons/WFButton.vue'
import WFComment from "@/components/bl/comments/WFComment.vue";
import { useToast } from "primevue/usetoast";
import { ERROR_SEVERITY, STANDARD_LIFE } from "@/infrastructure/components/toasts/toastConstants";
import { CommentService } from "@/comments/commentService";
import { ref } from "vue";

const toast = useToast();
const comment = ref("");
const commentService = new CommentService("/chatHub", faileCommentsLoad);

const sendComment = () => {
    alert("");
}

function faileCommentsLoad() {
    toast.add({ severity: ERROR_SEVERITY, summary: 'Error', detail: "Comments cannot be uploaded", life: STANDARD_LIFE });
}
</script>

<template>
    <main class="main">
        <WFComment
            :comments="[{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }, { id: 5 }, { id: 6 }, { id: 7 }, { id: 8 }, { id: 9 }, { id: 10 }]" />
        <form @submit.prevent="sendComment" class="form">
            <WFInput label="Comment" name="comment" placeholder="Input your comment" v-model:value="comment" minLength="1"
                maxLength="200" />
            <WFButton label="Send" icon="paper-plane" />
        </form>
        <Toast />
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