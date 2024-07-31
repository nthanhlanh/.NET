<template>
  <MDBNavbar expand="lg" light bgColor="light">
    <MDBContainer fluid>
      <MDBNavbarToggler
        v-bind:target="'#navbarSupportedContent'"
        v-bind:aria-controls="'navbarSupportedContent'"
        v-bind:aria-expanded="false"
        v-bind:aria-label="'Toggle navigation'"
      >
        <MDBIcon fas icon="bars" />
      </MDBNavbarToggler>

      <MDBCollapse navbar id="navbarSupportedContent">
        <MDBNavbarBrand href="#">
          <img
            src="https://mdbcdn.b-cdn.net/img/logo/mdb-transaprent-noshadows.webp"
            height="15"
            alt="MDB Logo"
            loading="lazy"
          />
        </MDBNavbarBrand>
        <MDBNavbarNav left class="me-auto mb-2 mb-lg-0">
          <MDBNavbarItem href="/"> Dashboard </MDBNavbarItem>
          <MDBNavbarItem href="/Conntacts"> Contacts </MDBNavbarItem>
          <MDBNavbarItem href="/Products"> Products </MDBNavbarItem>
          <MDBNavbarItem href="/login"> Login </MDBNavbarItem>
        </MDBNavbarNav>
      </MDBCollapse>

      <div class="d-flex align-items-center">
        <MDBIcon fas icon="shopping-cart" class="text-reset me-3" />
        <MDBDropdown v-model="dropdownNotifications">
          <MDBDropdownToggle
            tag="a"
            class="text-reset me-3 hidden-arrow"
            aria-expanded="false"
            @click="dropdownNotifications = !dropdownNotifications"
          >
            <MDBIcon fas icon="bell" />
            <MDBBadge color="danger" class="rounded-pill">1</MDBBadge>
          </MDBDropdownToggle>
          <MDBDropdownMenu right aria-labelledby="navbarDropdownMenuLink">
            <MDBDropdownItem href="#">Some news</MDBDropdownItem>
            <MDBDropdownItem href="#">Another news</MDBDropdownItem>
            <MDBDropdownItem href="#">Something else here</MDBDropdownItem>
          </MDBDropdownMenu>
        </MDBDropdown>
        <MDBDropdown v-model="dropdownAvatars">
          <MDBDropdownToggle
            tag="a"
            class="dropdown-toggle d-flex align-items-center hidden-arrow"
            aria-expanded="false"
            @click="dropdownAvatars = !dropdownAvatars"
          >
            <img
              src="https://mdbcdn.b-cdn.net/img/new/avatars/2.webp"
              class="rounded-circle"
              height="25"
              alt="Black and White Portrait of a Man"
              loading="lazy"
            />
          </MDBDropdownToggle>
          <MDBDropdownMenu right aria-labelledby="navbarDropdownMenuAvatar">
            <MDBDropdownItem href="#">My profile</MDBDropdownItem>
            <MDBDropdownItem href="#">Settings</MDBDropdownItem>
            <MDBDropdownItem href="#" @click="logout">Logout</MDBDropdownItem>
          </MDBDropdownMenu>
        </MDBDropdown>
      </div>
    </MDBContainer>
  </MDBNavbar>
</template>

<script>
import {
  MDBNavbar,
  MDBContainer,
  MDBNavbarToggler,
  MDBCollapse,
  MDBNavbarBrand,
  MDBNavbarNav,
  MDBNavbarItem,
  MDBIcon,
  MDBDropdown,
  MDBDropdownToggle,
  MDBDropdownMenu,
  MDBDropdownItem,
  MDBBadge,
} from "mdb-vue-ui-kit";
import { ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";

export default {
  name: "NavbarComponent",
  components: {
    MDBNavbar,
    MDBContainer,
    MDBNavbarToggler,
    MDBCollapse,
    MDBNavbarBrand,
    MDBNavbarNav,
    MDBNavbarItem,
    MDBIcon,
    MDBDropdown,
    MDBDropdownToggle,
    MDBDropdownMenu,
    MDBDropdownItem,
    MDBBadge,
  },
  setup() {
    const router = useRouter();
    const store = useStore();
    const dropdownAvatars = ref(false);
    const dropdownNotifications = ref(false);
    const logout = async () => {
      try {
        // Gọi API logout 
        await store.dispatch("logout");

        // Redirect đến trang login sau khi logout thành công
        router.push('/login');
      } catch (error) {
        console.error('logout failed:', error);
        // Hiển thị thông báo lỗi hoặc xử lý lỗi
      }
    };
    return {
      dropdownAvatars,
      dropdownNotifications,
      logout,
    };
  },
};
</script>

<style scoped>
/* Add any custom styles here */
</style>
