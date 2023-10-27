import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import Pop from "../utils/Pop.js"
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }

  async editAccount(updateData){
    const res = await api.put('/account', updateData)
    logger.log('editing account', res.data)
  }

async getProfileById(profileId){
  try {
    logger.log('getting profile')
    const res = await api.get(`api/profiles/${profileId}`)
    AppState.activeProfile = res.data
  } catch (error) {
    Pop.error(error)
  }
}


}

export const accountService = new AccountService()
