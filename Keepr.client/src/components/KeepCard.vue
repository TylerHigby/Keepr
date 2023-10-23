<template>
  <section @click="getKeepDetails()" data-bs-toggle="modal" data-bs-target="#keep-modal">
    <div class="card elevation-5 my-3 container p-0">
      <img :src="keep.img" alt="">
        <p class="col-6 keepName ps-3">{{ keep.name }}</p>
        <img :src="keep.creator.picture" alt="" class="col-6 ProfPic p-1">
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
/* .keepCardImage{
  width: 100%;
  aspect-ratio: 1/1;
  object-fit: cover;
  object-position: center;
} */

.ProfPic{
  height: 50px;
  width: 50px;
  border-radius: 50%;
  position: absolute;
  bottom: 1px;
  right: 1px;
}

.container{
  position: relative;
}

.keepName{
  position: absolute;
  bottom: 1px;
  left: 1px;
  color: white;
  /* background-color: grey; */
  /* -webkit-text-stroke-width: 1px;
  -webkit-text-stroke-color: grey; */
  /* background-color: white; */
}

.creatorImage{

}
</style>