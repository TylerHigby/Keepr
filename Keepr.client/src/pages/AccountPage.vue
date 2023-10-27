<template>
  <section class="container">
    <div class="text-center">
      <div>
        <img :src="account.coverImg" alt="" class="img-fluid p-3">
      </div>
      <img class="rounded" :src="account.picture" alt="" />
    <h1>{{ account.name }}</h1>
    <p>vaults {{ vaults.length }} | keeps {{ keeps.length }}</p>
  </div>



<!-- //SECTION - Vaults  -->
  <h1 class="text-center">Vaults</h1>
  <section class="masonry">
    <div v-for="vault in vaults" :key="vault.id" class="col-3">
      <VaultCard :vault="vault"/>
    </div>
  </section>

<!-- //SECTION - KEEPS  -->
<h1 class="text-center">Keeps</h1>
    <section class="masonry p-3">
    <div v-for="k in keeps" :key="k.id">
    <KeepCard :keep="k"/>
    </div>
    </section>

</section>
<section class="col-12 p-5">
<div class="elevation-5 rounded bg-grey">
    <form @submit.prevent="editAccount">

      <h1 class="text-center">Edit Account Details</h1>

<section class="row p-3">
  <div class="mb-3 col-12">
    <label for="name" class="form-label">Name:</label>
    <input v-model="editable.name" class="form-control" type="text" >
  </div>

  <!-- <div class="mb-3 col-6">
    <label for="exampleInputEmail1" class="form-label">Email:</label>
    <input v-model="editable.email" class="form-control" type="email">
  </div> -->

  <div class="mb-3 col-12">
    <label for="picture">Picture:</label>
    <input v-model="editable.picture" class="form-control" type="text">
  </div>

  <div class="mb-3 col-12">
    <label for="picture">Cover Image:</label>
    <input v-model="editable.coverImg" class="form-control" type="text">
  </div>
</section>

<div class="text-center pb-3">
  <button type="submit" class="btn btn-primary">Submit</button>
</div>


</form>
</div>

</section>
<!-- //SECTION - EDIT ACCOUNT FORM -->

</template>

<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { useRouter } from "vue-router";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultsService } from "../services/VaultsService.js";
import KeepCard from "../components/KeepCard.vue";
import VaultCard from "../components/VaultCard.vue";
export default {
    setup() {
        const editable = ref({});
        const router = useRouter();
        async function getMyKeeps() {
            try {
                const profileId = AppState.account.id;
                await keepsService.getKeepsByProfileId(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        async function getMyVaults() {
            try {
                const profileId = AppState.account.id;
                await vaultsService.getVaultsByProfileId(profileId);
            }
            catch (error) {
                Pop.error(error);
            }
        }
        watchEffect(() => {
            AppState.account;
            editable.value = AppState.account;
            getMyKeeps();
            getMyVaults();
        });
        return {
            editable,
            // profile: computed(() => AppState.profile),
            account: computed(() => AppState.account),
            keeps: computed(() => AppState.activeProfileKeeps),
            vaults: computed(() => AppState.activeProfileVaults),
            async editAccount() {
                try {
                    logger.log('edited info', editable.value);
                    await accountService.editAccount(editable.value);
                    Pop.success('Profile updated!');
                    router.push({ name: 'Account', params: { profileId: AppState.account.id } });
                }
                catch (error) {
                    Pop.error(error);
                }
            }
        };
    },
    components: { KeepCard, VaultCard }
}
</script>

<style scoped lang="scss">
/* img {
  max-width: 100vw;
} */


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
