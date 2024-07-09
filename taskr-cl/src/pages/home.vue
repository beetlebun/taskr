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
          <v-tooltip text="Apontamentos" location="bottom">
            <template v-slot:activator="{ props }">
              <v-btn v-bind="props" icon="mdi-alarm-check" @click="changeRoute('tasks')"></v-btn>
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

      <v-main class="page-content">
        <v-container fluid class="page-content__container">
          <div class="page-content__container_title">
            <h1 class="title-text">Gerenciador de projetos</h1>
            <v-btn
              prepend-icon="mdi-plus"
              variant="outlined"
              @click="setDialogState('add', true)"
              >Adicionar</v-btn
            >
          </div>
          <div class="card-layout">
            <div
              v-for="(project, index) in state.filteredProjects()"
              :key="project.id"
            >
              <v-card
                class="project-card"
                max-width="250"
                variant="outlined"
                @click="loadProject(project.id, project.name)"
              >
                <img
                  :src="getBackgroundImgCards(index)"
                  height="180"
                  width="250"
                />
                <div class="project-card__text">
                  <v-card-title>{{ project.name }}</v-card-title>
                  <v-card-subtitle>{{ project.id }}</v-card-subtitle>
                </div>
              </v-card>
            </div>
          </div>
        </v-container>
        <div class="text-center pagination-buttons" v-if="!emptyProjects()">
          <v-container>
            <v-row justify="center">
              <v-col cols="8">
                <v-container class="max-width">
                  <v-pagination
                    v-model="state.pagination.page"
                    :length="state.pagination.size"
                  ></v-pagination>
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </div>
      </v-main>
    </v-layout>
  </v-card>
  <Footer />
  <div class="text-center">
    <v-dialog v-model="state.dialogs.edit.open" max-width="400" persistent>
      <v-card
        prepend-icon="mdi-file-edit"
        text="Alterações no projeto serão aplicadas para todos as tarefas que forem associadas."
        title="Editar projeto"
      >
        <template v-slot:append>
          <v-btn
            icon="mdi-delete-outline"
            variant="text"
            @click="removeProject()"
          ></v-btn>
        </template>

        <div class="px-6 pt-2">
          <v-text-field
            v-model="state.dialogs.edit.project"
            :rules="[state.rules.required]"
            class="mb-2"
            label="Nome"
          ></v-text-field>

          <v-combobox
            v-if="!emptyTasks()"
            class="limited-size"
            v-model="state.tasks"
            label="Tarefas"
            chips
            multiple
            readonly
          >
          </v-combobox>
        </div>

        <template v-slot:actions>
          <v-btn class="ml-4" @click="setDialogState('edit', false)"
            >FECHAR</v-btn
          >
          <v-spacer></v-spacer>
          <v-btn
            class="mr-4"
            @click="editProject()"
            :disabled="disabledDialogSaveButton('edit')"
            >SALVAR</v-btn
          >
        </template>
      </v-card>
    </v-dialog>
  </div>
  <div class="text-center">
    <v-dialog v-model="state.dialogs.add.open" max-width="400" persistent>
      <v-card
        prepend-icon="mdi-file-plus"
        text="Projetos são listados na tela de gerenciamento de projetos, e quando tarefas são criadas, podem se associar."
        title="Adicionar projeto"
      >
        <div class="px-6 pt-2">
          <v-text-field
            v-model="state.dialogs.add.project"
            :rules="[state.rules.required]"
            class="mb-2"
            label="Nome"
          ></v-text-field>
        </div>

        <template v-slot:actions>
          <v-btn
            class="ml-4"
            @click="
              setDialogState('add', false);
              clearProject('add');
            "
            >FECHAR</v-btn
          >
          <v-spacer></v-spacer>
          <v-btn
            class="mr-4"
            @click="addProject()"
            :disabled="disabledDialogSaveButton('add')"
            >SALVAR</v-btn
          >
        </template>
      </v-card>
    </v-dialog>
  </div>
</template>

<script setup lang="ts">
import { onMounted, reactive } from "vue";
import { useRouter } from "vue-router";
import {
  getAll as getAllProjects,
  post as postProject,
  put as putProject,
  remove as deleteProject,
} from "@/services/project_service";
import { getAll as getAllTasks } from "@/services/task_service";
import { logout as logoutService } from "@/services/auth_service";

const router: any = useRouter();

const state: any = reactive({
  projects: [],
  filteredProjects: () => {
    var end: number = state.pagination.page * 5;
    var begin: number = end - 5;

    return state.projects.slice(begin, end);
  },
  projectId: "",
  tasks: [],
  dialogs: {
    edit: {
      open: false,
      project: "",
    },
    add: {
      open: false,
      project: "",
    },
  },
  pagination: {
    page: 1,
    size: 1,
  },
  rules: {
    required: (value: any) => !!value || "Obrigatório",
  },
});

onMounted(() =>
  getAllProjects().then((result) => {
    state.projects = result;
    state.pagination.size = Math.ceil(result.length / 5);
  })
);

const getBackgroundImg = () => "https://picsum.photos/1920/1080?random=2";

const getBackgroundImgCards = (index: number) =>
  `https://picsum.photos/1920/1080?random=${
    (index + 1) * state.pagination.page * 2
  }`;

const emptyTasks = () => state.tasks.length === 0;

const emptyProjects = () => state.projects.length === 0;

const loadTasks = (id: string) =>
  getAllTasks().then((result) => {
    result.forEach((element: any) => {
      if (element.projectId == id) state.tasks.push(element.name);
    });
  });

const loadProject = (id: string, name: string) => {
  state.tasks = [];
  state.projectId = id;
  setDialogProject(name);
  loadTasks(id);
};

const addProject = () => {
  postProject(state.dialogs.add.project)
    .then(() => setDialogState("add", false))
    .then(() => location.reload());
};

const editProject = () => {
  putProject(state.projectId, state.dialogs.edit.project)
    .then(() => setDialogState("edit", false))
    .then(() => location.reload());
};

const removeProject = () => {
  deleteProject(state.projectId)
    .then(() => setDialogState("edit", false))
    .then(() => location.reload());
};

const setDialogState = (dialog: string, value: boolean) => {
  state.dialogs[dialog].open = value;
};

const clearProject = (dialog: string) => (state.dialogs[dialog].project = "");

const disabledDialogSaveButton = (dialog: string) =>
  state.dialogs[dialog].project == "" ||
  state.dialogs[dialog].project == null ||
  state.dialogs[dialog].project == undefined;

const setDialogProject = (name: string) => {
  state.dialogs.edit.project = name;
  setDialogState("edit", true);
};

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
