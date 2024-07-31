import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '../views/HomePage.vue';
import LoginPage from '../views/LoginPage.vue';
import Products from '../views/Products.vue';
import Contact from '../views/Contact.vue';

// Mock authentication function
const isAuthenticated = () => {
    // Replace this with your actual authentication logic
    return localStorage.getItem('authToken') !== null;
};
const routes = [
    {
        path: '/',
        name: 'HomePage',
        component: HomePage,
        meta: { requiresAuth: true }
    },
    {
        path: '/Login',
        name: 'LoginPage',
        component: LoginPage,
        meta: { requiresAuth: false }
    },
    {
        path: '/Products',
        name: 'ProductPage',
        component: Products,
        meta: { requiresAuth: true }
    },
    {
        path: '/Contact',
        name: 'ContactPage',
        component: Contact,
        meta: { requiresAuth: true }
    },
    {
        path: '/:pathMatch(.*)*', // Catch-all route
        name: 'NotFound',
        component: () => import('../components/NotFound.vue') // Component hiển thị lỗi 404
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

// Navigation guard
router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuth && !isAuthenticated()) {
        // Redirect to login page if not authenticated
        next({ name: 'LoginPage' });
    } else if (!to.meta.requiresAuth && isAuthenticated() && to.path == '/login') {
        // Redirect to HomePage page if  authenticated
        next({ name: 'HomePage' });
    } else {
        next();
    }
});

export default router;
