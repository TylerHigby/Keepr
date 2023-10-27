<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
   <footer class="bg-secondary text-white">
    <p>Keepr</p>
  </footer>
  <KeepModal/>
  <KeepFormModal/>
  <VaultFormModal/>
</template>

<script>
import { computed, watchEffect } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import KeepModal from "./components/KeepModal.vue"
import KeepFormModal from "./components/KeepFormModal.vue"
import VaultFormModal from "./components/VaultFormModal.vue"
import Pop from "./utils/Pop.js"
import { vaultsService } from "./services/VaultsService.js"

export default {
  setup() {

    watchEffect(()=> {
      if (AppState.account.id){
        getMyVaults()
      }
    })

      async function getMyVaults(){
        try {
          await vaultsService.getMyVaults()
        } catch (error) {
          Pop.error(error)
        }
      }

    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, KeepModal, KeepFormModal, VaultFormModal }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root{
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
