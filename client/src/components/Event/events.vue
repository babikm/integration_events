<template>
  <div id="events" class="main-container--show">
    <h1 class="event__title">Aktualne wydarzenia</h1>
    <p class="wrapper wrapper-search--events">
      <input
        class="search-input"
        type="text"
        id="search"
        name="search"
        v-model="search"
        placeholder="szukaj..."
      />
      <label for="search"
        ><font-awesome-icon class="search-icon icon" icon="search"
      /></label>
    </p>
    <div class="lds-ring" v-if="loading">
      <div></div>
      <div></div>
      <div></div>
      <div></div>
    </div>
    <ul class="event__list">
      <li v-for="event in filteredEvents" :key="event.id" class="event__item">
        <span v-if="event.userList.length" class="event__counter">{{event.userList.length}}</span>
        <article class="event__wrapper">
          <h2 class="event__single-title">
            {{ event.name | to-uppercase }}
          </h2>
          <p class="event__paragraph">{{ event.description | snippet }}</p>
        </article>
        <div class="event__more-wrapper">
          <router-link class="event__single-link" :to="`/event/${event.id}`"
            >Dowiedz się więcej</router-link
          >
          <span v-rainbow class="event__created-date" >{{event.date | format-date}}</span>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";
import * as moment from "moment/moment";

export default {
  name: "events",
  data() {
    return {
      events: [],
      search: "",
      wide: "",
      timer: "",
      loading: false
    };
  },
  filters: {
    toUppercase(value) {
      return value.toUpperCase();
    },
    snippet(value) {
      return `${value.slice(0, 50)}...`;
    },
    formatDate(value) {
      return moment(String(value)).format('YYYY-MM-DD hh:mm')
    }
  },
  directives: {
    rainbow: {
       bind(el) {
         el.style.background = `#${Math.random().toString().slice(2,8)}`;
  }
    }
  },
  methods: {
    getEvents() {
      this.loading = true;
      this.$http
        .get(`${eventUrl}`)
        .then(resp => resp.json())
        .then(data => {
          this.loading = false;
          this.events = data;
        })
        .catch(err => {
          console.log(err);
        });
    },
  },

  created() {
    this.getEvents();
  },

  computed: {
    filteredEvents() {
      return this.events.filter(event => {
        const currentEventName = event.name.toLowerCase();
        const inputSearchValue = this.search.toLowerCase();
        return currentEventName.match(inputSearchValue);
      });
    },
  }
};
</script>

<style lang="scss">
@import "@/assets/styles/variables.scss";

@keyframes lds-ring {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
.main-container--show {
  position: relative;
  width: 100%;
  padding: 1rem;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  transition: background 0.4s ease-in-out;
}
.event__title {
  margin: 0;
  padding: 3rem 0 4rem 0;
}
.event__single-link {
  text-decoration: none;
  color: transparent;
  background: transparent;
  cursor: pointer;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  opacity: 0;
}
.event__wrapper {
  padding: 1.5rem 0.8rem 2rem;
  text-align: left;
}
.event__more-wrapper {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
}
.event__created-date {
  opacity: 0.5;
  width: 100%;
  text-align: right;
  padding: 0.8rem;
  color: $white;
  z-index: -1;
  border-bottom-right-radius: 0.2rem;
  border-bottom-left-radius: 0.2rem;
}
.event__list {
  list-style-type: none;
  padding: 0;
  width: 100%;
  max-width: 800px;
}
.event__item {
  position: relative;
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  border-radius: 0.3rem;
  margin-bottom: 2rem;
  transition: transform 0.4s ease-in-out, background 0.4s ease-in-out,
    color 0.4s ease-in-out;
}
.event__item:hover {
  transform: translateY(-3px);
}
.event__counter {
  position: absolute;
  top: -11px;
  left: -9px;
  z-index: 5;
  color: $white;
  text-align: center;
  padding: 0.1rem;
  border-radius: 25px;
  background: #ce3333;
  overflow: hidden;
  line-height: 1.5rem;
  width: 28px;
  height: 28px;
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.42);
}
.theme-switch .wrapper {
  text-align: center;
}
.wrapper {
  width: 100%;
  position: relative;
  &-search--events {
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
    max-width: 360px;
  }
  &-search--events .search-icon {
    width: 20px;
    height: 20px;
    position: absolute;
    top: 30px;
    right: 73px;
  }
}
.search-input {
  width: 100%;
  max-width: 220px;
  background: none;
  border: none;
  border-bottom: 0.11rem solid #00000085;
  color: #00000085;
  margin: 1.5rem 0 2.5rem 0;
  padding: 0.3rem 2rem 0.3rem 0;
  font-size: 1rem;

  &:focus {
    border-bottom-color: $navy-blue;
    color:$navy-blue;
  }
}
.icon {
  cursor: pointer;
}
.lds-ring {
  display: inline-block;
  position: relative;
  margin-top: 5rem;
  width: 80px;
  height: 80px;
}
.lds-ring div {
  box-sizing: border-box;
  display: block;
  position: absolute;
  width: 64px;
  height: 64px;
  margin: 8px;
  border: 8px solid #000;
  border-radius: 50%;
  animation: lds-ring 1.2s cubic-bezier(0.5, 0, 0.5, 1) infinite;
  border-color: #000 transparent transparent transparent;
}
.lds-ring div:nth-child(1) {
  animation-delay: -0.45s;
}
.lds-ring div:nth-child(2) {
  animation-delay: -0.3s;
}
.lds-ring div:nth-child(3) {
  animation-delay: -0.15s;
}
</style>
