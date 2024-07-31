<!-- src/components/Menu.vue -->
<template>
  <v-app>
    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-toolbar-title>My Shop</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn text>Login</v-btn>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" app>
      <v-list>
        <v-list-item v-for="item in items" :key="item.title" :to="item.path">
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>
          <v-list-item-title>{{ item.title }}</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script>
export default {
  data() {
    return {
      drawer: false,
      items: [
        { title: 'Home', icon: 'mdi-home', path: '/' },
        { title: 'Products', icon: 'mdi-cart', path: '/products' },
        { title: 'Contact', icon: 'mdi-phone', path: '/contact' },
      ],
    };
  },
};
</script>

<style>
/* Add your styles here */
</style>
