<template>
  <v-sheet class="bg-blue-grey-lighten-3 pa-12 background">
    <v-card class="mx-auto px-6 py-8 my-15" max-width="344">
      <v-form v-model="state.form" @submit.prevent="login">
        <v-text-field
          v-model="state.username"
          :readonly="state.loading"
          :rules="[state.rules.required]"
          class="mb-2"
          label="Nome de usuário"
          clearable
        ></v-text-field>

        <v-text-field
          v-model="state.password"
          :append-icon="state.show_icon ? 'mdi-eye' : 'mdi-eye-off'"
          :type="state.show_icon ? 'text' : 'password'"
          :readonly="state.loading"
          :rules="[state.rules.required]"
          @click:append="state.show_icon = !state.show_icon"
          label="Senha"
          placeholder="Digite sua senha"
          clearable
        ></v-text-field>

        <br />

        <v-btn
          :disabled="!state.form"
          :loading="state.loading"
          class="submit-button"
          size="large"
          type="submit"
          variant="elevated"
          block
        >
          Entrar
        </v-btn>
      </v-form>
    </v-card>
  </v-sheet>
  <p class="footer-text">® taskr | beetlebun</p>
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
    .then(() => router.replace({ path: "/home" }))
    .then(() => (state.loading = false));
};
</script>

<style>
.background {
  height: 95vh;
}

.footer-text {
  text-align: center;
  margin-top: 0.6%;
}

.submit-button {
  color: lightgrey;
  background-color: rgba(54, 67, 77, 0.644);
}
</style>
