<template>
  <section class="container">
    <img src="" alt="">
  </section>
</template>

<script>
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import { onMounted, popScopeId} from "vue";
import { router } from "../router.js";
import { logger } from "../utils/Logger.js";

export default {
setup() {
  const route = useRoute();
  async function getVaultById(){
    try {
      await vaultsService.getVaultById(route.params.vaultId)
    } catch (error) {
      router.push({name:'Home'})
      logger.error(error)
      Pop.toast("You can't view this vault")
    }
  }
  async function getKeepsByVaultId(){
    try {
      await keepsService.getKeepsByVaultId(route.params.vaultId)
    } catch (error) {
      Pop.error(error)
    }
  }
onMounted(()=> {
  getVaultById();
  getKeepsByVaultId();
})
  return {};
},
};
</script>


<style>
</style>