import {createRouter, createWebHistory} from "vue-router";
import Start from "@/views/WFStart.vue";
import Login from "@/views/WFLogin.vue";
import Register from "@/views/WFRegister.vue";
import Button from "@/views/Button.vue";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: "/",
            name: "Start",
            component: Start
        },
        {
            path: "/auth/login",
            name: "Login",
            component: Login
        },
        {
            path: "/auth/register",
            name: "Registration",
            component: Register
        },
        {
            path: "/buttons",
            name: "Button",
            component: Button
        },
    ]
});

export default router;