import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},

  /**@type {Keep} */
  keeps: [],

  /**@type {Keep} */
  activeKeep: null,

  /**@type {keep} */
  activeProfileKeeps: [],

  /**@type {Vault} */
  vaults: [],

  /**@type {Vault} */
  activeVault: null,

  /**@type {vault} */
  myVaults: [],

  /**@type {vault} */
  activeProfileVaults: [],

  /**@type {Profile} */
  profile: null,

    /**@type {Profile} */
    activeProfile: null,
})
