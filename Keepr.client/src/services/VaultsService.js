
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"

class VaultsService{


  async createVault(vaultData){
    const res = await api.post('api/vaults', vaultData)
    logger.log('Vault Created', res.data)
    AppState.vaults.push(new Vault(res.data))
    AppState.myVaults.push(new Vault(res.data))
    // AppState.activeProfileVaults.push(new Vault(res.data))
  }

async getVaultById(vaultId){
  const res = await api.get(`api/vaults/${vaultId}`)
  logger.log('Got vault by Id', res.data)
  AppState.activeVault = new Vault(res.data)
}

async getAccountVaults(){
  const res = await api.get('account/vaults')
  logger.log('getting accounts vaults', res.data)
  AppState.vaults = res.data.map(vault => new Vault(vault))
}

async getMyVaults(){
  const res = await api.get('account/vaults')
  AppState.myVaults = res.data.map(vault => new Vault(vault))
}

async deleteVault(vaultId){
  await api.delete(`api/vaults/${vaultId}`)
  const indexToRemove = AppState.vaults.findIndex(vault => vault.id == vaultId)
  AppState.vaults.splice(indexToRemove, 1)
}

async getVaultsByProfileId(profileId){
  const res = await api.get(`api/profiles/${profileId}/vaults`)
  logger.log('getting vaults by profile Id', res.data)
  // AppState.activeProfileVaults = res.data.map(vault => new Vault(vault))
  AppState.myVaults = res.data.map(vault => new Vault(vault))
}



async createVaultKeep(data){
  await api.post('api/vaultKeeps',data)
  logger.log('VaultKeep Created', )
}

async MakePrivateToggle(data, vaultId){
  await api.put(`api/vaults/${vaultId}`, data)
  AppState.activeVault.isPrivate = !AppState.activeVault.isPrivate
  logger.log('IsPrivate?' ,AppState.activeVault.isPrivate)
}


}

export const vaultsService = new VaultsService

