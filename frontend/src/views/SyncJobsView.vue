<script setup>
import { onMounted, ref } from 'vue'
import { apiClient } from '../api/client'

const jobs = ref([])
const busy = ref(false)
const error = ref('')

const loadJobs = async () => {
  try {
    jobs.value = await apiClient.getSyncJobs()
  } catch (e) {
    error.value = e.message
  }
}

const triggerSync = async () => {
  busy.value = true
  error.value = ''

  try {
    await apiClient.triggerSync()
    await loadJobs()
  } catch (e) {
    error.value = e.message
  } finally {
    busy.value = false
  }
}

onMounted(loadJobs)
</script>

<template>
  <section>
    <h2>Sync Jobs</h2>
    <button :disabled="busy" @click="triggerSync">
      {{ busy ? 'Queuing...' : 'Trigger sync' }}
    </button>
    <p v-if="error">{{ error }}</p>

    <ul>
      <li v-for="job in jobs" :key="job.id">
        {{ job.id }} — {{ job.status }}
      </li>
      <li v-if="jobs.length === 0">No sync jobs yet.</li>
    </ul>
  </section>
</template>
