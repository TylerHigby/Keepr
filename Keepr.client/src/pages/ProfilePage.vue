<template>
  <section class="container">
  <div class="text-center" v-if="profile">
    <div>
    <img class="p-2 img-fluid" :src="profile.coverImg" alt="">
    </div>
    <img class="p-2 img-fluid" :src="profile.picture" alt="">
    <h1>{{ profile.name }}</h1>
  </div>
    <p class="text-center">vaults {{vaults.length}} | keeps {{ keeps.length }}</p>
</section>
<!-- //SECTION - Vaults  -->
    <h1 class="text-center">Vaults</h1>
  <section class="masonry p-3">
    <div v-for="vault in vaults" :key="vault.id">
      <VaultCard :vault="vault"/>
    </div>
  </section>


<!-- //SECTION - KEEPS -->
    <h1 class="text-center">Keeps</h1>
    <section class="masonry p-3">
    <div v-for="k in keeps" :key="k.id">
    <KeepCard :keep="k"/>
    </div>
    </section>
</template>

<script>
import { computed, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { useRoute } from "vue-router";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";
import { keepsService } from "../services/KeepsService.js";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";
import VaultCard from "../components/VaultCard.vue";

export default {
    setup() {
        const route = useRoute();
        watchEffect(() => {
            getProfileById();
            getKeepsByProfileId();
            getVaultsByProfileId();
        });
        async function getProfileById() {
            try {
                await accountService.getProfileById(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getKeepsByProfileId() {
            try {
                await keepsService.getKeepsByProfileId(route.params.profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getVaultsByProfileId(){
          try {
            await vaultsService.getVaultsByProfileId(route.params.profileId)
          } catch (error) {
            Pop.error(error)
          }
        }


        return {
            profile: computed(() => AppState.activeProfile),
            // account: computed(()=> AppState.account),
            keeps: computed(() => AppState.activeProfileKeeps),
            vaults: computed(() => AppState.activeProfileVaults)
        };
    },
    components: { KeepCard, VaultCard }
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

.coverImg{
  width: 300px;
}

</style>