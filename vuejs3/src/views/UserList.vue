<template>
  <div class="user-list-container">
    <div class="header">
      <h1>User List</h1>
      <MDBBtn color="primary" @click="addUser">Add New User</MDBBtn>
    </div>
    <MDBTable>
      <MDBTableHead>
        <tr>
          <th>#</th>
          <th>Name</th>
          <th>Email</th>
          <th>Role</th>
          <th>Actions</th>
        </tr>
      </MDBTableHead>
      <MDBTableBody>
        <tr v-for="(user, index) in users" :key="user.id">
          <td>{{ index + 1 }}</td>
          <td>{{ user.name }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.role }}</td>
          <td>
            <MDBBtn size="sm" color="warning" @click="editUser(user)">Edit</MDBBtn>
            <MDBBtn size="sm" color="danger" @click="deleteUser(user)">Delete</MDBBtn>
          </td>
        </tr>
      </MDBTableBody>
    </MDBTable>
  </div>
</template>

<script>
import { ref } from 'vue';
import { MDBBtn, MDBTable, MDBTableHead, MDBTableBody } from 'mdb-vue-ui-kit';

export default {
  name: 'UserList',
  components: {
    MDBBtn,
    MDBTable,
    MDBTableHead,
    MDBTableBody,
  },
  setup() {
    const users = ref([
      { id: 1, name: 'John Doe', email: 'john@example.com', role: 'Admin' },
      { id: 2, name: 'Jane Doe', email: 'jane@example.com', role: 'User' },
      // Thêm người dùng khác ở đây
    ]);

    const addUser = () => {
      // Logic thêm người dùng mới
      console.log('Add new user');
    };

    const editUser = (user) => {
      // Logic chỉnh sửa người dùng
      console.log('Edit user', user);
    };

    const deleteUser = (user) => {
      // Logic xóa người dùng
      console.log('Delete user', user);
    };

    return {
      users,
      addUser,
      editUser,
      deleteUser,
    };
  },
};
</script>

<style scoped>
.user-list-container {
  max-width: 800px;
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
