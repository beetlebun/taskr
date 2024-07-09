<template>
  <v-sheet class="bg-blue-grey-lighten-3 pa-12 background">
    <v-card class="mx-auto px-6 py-8 my-15" max-width="344">
      <v-form v-model="state.form" @submit.prevent="login">
        <v-text-field
          variant="outlined"
          density="compact"
          prepend-inner-icon="mdi-shield-account"
          v-model="state.username"
          :readonly="state.loading"
          :rules="[state.rules.required]"
          class="mb-4"
          label="Nome de usuário"
          clearable
        ></v-text-field>

        <v-text-field
          variant="outlined"
          density="compact"
          prepend-inner-icon="mdi-lock-outline"
          v-model="state.password"
          :append-inner-icon="state.show_icon ? 'mdi-eye' : 'mdi-eye-off'"
          :type="state.show_icon ? 'text' : 'password'"
          :readonly="state.loading"
          :rules="[state.rules.required]"
          @click:append-inner="state.show_icon = !state.show_icon"
          label="Senha"
          placeholder="Digite sua senha"
        ></v-text-field>

        <v-btn
          :disabled="!state.form"
          :loading="state.loading"
          class="submit-button mt-4"
          type="submit"
          variant="elevated"
          block
        >
          Entrar
        </v-btn>
      </v-form>
    </v-card>
  </v-sheet>
  <Footer />
</template>

<script setup lang="ts">
import { reactive } from "vue";
import { useRouter } from "vue-router";
import { login as login_service } from "@/services/auth_service";

const router: any = useRouter();

const state: any = reactive({
  show_icon: false,
  loading: false,
  form: false,
  username: "",
  password: "",
  rules: {
    required: (value: any) => !!value || "Obrigatório",
  },
});

const login = () => {
  if (!state.form) {
    return;
  }

  state.loading = true;

  return login_service(state.username, state.password)
    .then(() => (state.loading = false))
    .then(() => router.replace({ path: "/home" }));
};
</script>

<style>
.background {
  height: 95vh;
}

.submit-button {
  color: lightgrey;
  background-color: rgb(49 59 66 / 55%);
}

/* brigando com o vuetify */
.v-sheet {
  background-image: url("https://picsum.photos/1920/1080?random=1") !important;
}

.v-messages__message {
  font-size: 10.5px !important;
}
/* --- */
</style>
