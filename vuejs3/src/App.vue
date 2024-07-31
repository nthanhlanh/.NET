<template>
  <div id="app">
    <Navbar v-if="isLoggedIn" />
    <main>
      <router-view />
    </main>
    <Footer v-if="isLoggedIn" />
  </div>
</template>

<script>
import { computed } from 'vue';
import { useStore } from 'vuex';
import Navbar from "./components/Navbar.vue";
import Footer from "./components/Footer.vue";

export default {
  name: "App",
  components: {
    Navbar,
    Footer,
  },
  setup() {
    const store = useStore();
    const isLoggedIn = computed(() => store.state.isLoggedIn);

    return {
      isLoggedIn
    };
  }
};
</script>

<style>
main {
  min-height: calc(
    100vh - 160px
  ); /* Adjust based on header and footer height */
}
</style>
