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
    "app-header": appHeader
  }
};
</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css?family=Poppins:400,700&display=swap&subset=latin-ext');
@import "@/assets/styles/variables.scss";


html {
  box-sizing: border-box;
  scroll-behavior: smooth;
}

*,
*:before,
*:after {
  box-sizing: inherit;
}

body {
   background: linear-gradient(
    90deg,
    $bg-color ($dot-space - $dot-size),
    transparent 1%
   )
   center,
  linear-gradient($bg-color ($dot-space - $dot-size), transparent 1%) center,
  $dot-color;
 background-size: $dot-space $dot-space;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  font-family: 'Poppins', sans-serif;
  margin: 0;
  padding: 0;
  font-size: 16px;

  @media only screen and (max-width: 640px) {
    font-size: 12px;
  }
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
