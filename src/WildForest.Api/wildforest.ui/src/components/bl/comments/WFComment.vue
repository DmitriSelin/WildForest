<script setup>
const props = defineProps({
    comments: {
        type: Array,
        required: true
    }
});
</script>

<template>
    <div class="container">
        <div class="comment" v-for="comment in comments" :key="comment.id">
            <div class="credentials">
                <img src="../../../assets/images/logo.ico" alt="userIcon">
                <span>LastName FirstName</span>
            </div>
            <div class="text">
                <span></span>
            </div>
        </div>
    </div>
</template>

<style lang="scss" scoped>
.container {
    display: flex;
    flex-direction: column;
    flex-wrap: nowrap;
    height: 90%;
    overflow: auto;
    gap: 2vh;
    padding: 0 5%;
    margin: 2vh auto;
    width: 100%;
    align-items: center;

    .comment {
        width: 50%;
        display: flex;
        flex-direction: column;
        padding: 10px;
        border-radius: 10px;

        &:hover {
            background-color: var(--second);
        }

        .credentials {
            display: flex;
            align-items: center;
            gap: 2vh;
        }

        .text {
            margin-left: 9%;

            span {
                word-wrap: break-word;
                text-wrap: wrap;
            }
        }
    }
}
</style>