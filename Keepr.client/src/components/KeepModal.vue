<template>



<div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">


        <!-- //ANCHOR - DELETE BUTTON -->
        <button v-if="activeKeep != null && account.id == activeKeep.creatorId" @click="deleteKeep" class="btn btn-danger" title="Delete Keep">Delete<i class="mdi mdi-trash-can-outline"></i></button>


      </div>
      <div class="modal-body">
        <div class="container-fluid">
          <section v-if="activeKeep" class="row">
            <div class="col-12 col-md-6 p-0">
              <img class="img-fluid" :src="activeKeep.img" alt="">
            </div>
            <div class="col-12 col-md-6 row">
              <p class="col-6 text-center fs-4"><i class="mdi mdi-eye"></i> {{activeKeep.views}}</p>
              <p class="col-6 text-center fs-4"><i class="mdi mdi-alpha-k-circle-outline"></i> {{activeKeep.kept}}</p>
              <h3 class="text-center">{{ activeKeep.name }}</h3>
              <p>{{ activeKeep.description }}</p>
              <div class="col-6 text-center">
                
                <div class="dropdown">
            <button v-if="account.id" class="btn btn-success" type="button" id="createDropdown" data-bs-toggle="dropdown"
              title="">
              Add to Vault<i class="mdi mdi-arrow-down"></i>
            </button>
            <!-- <button v-if="activeVault != null && activeVault.id && account.id == activeVault.creatorId" class="btn btn-danger m-2" @click="removeVaultKeep" >remove from vault <i class="mdi mdi-trash-can"></i></button> -->
            <ul class="dropdown-menu" aria-labelledby="createDropdown">
              <li v-for="v in myVaults" :key="v.id" @click="createVaultKeep(v.id)" ><a class="dropdown-item text-center" title="">{{v.name}}</a></li>
            </ul>
          </div>
              </div>
              <div class="col-6 text-center">
<div @click="goToProfile" class="selectable">
                <img class="profImg" :src="activeKeep.creator.picture" alt="">
                <p>{{ activeKeep.creator.name }}</p>
</div>
              </div>
            </div>
          </section>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <!-- <button type="button" class="btn btn-primary">Save changes</button> -->
      </div>
    </div>
  </div>
</div>
</template>

<script>
import { computed, popScopeId } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute, useRouter } from "vue-router";
import { Modal } from "bootstrap";
import { vaultsService } from "../services/VaultsService.js";

export default {
  // props: {id: {type: String, required: true}},
setup() {
  const route = useRoute();
  const router = useRouter();
  return {
    activeKeep: computed(()=> AppState.activeKeep),
    activeVault: computed(()=>AppState.activeVault),
    account: computed(()=> AppState.account),
    myVaults: computed(()=> AppState.myVaults),
    // profile: computed(()=> AppState.profile),

    async deleteKeep(){
      try {
        if (await Pop.confirm()){
          await keepsService.deleteKeep(AppState.activeKeep.id)
          Modal.getOrCreateInstance('#keep-modal').hide()
          Pop.success('Keep successfully deleted.')
          router.push({name: "Home"})
        }
      } catch (error) {
        Pop.error(error)
      }
    },

    goToProfile(){
      try {
        router.push({name: 'Profile', params: {profileId: AppState.activeKeep.creatorId}})
        Modal.getOrCreateInstance('#keep-modal').hide()
      } catch (error) {
        Pop.error(error)
      }
    },

    async createVaultKeep(vaultId) {
      try {
        const data = {keepId: AppState.activeKeep.id, vaultId: vaultId}
        await vaultsService.createVaultKeep(data)
        AppState.activeKeep.kept++
        Pop.success('Successfully added keep to your vault.')
      } catch (error) {
        Pop.toast("You've already added that keep to your vault.")
      }
    }


  };
},
};
</script>


<style>
.profImg{
  border-radius: 50%;
  height: 50px;
}

/* .mainpic{
width: 100%;
object-fit: cover;
object-position: center;
} */

.img-fluid{
  /* border-radius: 25px; */
}
</style>