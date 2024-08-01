<template>
  <div class="pagination-container">
    <nav aria-label="Page navigation example">
      <MDBPagination>
        <MDBPageNav
          prev
          :disabled="currentPage === 1"
          @click="changePage(currentPage - 1)"
        ></MDBPageNav>
        <MDBPageItem
          v-for="page in totalPages"
          :key="page"
          :active="page === currentPage"
          @click="changePage(page)"
        >
          {{ page }}
        </MDBPageItem>
        <MDBPageNav
          next
          :disabled="currentPage === totalPages"
          @click="changePage(currentPage + 1)"
        ></MDBPageNav>
      </MDBPagination>
    </nav>
  </div>
</template>

<script>
import { MDBPagination, MDBPageNav, MDBPageItem } from "mdb-vue-ui-kit";
export default {
  name: "PaginationCompoment",
  components: {
    MDBPagination,
    MDBPageNav,
    MDBPageItem,
  },
  props: {
    currentPage: {
      type: Number,
      required: true,
    },
    totalPages: {
      type: Number,
      required: true,
    },
  },
  emits: ["page-changed"],
  methods: {
    changePage(page) {
      if (page > 0 && page <= this.totalPages) {
        this.$emit("page-changed", page);
      }
    },
  },
};
</script>

<style scoped>
.pagination-container {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}
</style>
