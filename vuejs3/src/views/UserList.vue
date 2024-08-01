<template>
  <div class="user-list-container">
    <div class="header">
      <h1>User List</h1>
      <MDBBtn color="primary" @click="toggleAddUserForm">
        {{ showAddUserForm ? "Back to List" : "Add New User" }}
      </MDBBtn>
    </div>
    <div v-if="showAddUserForm">
      <AddUserForm @user-added="handleUserAdded" />
    </div>
    <div v-if="!showAddUserForm">
      <MDBTable>
        <thead>
          <tr>
            <th scope="col">#</th>
            <th scope="col">UserName</th>
            <th scope="col">Email</th>
            <th scope="col">Phone Number</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(user, index) in users" :key="user.id">
            <td>{{ currentPage + index }}</td>
            <td>{{ user.userName }}</td>
            <td>{{ user.email }}</td>
            <td>{{ user.phoneNumber || "N/A" }}</td>
            <td>
              <MDBBtn size="sm" color="warning" @click="editUser(user)"
                >Edit</MDBBtn
              >
              <MDBBtn size="sm" color="danger" @click="deleteUser(user)"
                >Delete</MDBBtn
              >
            </td>
          </tr>
        </tbody>
      </MDBTable>
      <Pagination
        :currentPage="currentPage"
        :totalPages="totalPages"
        @page-changed="changePage"
      />
    </div>
  </div>
</template>

<script>
import { ref, onMounted, computed } from "vue";
import { MDBBtn, MDBTable } from "mdb-vue-ui-kit";
import userApi from "@/api/userApi";
import Pagination from "@/components/Pagination.vue";

export default {
  name: "UserList",
  components: {
    MDBBtn,
    MDBTable,
    Pagination,
  },
  setup() {
    const defaultPage = parseInt(process.env.VUE_APP_DEFAULT_PAGE, 10) || 1;
    const defaultPageSize = parseInt(process.env.VUE_APP_PAGE_SIZE, 10) || 10;

    const users = ref([]);
    const currentPage = ref(defaultPage);
    const pageSize = ref(defaultPageSize);
    const totalCount = ref(0);
    const showAddUserForm = ref(false);

    const fetchUsers = async (page, size) => {
      try {
        const data = await userApi.fetchUsers(page, size);
        users.value = data.users;
        totalCount.value = data.totalCount;
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    };

    const toggleAddUserForm = () => {
      showAddUserForm.value = !showAddUserForm.value;
      if (!showAddUserForm.value) {
        fetchUsers(currentPage.value, pageSize.value);
      }
    };

    const addUser = () => {
      // Logic thêm người dùng mới
      console.log("Add new user");
    };

    const editUser = (user) => {
      // Logic chỉnh sửa người dùng
      console.log("Edit user", user);
    };

    const deleteUser = (user) => {
      // Logic xóa người dùng
      console.log("Delete user", user);
    };

    const changePage = (page) => {
      if (page > 0 && page <= totalPages.value) {
        currentPage.value = page;
        fetchUsers(currentPage.value, pageSize.value);
      }
    };

    onMounted(() => {
      fetchUsers(currentPage.value, pageSize.value);
    });

    const totalPages = computed(() => {
      const total = totalCount.value;
      const size = pageSize.value;
      if (isNaN(total) || isNaN(size) || size === 0) {
        return 1;
      }
      return Math.ceil(total / size);
    });

    return {
      users,
      currentPage,
      pageSize,
      totalPages,
      showAddUserForm,
      toggleAddUserForm,
      addUser,
      editUser,
      deleteUser,
      changePage,
    };
  },
};
</script>

<style scoped>
.user-list-container {
  max-width: 100%;
  margin: 0 auto;
  padding: 20px;
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}
</style>
