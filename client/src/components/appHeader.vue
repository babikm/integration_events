<template>
  <header v-if="checkIsLogged" class="header">
    <nav class="navigation">
      <span class="navigation__logo">
        <span class="navigation__link">Wydarzenia integracyjne 😎</span>
      </span>
      <button @click="showMenu" class="burger-show">
        <span class="burger-show--box">
          <span class="burger-show--inner"></span>
        </span>
      </button>
      <ul class="navigation__list">
        <button @click="hideMenu" class="burger-hide">
          <span class="burger-hide--box">
            <span class="burger-hide--inner"></span>
          </span>
        </button>
        <li class="navigation__item">
          <router-link class="navigation__link" to="/">Wydarzenia</router-link>
        </li>
        <li class="navigation__item">
          <router-link class="navigation__link" to="/addEvent">Dodaj wydarzenie</router-link>
        </li>
        <li class="current-user">
          <font-awesome-icon class="icon user" icon="user"/>
          <ul class="current-user__about-list">
            <li class="current-user__about-item">
              <strong>
                <font-awesome-icon class="icon user-icon" icon="user-shield"/>
              </strong>
              {{user.userName}}
            </li>
            <li class="current-user__about-item">
              <strong>
                <font-awesome-icon class="icon user-icon" icon="signature"/>
              </strong>
              {{user.firstName}} {{user.lastName}}
            </li>
          </ul>
        </li>
        <button class="navigation__logout" @click="logout">Wyloguj
          <font-awesome-icon class="icon" icon="door-open"/>
        </button>
      </ul>
    </nav>
  </header>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name: "appHeader",

  data() {
    return {
      user: {},
    };
  },

  computed: {
    checkIsLogged() {
      return this.$store.getters.isLogged;
    },
    menuSelector() {
      return document.querySelector(".navigation__list");
    },
  },
  created() {
    this.user = this.$store.getters.getCurrentUser;
  },
  methods: {
    showMenu() {
      this.menuSelector.classList.add("navigation__list--visible");
    },
    hideMenu() {
      this.menuSelector.classList.remove("navigation__list--visible");
    },
    logout() {
      this.$store.dispatch('logout');
    },
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style lang="scss" scoped>
/* Logo animation*/

.duos_kolos_logo,
.duos_text,
.k_text,
.l_text,
.s_text {
  transition: transform 0.4s ease-in-out, opacity 0.7s ease-in-out;
}

.duos_kolos_logo:hover {
  transform: scale(0.9) rotate(-14deg);
}

.duos_kolos_logo:hover > .duos_text {
  opacity: 0;
}

.duos_kolos_logo:hover > .k_text {
  opacity: 0;
}

.duos_kolos_logo:hover > .l_text {
  opacity: 0;
}

.duos_kolos_logo:hover > .s_text {
  opacity: 0;
}

.user {
  width: 23px;
  height: 23px;
}

.current-user {
  margin: 0 0 0 3rem;
  position: relative;

  &:hover > &__about-list {
    transform: translateX(0px);
    opacity: 1;
    visibility: visible;
  }

  &__about-list {
    background: #324960;
    color: #fff;
    padding: 1.5rem 1rem;
    position: absolute;
    left: -200px;
    width: 100%;
    min-width: 220px;
    list-style-type: none;
    visibility: hidden;
    transform: translateX(-50px);
    opacity: 0;
    text-align: left;
    box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
    transition: transform 0.3s ease-in-out, opacity 0.3s ease-in-out;
  }

  &__about-item {
    border-bottom: 1px solid #ffffff26;
    padding-bottom: 0.3rem;
  }

  &__about-item + &__about-item {
    margin-top: 1rem;
  }
}

.user-icon {
  margin-right: 0.4rem;
}

@mixin hamburger-line {
  width: 100%;
  height: 0.2rem;
  background-color: #324960;
  position: absolute;
}
.header {
  padding: 1rem;
  position: fixed;
  z-index: 9999;
  top: 0;
  left: 0;
  width: 100%;
  background: #ffffff;
  box-shadow: 0px 4px 11px rgba(0, 0, 0, 0.2);
}

.header::before {
  position: absolute;
  content: "";
  top: 0;
  left: 0;
  display: block;
  width: 100%;
  height: 0.1rem;
  background-color: #324960;
}

.navigation {
  position: relative;
  width: 100%;
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 1rem;
  display: flex;
  justify-content: space-between;
  align-items: center;

  &__list {
    padding: 0;
    margin: 0;
    list-style-type: none;
    display: flex;
    justify-content: space-between;
    align-items: center;
    transform: translateX(0px);
    transition: transform 0.3s ease-in-out;
  }

  &__link {
    text-decoration: none;
    color: #324960;
    font-weight: bold;
    position: relative;

    &::after {
      position: absolute;
      content: "";
      width: 0%;
      height: 0.1rem;
      background: #324960;
      bottom: -10px;
      left: 0;
      transition: width 0.4s ease-in-out;
    }
  }
}

.router-link-exact-active::after {
  width: 100%;
}

.burger-show {
  cursor: pointer;
  padding: 0.2rem;
  border: none;
  background-color: transparent;

  &--box {
    width: 30px;
    height: 20px;
    display: inline-block;
    position: relative;
  }

  &--inner {
    @include hamburger-line;
    left: 0;
    top: 50%;
    transform: translateY(-50%);

    &::before,
    &::after {
      @include hamburger-line;
      left: 0;
      content: "";
    }

    &::before {
      top: -10px;
    }

    &::after {
      top: 10px;
    }
  }
}

.burger-hide {
  position: absolute;
  top: 20px;
  right: 15px;
  cursor: pointer;
  padding: 0.6rem;
  border: none;
  background-color: transparent;

  &--box {
    width: 30px;
    height: 25px;
    display: inline-block;
    position: relative;
  }

  &--inner {
    @include hamburger-line;
    left: 0;
    top: 50%;
    transform: translateY(-50%);
    background-color: transparent;

    &::before,
    &::after {
      @include hamburger-line;
      left: 0;
      content: "";
    }

    &::before {
      top: -10px;
      transform: translateY(10px) rotate(45deg);
    }

    &::after {
      top: 10px;
      transform: translateY(-10px) rotate(-45deg);
    }
  }
}

.navigation__item + .navigation__item {
  margin-left: 20px;
}

.navigation__logout {
  background: none;
  border-radius: 0px 8px;
  cursor: pointer;
  border: 2px solid #324960;
  font-weight: bold;
  padding: 0.7rem 1.5rem;
  margin-left: 4.5rem;
  color: #324960;
  transition: background 0.3s ease-in-out, color 0.1s ease-in-out;

  &:hover {
    background: #324960;
    color: #fff;
  }

  &:active {
    background: #324960de;
  }
}

@media (min-width: 1021px) {
  .burger-show,
  .burger-hide {
    display: none;
  }
}

@media (max-width: 1020px) {
  .burger-show,
  .burger-hide {
    display: block;
  }

  .navigation {
    max-width: 100vw;
    text-align: left;
  }

  .navigation__logo {
    width: 100%;
    text-align: left;
  }

  .navigation__list {
    background: #ffffff;
    padding: 1rem 0;
    margin: 0;
    position: fixed;
    transform: translateX(-1300px);
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    box-shadow: 0px 4px 11px rgba(0, 0, 0, 0.2);
  }

  .navigation__list--visible {
    transform: translateX(0px);
  }

  .navigation__item + .navigation__item {
    margin-left: 0;
  }

  .navigation__item {
    padding: 3rem 0 0 0;
  }

  .navigation__logout {
    margin: 3rem 0 0 0;
  }

  .current-user {
    margin: 3rem 0 0 0;
  }
}
</style>
