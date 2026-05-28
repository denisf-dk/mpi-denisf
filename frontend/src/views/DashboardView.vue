<script setup>
import { onMounted, ref } from 'vue'
import { apiClient } from '../api/client'

const stats = ref({ products: 0, channels: 0, syncJobs: 0 })

onMounted(async () => {
  const [products, channels, syncJobs] = await Promise.all([
    apiClient.getProducts(),
    apiClient.getChannels(),
    apiClient.getSyncJobs()
  ])

  stats.value = {
    products: products.length,
    channels: channels.length,
    syncJobs: syncJobs.length
  }
})
</script>

<template>
  <section>
    <h2>Dashboard</h2>
    <ul>
      <li>Products: {{ stats.products }}</li>
      <li>Channels: {{ stats.channels }}</li>
      <li>Sync jobs: {{ stats.syncJobs }}</li>
    </ul>
  </section>
</template>
