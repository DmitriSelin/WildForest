import {createRouter, createWebHistory} from "vue-router";
import Index from "@/views/WFIndex.vue";
import Button from "@/views/Button.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Index",
            component: Index
        },
        {
            path: "/buttons",
            name: "Button",
            component: Button
        }
    ]
});

export default router;