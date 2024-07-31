<template>
  <div class="login-background">
    <div class="login-container">
      <form @submit.prevent="login">
        <!-- Email input -->
        <MDBInput
          type="email"
          label="Email address"
          id="email"
          v-model="email"
          wrapperClass="mb-4"
        />
        <!-- Password input -->
        <MDBInput
          type="password"
          label="Password"
          id="password"
          v-model="password"
          wrapperClass="mb-4"
        />
        <!-- 2 column grid layout for inline styling -->
        <MDBRow class="mb-4">
          <MDBCol class="d-flex justify-content-center">
            <!-- Checkbox -->
            <MDBCheckbox
              label="Remember me"
              id="rememberMe"
              v-model="rememberMe"
              wrapperClass="mb-3 mb-md-0"
            />
          </MDBCol>
          <MDBCol>
            <!-- Simple link -->
            <a href="#!">Forgot password?</a>
          </MDBCol>
        </MDBRow>
        <!-- Submit button -->
        <MDBBtn type="submit" color="primary" block>Sign in</MDBBtn>

        <!-- Register buttons -->
        <div class="text-center">
          <p>Not a member? <a href="#!">Register</a></p>
          <p>or sign up with:</p>
          <MDBBtn color="secondary" floating class="mx-1">
            <MDBIcon iconStyle="fab" icon="facebook-f"/>
          </MDBBtn>
          <MDBBtn color="secondary" floating class="mx-1">
            <MDBIcon iconStyle="fab" icon="google"/>
          </MDBBtn>
          <MDBBtn color="secondary" floating class="mx-1">
            <MDBIcon iconStyle="fab" icon="twitter"/>
          </MDBBtn>
          <MDBBtn color="secondary" floating class="mx-1">
            <MDBIcon iconStyle="fab" icon="github"/>
          </MDBBtn>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useStore } from 'vuex';
  import {
    MDBRow,
    MDBCol,
    MDBInput,
    MDBCheckbox,
    MDBBtn,
    MDBIcon
  } from "mdb-vue-ui-kit";

export default {
  name: "LoginPage",
  components: {
      MDBRow,
      MDBCol,
      MDBInput,
      MDBCheckbox,
      MDBBtn,
      MDBIcon
    },
  setup() {
    const email = ref('');
    const password = ref('');
    const rememberMe = ref(false);
    const router = useRouter();
    const store = useStore();

    const login = async () => {
      try {
        // Gọi API đăng nhập với định dạng dữ liệu yêu cầu
        await store.dispatch("login", {
          Username: email.value,
          Password: password.value,
          RememberMe: rememberMe.value ,
        });

        // Redirect đến trang chính sau khi đăng nhập thành công
        router.push('/');
      } catch (error) {
        console.error('Login failed:', error);
        // Hiển thị thông báo lỗi hoặc xử lý lỗi
      }
    };

    return {
      email,
      password,
      rememberMe,
      login,
    };
  },
};
</script>

<style scoped>
/* Toàn bộ trang */
.login-background {
  background: linear-gradient(to right, #e4e9eb, #d4d7db);
  background-size: cover;
  background-position: center;
  height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  position: relative;
  overflow: hidden;
}

.login-background::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.3); /* Semi-transparent overlay */
  z-index: -1;
}

.login-container {
  background: rgba(
    255,
    255,
    255,
    0.9
  ); /* Semi-transparent background for form */
  border-radius: 12px;
  padding: 2rem;
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
  max-width: 400px;
  width: 100%;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  position: relative;
  z-index: 1;
}

.login-container:hover {
  transform: translateY(-10px);
  box-shadow: 0 16px 32px rgba(0, 0, 0, 0.3);
}

.form-control {
  border-radius: 8px;
}

.btn-primary {
  background-color: #0072ff;
  border: none;
}

.btn-primary:hover {
  background-color: #005bb5;
}
</style>

