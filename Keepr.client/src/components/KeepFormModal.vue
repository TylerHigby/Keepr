<template>

<!-- Modal -->
<div class="modal fade" id="CreateKeepForm" tabindex="-1" aria-labelledby="CreateKeepFormLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-5" id="CreateKeepFormLabel">Create a Keep!</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <KeepForm/>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <!-- <button type="button" class="btn btn-primary">Save changes</button> -->
      </div>
    </div>
  </div>
</div>
</template>

<script>
export default {
setup() {
  return {};
},
};
</script>


<style>
</style>