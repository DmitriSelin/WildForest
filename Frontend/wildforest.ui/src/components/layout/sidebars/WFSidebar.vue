<template>
  <div class="sidebar">
    <h1>
      <span>
        <div>V</div>
        <div>S</div>
      </span>
      <span>Vue Sidebar</span>
    </h1>

    <SidebarLink to="/" icon="fas fa-home">Home</SidebarLink>
    <SidebarLink to="/dashboard" icon="fas fa-columns">Dashboard</SidebarLink>
    <SidebarLink to="/analytics" icon="fas fa-chart-bar">Analytics</SidebarLink>
    <SidebarLink to="/friends" icon="fas fa-users">Friends</SidebarLink>
    <SidebarLink to="/image" icon="fas fa-image">Images</SidebarLink>
  </div>
</template>

<style lang="scss" scoped>
.sidebar {
  color: var(--white);
  background-color: var(--second);
  float: left;
  position: fixed;
  z-index: 1;
  top: 0;
  left: 0;
  bottom: 0;
  padding: 0.5em;
  transition: 0.3s ease;
  display: flex;
  width: 250px;
  flex-direction: column;

  h1 {
    height: 2.5em;
  }
}

.collapse-icon {
  position: absolute;
  bottom: 0;
  padding: 0.75em;
  color: var(--white);
  transition: 0.2s linear;
}
</style>
