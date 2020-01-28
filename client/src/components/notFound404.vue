<template>
  <div class="not-found">
    <div class="not-found__box">
      <h1 class="not-found__title">404</h1>
      <span class="not-found__span">
        <em class="not-found__path">{{currentPath}}</em> there's nothing here.
      </span>
      <router-link class="not-found__link" to="/">
        <font-awesome-icon class="icon not-found__icon-arrow" icon="arrow-left"/>Back to home
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "NotFound404",
  data() {
    return {
      currentPath: this.$route.path
    };
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.not-found {
  position: fixed;
  z-index: 9999;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  padding: 1.5rem;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  &__box {
    width: 100%;
    height: 100%;
    background: #fff;
    color: #324960f5;
    box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }

  &__title {
    animation: color-transition linear 20s infinite;
    font-size: 10rem;
    margin: 0 0 1.5rem 0;
    letter-spacing: 0.5rem;

    @media only screen and (max-width: 640px) {
      font-size: 6rem;
      letter-spacing: 0.3rem;
    }
  }

  &__span {
    color: #00000094;
    text-transform: uppercase;
    font-weight: bold;
    width: 100%;
    max-width: 250px;
    overflow-wrap: break-word;
  }

  &__link {
    text-decoration: none;
    margin-top: 3.5rem;
    padding: 0.7rem 1.3rem;
    cursor: pointer;
    color: #324960f5;
    border: 1px solid #324960f5;
    transition: background-color 0.5s ease-in-out, color 0.5s ease-in-out;

    &:hover {
      color: #fff;
      background-color: #324960f5;
    }
    &:active {
      background-color: darken(#324960f5, 10%);
      transition: none;
    }
  }

  &__path {
    color: #d86060;
    text-transform: none;
    display: block;
    margin-bottom: 1rem;
  }

  &__icon-arrow {
    margin-right: 0.5rem;
  }
}

@keyframes color-transition {
  0% {
    color: #324960f5;
  }
  33% {
    color: rgba(50, 122, 74, 0.616);
  }
  66% {
    color: rgba(145, 43, 97, 0.596);
  }
  100% {
    color: #324960f5;
  }
}
</style>
