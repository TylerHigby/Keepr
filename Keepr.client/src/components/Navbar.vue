<template>
  <nav class="navbar navbar-expand-lg navbar-dark bg-secondary px-3">
    <router-link class="navbar-brand d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <h1>Keepr</h1>
      </div>
    </router-link>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText"
      aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav me-auto">
        <li>
<!-- //ANCHOR - CREATE BUTTON -->
          <div class="dropdown">
            <button v-if="account.id" class="btn btn-success" type="button" id="createDropdown" data-bs-toggle="dropdown"
              title="">
              Create your own <i class="mdi mdi-arrow-down"></i>
            </button>
            <ul class="dropdown-menu" aria-labelledby="createDropdown">
              <li><a class="dropdown-item text-center fs-3" title="Create Keep" href="#" data-bs-toggle="modal"
                  data-bs-target="#CreateKeepForm">Keep</a></li>
              <li><a class="dropdown-item text-center fs-3" href="#" title="Create Vault" data-bs-toggle="modal"
                  data-bs-target="#CreateVaultForm">Vault</a>
              </li>
            </ul>
          </div>

        </li>
      </ul>
      <!-- LOGIN COMPONENT HERE -->
      <Login />
    </div>
  </nav>
</template>

<script>
import { computed } from "vue";
import Login from './Login.vue';
import { AppState } from "../AppState.js";
export default {
  setup() {
    return {
      account : computed(()=> AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
a:hover {
  text-decoration: none;
}

.nav-link {
  text-transform: uppercase;
}

.navbar-nav .router-link-exact-active {
  border-bottom: 2px solid var(--bs-success);
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}

@media screen and (min-width: 768px) {
  nav {
    height: 64px;
  }
}
</style>
