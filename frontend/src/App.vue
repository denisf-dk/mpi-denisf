<template>
  <div class="layout">
    <header>
      <h1>Marketplace Integration Admin</h1>
      <nav>
        <router-link to="/">Dashboard</router-link>
        <router-link to="/products">Products</router-link>
        <router-link to="/channels">Channels</router-link>
        <router-link to="/sync-jobs">Sync Jobs</router-link>
      </nav>
    </header>

    <main>
      <router-view />
    </main>
  </div>
</template>
