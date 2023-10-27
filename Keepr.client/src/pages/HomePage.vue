<template>
  <div class="container">

<!-- //ANCHOR - Keep Cards -->
    <section class="masonry">
      <div v-for="keep in keeps" :key="keep.id" class="col-6 col-md-3">
        <KeepCard :keep="keep"/>
      </div>
    </section>


  </div>
</template>

<script>
import {computed, onMounted } from 'vue';
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { AppState } from "../AppState.js";
import KeepCard from "../components/KeepCard.vue";
import { vaultsService } from "../services/VaultsService.js";


export default {
    setup() {
        onMounted(() => {
            getKeeps();
            // getMyVaults();
        });
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error);
            }
        }

        async function getMyVaults(){
          try {
            await vaultsService.getAccountVaults()
          } catch (error) {
            Pop.error(error)
          }
        }



        return {
            user: computed(() => AppState.user),
            keeps: computed(() => AppState.keeps),

            // activeKeep: computed(()=> AppState.activeKeep)
        };
    },
    components: { KeepCard }
}
</script>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: 50vw;

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}

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
