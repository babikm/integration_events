<template>
  <div id="app" class="myApp">
    <div id="nav">
      <router-link to="/">Hello</router-link>
    </div>
    <router-view/>
  </div>
</template>

<style lang="scss">
* {
  box-sizing: border-box;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

body {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  margin: 0;
  padding: 0;
  font-size: 1rem;
}

.myApp {
  text-align: center;
  color: #2c3e50;
}
</style>
