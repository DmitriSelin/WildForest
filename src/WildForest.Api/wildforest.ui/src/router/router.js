import { createRouter, createWebHistory } from "vue-router";
import Start from "@/views/WFStart.vue";
import Login from "@/views/WFLogin.vue";
import AuthCredentials from "@/views/WFAuthCredentials.vue";
import Register from "@/views/WFRegister.vue";
import Index from "@/views/WFIndex.vue";
import Home from "@/views/WFHome.vue";
import Forecast from "@/views/WFForecast.vue";
import Comments from "@/views/WFComments.vue";
import NotFound from "@/views/WFNotFound.vue";
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
            path: "/auth/credentials",
            name: "AuthCredentials",
            component: AuthCredentials
        },
        {
            path: "/auth/register",
            name: "Registration",
            component: Register
        },
        {
            path: "/weather",
            name: "Weather",
            component: Index,
            children: [
                {
                    path: "",
                    name: "Home",
                    component: Home
                },
                {
                    path: "forecast",
                    component: Forecast
                },
                {
                    path: "comments",
                    component: Comments
                }
            ]
        },
        {
            path: "/:pathMatch(.*)*",
            name: "NotFound",
            component: NotFound
        },
        {
            path: "/buttons",
            name: "Button",
            component: Button
        },
    ]
});

export default router;