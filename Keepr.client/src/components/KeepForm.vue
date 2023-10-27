<template>
  <form @submit.prevent="createKeep">
  <h1 class="text-center">Create a Keep!</h1>
  <div class="mb-3">
    <label for="" class="form-label">Name:</label>
    <input v-model="keepData.name" type="text" class="form-control" required>
  </div>
  <div class="mb-3">
    <label for="" class="form-label">Description:</label>
    <input v-model="keepData.description" type="text" class="form-control" required>
  </div>
  <div class="mb-3">
    <label for="" class="form-label">Image:</label>
    <input v-model="keepData.img" type="url" class="form-control" required>
  </div>
  <button type="submit" class="btn btn-primary">Submit</button>
</form>
</template>

<script setup>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";
import { Modal } from "bootstrap";


const keepData = ref({})

        function resetForm() {
            keepData.value = { type: '' };
        }



async function createKeep(){
  // TODO grab the profile id from the route
  // TODO pass id to service for business logic check
  // route.params.profileId
  try {
    await keepsService.createKeep(keepData.value)
    Modal.getOrCreateInstance('#CreateKeepForm').hide()
    resetForm();
    Pop.success("Keep Created!")
  } catch (error) {
    Pop.error(error)
  }
}


</script>


<style>
</style>