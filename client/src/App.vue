<template>
  <div id="app" class="myApp">
    <Header></Header>
    <div class="main-wrapper">
      <transition name="router-anim">
        <router-view></router-view>
      </transition>
    </div>
  </div>
</template>

<script>
import Header from "@/components/Header";

export default {
  name: "App",
  components: {
    Header,
  },
}
</script>

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

.main-wrapper {
  padding: 0 1rem;
  width: 100%;
  max-width: 1400px;
  margin: 11rem auto 3rem;
}

.router-anim-enter-active {
  animation: coming 0.5s;
  animation-delay: 0.5s;
  opacity: 0;
}

.router-anim-leave-active {
  animation: going 0.3s;
}

@keyframes going {
  from {
    transform: translateX(0px);
  }

  to {
    transform: translateX(-50px);
    opacity: 0;
  }
}

@keyframes coming {
  from {
    transform: translateX(-50px);
    opacity: 0;
  }

  to {
    transform: translateX(0px);
    opacity: 1;
  }
}
</style>
