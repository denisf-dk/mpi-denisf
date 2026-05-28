<script setup>
import { onMounted, ref } from 'vue'
import { apiClient } from '../api/client'

const channels = ref([])
const error = ref('')

onMounted(async () => {
  try {
    channels.value = await apiClient.getChannels()
  } catch (e) {
    error.value = e.message
  }
})
</script>

<template>
  <section>
    <h2>Channels</h2>
    <p v-if="error">{{ error }}</p>
    <ul v-else>
      <li v-for="channel in channels" :key="channel.id">
        {{ channel.name }} ({{ channel.type }})
      </li>
      <li v-if="channels.length === 0">No channels yet.</li>
    </ul>
  </section>
</template>
