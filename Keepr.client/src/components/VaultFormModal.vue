<template>


<!-- Modal -->
<div class="modal fade" id="CreateVaultForm" tabindex="-1" aria-labelledby="CreateVaultFormLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="CreateVaultFormLabel">Create a Vault!</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <VaultForm/>
      </div>
      <div class="modal-footer">
        <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
        <!-- <button type="button" class="btn btn-primary">Save changes</button> -->
      </div>
    </div>
  </div>
</div>
</template>

<script>
import VaultForm from "./VaultForm.vue";

export default {
    setup() {
        return {};
    },
    components: { VaultForm }
};
</script>


<style>
</style>