<template>



<div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog modal-lg modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">


        <!-- //ANCHOR - DELETE BUTTON -->
        <!-- <button v-show="account.id == activeKeep.creatorId" @click="deleteKeep" class="btn btn-danger"><i class="mdi mdi-trash-can-outline"></i></button> -->


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
              <p class="col-6 text-center">user vaults</p>
              <div class="col-6 text-center">
                <img class="profImg" :src="activeKeep.creator.picture" alt="">
                <p>{{ activeKeep.creator.name }}</p>
              </div>
            </div>
          </section>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>
</template>

<script>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { useRoute, useRouter } from "vue-router";
import { Modal } from "bootstrap";

export default {
  // props: {id: {type: String, required: true}},
setup() {
  const route = useRoute();
  const router = useRouter();
  return {
    activeKeep: computed(()=> AppState.activeKeep),
    account: computed(()=> AppState.account),

    async deleteKeep(){
      try {
        if (await Pop.confirm()){
          await keepsService.deleteKeep(route.params.keepId)
          Modal.getOrCreateInstance('#keep-modal').hide()
          Pop.success('Keep successfully deleted.')
          // router.push({name: "Home"})
        }
      } catch (error) {
        Pop.error(error)
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