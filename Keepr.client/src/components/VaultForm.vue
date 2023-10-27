<template>
<form @submit.prevent="createVault">
  <h1 class="text-center">Create a Vault!</h1>
  <div class="mb-3">
    <label for="" class="form-label">Name:</label>
    <input v-model="vaultData.name" type="text" class="form-control" required>
  </div>
  <div class="mb-3">
    <label for="" class="form-label">Description:</label>
    <input v-model="vaultData.description" type="text" class="form-control" required>
  </div>
  <div class="mb-3">
    <label for="" class="form-label">Image:</label>
    <input v-model="vaultData.img" type="url" class="form-control" required>
  </div>
  <div class="mb-3">
    <label for="isPrivate">Make Private?</label>
    <input v-model="vaultData.isPrivate" type="checkbox">
  </div>
  <button type="submit" class="btn btn-primary">Submit</button>
</form>
</template>


<script setup>

import { ref } from "vue";
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";


const vaultData = ref({})


function resetForm() {
            vaultData.value = { type: '' };
        }

async function createVault(){
  try {
  await vaultsService.createVault(vaultData.value)
  Modal.getOrCreateInstance('#CreateVaultForm').hide()
  resetForm();
  Pop.success("Vault Created!")
  } catch (error) {
  Pop.error(error)
  }
}




</script>


<style>
</style>