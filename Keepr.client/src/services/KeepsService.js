import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService{

  async getKeeps(){
    const res = await api.get('api/keeps')
    logger.log('got keeps', res.data)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }

  async getKeepById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    logger.log('Got keep by id', res.data)
    AppState.activeKeep = new Keep(res.data)
  }

  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    logger.log('Keep Created', res.data)
    AppState.keeps.push(new Keep(res.data))
  }

  async getKeepsByVaultId(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log('getting keeps by profile Id', res.data)
    AppState.keeps = res.data.map(keep => new Keep(keep))
  }


  async deleteKeep(keepId){
    const res = await api.delete(`api/keeps/${keepId}`)
    logger.log('deleting keep', res.data)
    let indexToRemove = AppState.activeKeep.findIndex(keep => keep.id == keepId)
    AppState.activeKeep.splice(indexToRemove, 1)
  }



}

export const keepsService = new KeepsService