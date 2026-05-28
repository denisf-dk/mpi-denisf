<script setup>
import { onMounted, ref } from 'vue'
import { apiClient } from '../api/client'

const products = ref([])
const error = ref('')

onMounted(async () => {
  try {
    products.value = await apiClient.getProducts()
  } catch (e) {
    error.value = e.message
  }
})
</script>

<template>
  <section>
    <h2>Products</h2>
    <p v-if="error">{{ error }}</p>
    <ul v-else>
      <li v-for="product in products" :key="product.id">
        {{ product.sku }} — {{ product.name }}
      </li>
      <li v-if="products.length === 0">No products yet.</li>
    </ul>
  </section>
</template>
