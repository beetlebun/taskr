<template>
  <v-card class="mx-auto background" color="grey-lighten-3">
    <v-layout>
      <v-app-bar
        color="teal-darken-4"
        image="https://picsum.photos/1920/1080?random"
      >
        <template v-slot:image>
          <v-img
            gradient="to top right, rgba(19,84,122,.8), rgba(128,208,199,.8)"
          ></v-img>
        </template>

        <template v-slot:prepend>
          <v-app-bar-nav-icon></v-app-bar-nav-icon>
        </template>

        <v-app-bar-title>taskr</v-app-bar-title>

        <v-spacer></v-spacer>

        <div class="logout-button">
          <v-tooltip text="Sair" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn v-bind="props" icon @click="logout">
                <v-icon>mdi-logout</v-icon>
              </v-btn>
            </template>
          </v-tooltip>
        </div>
      </v-app-bar>

      <v-main class="page-content">
        <v-container fluid class="page-content__container">
          <v-row dense>
            <v-col
              v-for="project in state.projects"
              :key="project.id"
              cols="12"
            >
              <v-card
                class="project-card"
                :subtitle="project.id"
                :title="project.name"
                max-width="448"
                text="Projecto descrip"
              ></v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-main>
    </v-layout>
  </v-card>
  <p class="footer-text">® taskr | beetlebun</p>
</template>

<script setup lang="ts">
import { onMounted, reactive } from "vue";
import { useRouter } from "vue-router";
import { getAll } from "@/services/project_service";
import { logout as logout_service } from "@/services/auth_service";

const router: any = useRouter();

const state: any = reactive({
  projects: [],
});

onMounted(() => getAll().then((result) => (state.projects = result)));

const logout = () =>
  logout_service().then(() => router.replace({ path: "/login" }));
</script>

<style>
.background {
  border-radius: 0px !important;
}

.footer-text {
  font-size: 10px;
  font-family: "Open Sans", sans-serif;
  text-align: center;
  margin-top: 0.6%;
}

.v-overlay__content {
  background-color: rgba(0, 0, 0, 0.829) !important;
  color: white !important;
  font-weight: 500 !important;
  font-family: "Trebuchet MS", sans-serif !important;
}

.page-content {
  height: 95vh;
}

.project-card {
  margin: 0 auto;
}

.logout-button {
  margin-right: 5px;
}
</style>
