<template>
  <div class="login-background">
    <div class="login-container">
      <form @submit.prevent="login">
        <!-- Email input -->
        <div class="form-outline mb-4">
          <input
            type="email"
            id="form2Example1"
            class="form-control"
            v-model="email"
          />
          <label class="form-label" for="form2Example1">Email address</label>
        </div>

        <!-- Password input -->
        <div class="form-outline mb-4">
          <input
            type="password"
            id="form2Example2"
            class="form-control"
            v-model="password"
          />
          <label class="form-label" for="form2Example2">Password</label>
        </div>

        <!-- 2 column grid layout for inline styling -->
        <div class="row mb-4">
          <div class="col d-flex justify-content-center">
            <!-- Checkbox -->
            <div class="form-check">
              <input
                class="form-check-input"
                type="checkbox"
                value=""
                id="form2Example34"
                v-model="rememberMe"
              />
              <label class="form-check-label" for="form2Example34">
                Remember me
              </label>
            </div>
          </div>

          <div class="col">
            <!-- Simple link -->
            <a href="#!">Forgot password?</a>
          </div>
        </div>

        <!-- Submit button -->
        <button type="submit" class="btn btn-primary btn-block mb-4">
          Sign in
        </button>

        <!-- Register buttons -->
        <div class="text-center">
          <p>Not a member? <a href="#!">Register</a></p>
          <p>or sign up with:</p>
          <button type="button" class="btn btn-secondary btn-floating mx-1">
            <i class="fab fa-facebook-f"></i>
          </button>
          <button type="button" class="btn btn-secondary btn-floating mx-1">
            <i class="fab fa-google"></i>
          </button>
          <button type="button" class="btn btn-secondary btn-floating mx-1">
            <i class="fab fa-twitter"></i>
          </button>
          <button type="button" class="btn btn-secondary btn-floating mx-1">
            <i class="fab fa-github"></i>
          </button>
        </div>
      </form>
    </div>
  </div>
</template>


<script>
// import { useStore } from 'vuex';

export default {
  name: "LoginPage",
  methods: {
    async login() {
      try {
        await this.$store.dispatch("login", {
          Username: this.email,
          Password: this.password,
          RememberMe: this.rememberMe || false,
        });
        this.$router.push("/"); // Redirect to home or other authenticated page
      } catch (error) {
        console.error("Login failed:", error);
        // Handle error and show message if necessary
      }
    },
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

