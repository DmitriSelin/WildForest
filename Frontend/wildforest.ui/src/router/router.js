import {createRouter, createWebHistory} from "vue-router";
import Index from "@/views/Index.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Main",
            component: Index
        }
    ]
});

export default router;