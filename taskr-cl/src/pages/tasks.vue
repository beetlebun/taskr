<template>
  <v-card class="mx-auto no-border-radius" color="grey-lighten-3">
    <v-layout>
      <v-app-bar color="teal-darken-4" :image="getBackgroundImg()">
        <template v-slot:image>
          <v-img
            gradient="to top right, rgba(19,84,122,.8), rgba(128,208,199,.8)"
          ></v-img>
        </template>

        <v-app-bar-title>taskr</v-app-bar-title>

        <v-spacer></v-spacer>

        <div class="nav-buttons">
          <v-tooltip text="Projetos" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn
                v-bind="props"
                icon="mdi-file-table-box-multiple-outline"
                @click="changeRoute('home')"
              ></v-btn>
            </template>
          </v-tooltip>
          <v-tooltip text="Sair" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn v-bind="props" icon @click="logout()">
                <v-icon>mdi-logout</v-icon>
              </v-btn>
            </template>
          </v-tooltip>
        </div>
      </v-app-bar>

      <v-main class="page-content"> </v-main>
    </v-layout>
  </v-card>
  <Footer />
</template>

<script setup lang="ts">
import { onMounted, reactive } from "vue";
import { useRouter } from "vue-router";
import { getAll as getAllTasks } from "@/services/task_service";
import { logout as logoutService } from "@/services/auth_service";

const router: any = useRouter();

const state: any = reactive({ tasks: [] });

onMounted(() =>
  getAllTasks().then((result) => {
    state.tasks = result;
    state.pagination.size = Math.ceil(result.length / 5);
  })
);

const getBackgroundImg = () => "https://picsum.photos/1920/1080?random=2";

// const emptyTasks = () => state.tasks.length === 0;

const logout = () =>
  logoutService().then(() => router.replace({ path: "/login" }));

const changeRoute = (route: string) => router.replace({ path: `/${route}` });
</script>

<style>
@font-face {
  font-family: "San Francisco";
  font-weight: 400;
  src: url("https://applesocial.s3.amazonaws.com/assets/styles/fonts/sanfrancisco/sanfranciscodisplay-regular-webfont.woff");
}

.page-content {
  height: 95vh;
  background-color: #04181eed;
  padding-bottom: 0px;
}

.page-content__container_title {
  padding-left: 8px;
  padding-bottom: 5px;
  padding-top: 10px;
  display: flex;
  justify-content: space-between;
  width: 32%;
  color: #2a8ba9ed;
}

.title-text {
  font-size: 24px;
  font-family: "San Francisco";
}

.project-card {
  margin: 5px;
  padding-right: 15px;
  border-radius: 0;
  color: rgb(8 24 26);
}

.project-card:hover {
  color: rgba(177, 207, 224, 0.849) !important;
  background-color: rgb(22, 22, 22) !important;
}

.project-card__text {
  padding-bottom: 10px;
}

.nav-buttons {
  margin-right: 5px;
}

.buttons {
  display: flex;
  justify-content: end;
}

.card-layout {
  display: flex;
  border-radius: 0;
  padding-left: 6px;
  padding-top: 25px;
}

.pagination-buttons {
  color: #2a8ba9ed;
}

.limited-size {
  max-height: 150px;
  overflow: auto;
}

/* brigando com o vuetify */
.no-border-radius {
  border-radius: 0px !important;
}

.v-overlay__content {
  background-color: rgba(0, 0, 0, 0.829) !important;
  color: rgb(230, 230, 230) !important;
  font-weight: 500 !important;
  font-family: "Trebuchet MS", sans-serif !important;
}

.v-card--variant-outlined {
  border: 0 !important;
  background-color: #1a404285 !important;
  color: #6ab6c7a1 !important;
}

.v-container {
  padding-bottom: 0px !important;
}
/* --- */
</style>
