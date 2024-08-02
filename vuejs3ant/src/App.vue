<template>
  <a-layout>
    <a-layout-header class="header">
      <div class="logo">
        <HomeOutlined />
        My App
      </div>

      <a-menu
        v-model:selectedKeys="selectedKeys1"
        theme="light"
        mode="horizontal"
        :style="{ lineHeight: '64px', width: '300px' }"
      >
        <a-menu-item key="1" @click="handleNavClick(1)"
          >nav 1nav 1nav 1nna nav 1nav
        </a-menu-item>
        <a-menu-item key="2" @click="handleNavClick(2)"
          >nav 2nav 2nav 2nav 2</a-menu-item
        >
        <a-menu-item key="3" @click="handleNavClick(3)">nav 3</a-menu-item>
      </a-menu>
      <div class="menu-right">
        <a-badge count="5" class="menu-icon">
          <BellOutlined />
        </a-badge>
        <a-avatar icon="user" class="menu-icon" />
      </div>
    </a-layout-header>
    <a-layout>
      <a-layout-sider width="200" style="background: #fff">
        <a-menu
          v-model:selectedKeys="selectedKeys2"
          v-model:openKeys="openKeys"
          mode="inline"
          :style="{ height: '100%', borderRight: 0 }"
        >
          <a-sub-menu key="sub1">
            <template #title>
              <span>
                <user-outlined />
                subnav 1
              </span>
            </template>
            <a-sub-menu key="1-1">
              <template #title>
                <span>
                  <notification-outlined />
                  option1
                </span>
              </template>
              <a-menu-item key="1-1-1" @click="handleSubNavClick(1)"
                >sub option 1</a-menu-item
              >
              <a-menu-item key="1-1-2" @click="handleSubNavClick(1)"
                >sub option 2</a-menu-item
              >
            </a-sub-menu>

            <a-menu-item key="1-2" @click="handleSubNavClick(1)"
              >option2</a-menu-item
            >
            <a-menu-item key="1-3" @click="handleSubNavClick(1)"
              >option3</a-menu-item
            >
            <a-menu-item key="1-4" @click="handleSubNavClick(1)"
              >option4</a-menu-item
            >
          </a-sub-menu>
          <a-sub-menu key="sub2">
            <template #title>
              <span>
                <laptop-outlined />
                subnav 2
              </span>
            </template>
            <a-menu-item key="2-1" @click="handleSubNavClick(2)"
              >option5</a-menu-item
            >
            <a-menu-item key="2-2" @click="handleSubNavClick(2)"
              >option6</a-menu-item
            >
            <a-menu-item key="2-3" @click="handleSubNavClick(2)"
              >option7</a-menu-item
            >
            <a-menu-item key="2-4" @click="handleSubNavClick(2)"
              >option8</a-menu-item
            >
          </a-sub-menu>
          <a-sub-menu key="sub3">
            <template #title>
              <span>
                <notification-outlined />
                subnav 3
              </span>
            </template>
            <a-menu-item key="3-1" @click="handleSubNavClick(3)"
              >option9</a-menu-item
            >
            <a-menu-item key="3-2" @click="handleSubNavClick(3)"
              >option10</a-menu-item
            >
            <a-menu-item key="3-3" @click="handleSubNavClick(3)"
              >option11</a-menu-item
            >
            <a-menu-item key="3-4" @click="handleSubNavClick(3)"
              >option12</a-menu-item
            >
          </a-sub-menu>
        </a-menu>
      </a-layout-sider>
      <a-layout style="padding: 0 24px 24px">
        <a-breadcrumb style="margin: 16px 0">
          <a-breadcrumb-item>Home</a-breadcrumb-item>
          <a-breadcrumb-item>List</a-breadcrumb-item>
          <a-breadcrumb-item>App</a-breadcrumb-item>
        </a-breadcrumb>
        <a-layout-content
          :style="{
            background: '#fff',
            padding: '24px',
            margin: 0,
            minHeight: '280px',
          }"
        >
          Content
        </a-layout-content>
      </a-layout>
    </a-layout>
  </a-layout>
</template>

<script>
import { ref } from "vue";
import {
  UserOutlined,
  LaptopOutlined,
  NotificationOutlined,
  HomeOutlined,
  BellOutlined,
} from "@ant-design/icons-vue";
import { Avatar, Badge, Menu } from "ant-design-vue";

export default {
  name: "App",
  components: {
    UserOutlined,
    LaptopOutlined,
    NotificationOutlined,
    HomeOutlined,
    BellOutlined,
    "a-avatar": Avatar,
    "a-badge": Badge,
    "a-menu-item": Menu.Item,
    "a-sub-menu": Menu.SubMenu,
  },
  setup() {
    const selectedKeys1 = ref(["1"]);
    const selectedKeys2 = ref(["1-1"]);
    const openKeys = ref(["sub1"]);

    const handleNavClick = (key) => {
      if (key === 1) {
        openKeys.value = ["sub1"];
        selectedKeys2.value = ["1-1"];
      } else if (key === 2) {
        openKeys.value = ["sub2"];
        selectedKeys2.value = ["2-1"];
      } else if (key === 3) {
        openKeys.value = ["sub3"];
        selectedKeys2.value = ["3-1"];
      } else {
        openKeys.value = [];
        selectedKeys2.value = [];
      }
    };

    const handleSubNavClick = (key) => {
      selectedKeys1.value = [`${key}`];
      handleNavClick(key);
    };

    return {
      selectedKeys1,
      selectedKeys2,
      openKeys,
      handleNavClick,
      handleSubNavClick,
    };
  },
};
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #f0f0f0; /* Màu nền của header */
  padding: 20px;
}

.logo {
  display: flex;
  align-items: center;
  font-size: 1.2em;
  font-weight: bold;
  width: 190px;
}

.home-icon {
  font-size: 1.5em;
  margin-right: 8px;
}

.menu-right {
  display: flex;
  align-items: center;
  margin-left: auto;
}

.menu-icon {
  margin-left: 20px;
  font-size: 20px;
}

.ant-menu-light {
  background: #f0f0f0; /* Màu nền của menu */
  color: #000;
}

.ant-menu-light .ant-menu-item {
  color: #000;
}

.ant-menu-light .ant-menu-item:hover {
  color: #1890ff;
}
</style>
