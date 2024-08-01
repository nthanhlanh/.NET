<template>
  <div class="not-found-container">
    <div class="not-found-content">
      <img src="@/assets/404-error.png" alt="404 Error" class="not-found-image" />
      <h1>404 - Page Not Found</h1>
      <p>The page you are looking for does not exist.</p>
      <router-link to="/" class="home-link">Go back to home</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: 'NotFoundPage',
};
</script>

<style scoped>
.not-found-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #f8f9fa;
  text-align: center;
}

.not-found-content {
  max-width: 600px;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.not-found-image {
  width: 100%;
  max-width: 300px;
  margin-bottom: 20px;
}

h1 {
  font-size: 2em;
  margin-bottom: 20px;
  color: #343a40;
}

p {
  font-size: 1.2em;
  margin-bottom: 20px;
  color: #6c757d;
}

.home-link {
  display: inline-block;
  padding: 10px 20px;
  background-color: #007bff;
  color: #fff;
  border-radius: 5px;
  text-decoration: none;
  transition: background-color 0.3s ease;
}

.home-link:hover {
  background-color: #0056b3;
}
</style>
