<template>
<h1>welcome to keepr</h1>
  <div class="container">
    <section class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-3">
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


export default {
    setup() {
        onMounted(() => {
            getKeeps();
        });
        async function getKeeps() {
            try {
                await keepsService.getKeeps();
            }
            catch (error) {
                Pop.error(error);
            }
        }
        return {
            user: computed(() => AppState.user),
            keeps: computed(() => AppState.keeps)
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
</style>
