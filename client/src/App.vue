<template>
  <div id="app" class="myApp">
    <app-header></app-header>
    <div class="main-wrapper">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import appHeader from "@/components/appHeader";

export default {
  name: "App",
  components: {
    'app-header': appHeader,
  },
}
</script>

<style lang="scss">
@import "@/assets/styles/variables.scss";

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
  color: $navy-blue;
}

.main-wrapper {
  padding: 0 1rem;
  width: 100%;
  max-width: 1400px;
  margin: 6rem auto 3rem;
}

</style>
