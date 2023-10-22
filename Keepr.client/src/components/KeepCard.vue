<template>
  <section @click="getKeepDetails()" data-bs-toggle="modal" data-bs-target="#keep-modal">
    <div class="card elevation-5 my-3">
      <div class="row">
        <h2 class="col-6">{{ keep.name }}</h2>
        <img :src="keep.creator.picture" alt="" class="col-6">
      </div>
      <img :src="keep.img" alt="" class="keepCardImage">
    </div>
  </section>
</template>

<script>
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {keep: {type: Keep, required: true}},
setup(props) {
  return {
    async getKeepDetails(){
      try {
        logger.log('keep id:', props.keep.id)
        const keepId = props.keep.id
        await keepsService.getKeepById(keepId)
      } catch (error) {
        Pop.error(error)
      }
    }
  };
},
};
</script>


<style>
.keepCardImage{
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
}

</style>