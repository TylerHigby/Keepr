<template>
<div class="text-center p-3">
  <button v-if="vault != null && account.id == vault.creator.id" class="btn btn-danger m-3" @click="deleteVault(vault.id)">Delete Vault</button>
  <button v-if="vault != null && account.id == vault.creator.id" class="btn btn-secondary m-3" @click="MakePrivateToggle()">Make Private</button>
</div>
<div v-if="vault" class="text-center">
  <img class="img-fluid" :src="vault.img" alt="">
  <h1>{{ vault.name }}</h1>
  <div>{{ keeps.length }} keeps</div>
</div>

<h1 class="text-center">Keeps</h1>
<section class="masonry p-3">
  <div v-for="k in keeps" :key="k.id">
    <KeepCard :keep="k"/>
  </div>
</section>

</template>

<script>
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { vaultsService } from "../services/VaultsService.js";
import { keepsService } from "../services/KeepsService.js";
import { computed, onMounted, popScopeId} from "vue";
import { router } from "../router.js";
import { logger } from "../utils/Logger.js";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";



export default {
    setup() {
        const route = useRoute();
        async function getVaultById() {
            try {
                await vaultsService.getVaultById(route.params.vaultId);
            }
            catch (error) {
          if (AppState.activeVault.isPrivate == true && AppState.account.id != AppState.activeVault.creatorId){
                router.push({ name: 'Home' });
                logger.error(error);
                Pop.toast("You can't view this vault");}
            }
        }
        async function getKeepsByVaultId() {
            try {
                await keepsService.getKeepsByVaultId(route.params.vaultId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        onMounted(() => {
            getVaultById();
            getKeepsByVaultId();
        });
        return {
            vault: computed(() => AppState.activeVault),
            keeps: computed(() => AppState.keeps),
            account: computed(() => AppState.account),

            async deleteVault(vaultId){
              try {
                if(await Pop.confirm()) {
                await vaultsService.deleteVault(vaultId)
                router.push({name: 'Home'})}
              } catch (error) {
                Pop.error(error)
              }
            },

            async MakePrivateToggle(){
              try {
                logger.log('flipping privacy toggle')
                let isPrivate = AppState.activeVault.isPrivate
                isPrivate = !isPrivate
                const editable = {isPrivate: isPrivate}
                vaultsService.MakePrivateToggle(editable, AppState.activeVault.id)
                if (isPrivate == true){
                  Pop.success('Made vault private')
                }
                  else {Pop.success('unlocked vault')}
              } catch (error) {
                Pop.error(error)
              }
            }

        };
    },
    components: { KeepCard }
};
</script>


<style scoped lang="scss">
.masonry{
  columns: 4;
div {
    display: inline-block;
    width: 100%;
  }
}

@media screen and (max-width: 769px) {
  .masonry{
    columns: 2;
div {
      display: inline-block;
      width: 100%;
    }
  }
}
</style>