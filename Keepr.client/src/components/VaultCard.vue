<template>
<!-- <router-link :to="{name: 'Vaults', params: {id: vault.id}}"> -->
  <section class="selectable">
    <div class="card elevation-5 my-3 container p-0">
      <img :src="vault.img" :alt="vault.name" class="vaultCardImage">
      <p class="col-9 vaultName ps-3">{{ vault.name }}</p>
    </div>
  </section>
<!-- </router-link> -->
</template>

<script>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { AppState } from "../AppState.js";


export default {
  props: {vault: {type: Vault, required: true}},
setup(props) {
  return {
    account: computed(()=>AppState.account)
  };
},
};
</script>


<style>
.vaultCardImage{
border-radius: 10px;
}
.vaultName{
  position: absolute;
  bottom: 1px;
  left: 1px;
  color: white;
}
</style>