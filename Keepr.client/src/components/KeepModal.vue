<template>

<div class="modal fade" id="keep-modal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="exampleModalLabel">Modal title</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        ...
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

export default {
  props: {id: {type: String, required: true}},
setup() {
  return {
    keep: computed(()=> AppState.activeKeep),
    account: computed(()=> AppState.account),
  };
},
};
</script>


<style>
</style>