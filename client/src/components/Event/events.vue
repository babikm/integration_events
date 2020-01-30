<template>
  <div id="showBlogs" class="main-container--show">
    <h1 class="blog__title">Aktualne wydarzenia</h1>
    <p class="wrapper wrapper-search--blogs">
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
    <ul class="blog__list">
      <li v-for="event in filteredEvents" :key="event.id" class="blog__item">
        <article class="blog__wrapper">
          <h2 class="blog__single-title">
            {{ event.name | to-uppercase }}
          </h2>
          <p class="blog__paragraph">{{ event.description | snippet }}</p>
        </article>
        <div class="blog__more-wrapper">
          <router-link class="blog__single-link" :to="`/event/${event.id}`"
            >Dowiedz się więcej</router-link
          >
          <span class="blog__created-date">{{ event.date }}</span>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";

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
    formatDate(date) {
      const day = date.getDate();
      const month = date.getMonth();
      const year = date.getFullYear();
      const currentHour = date.getHours();
      const minutes = date.getMinutes();

      
      return `${day}-${month}-${year} ${currentHour}:${minutes}`;
    }
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
    }
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
.blog__title {
  margin: 0;
  padding: 3rem 0 4rem 0;
}
.blog__single-link {
  text-decoration: none;
  @include default-button($navy-blue);
}
.blog__wrapper {
  padding-bottom: 1rem;
  text-align: left;
}
.blog__more-wrapper {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
}
.blog__created-date {
  opacity: 0.5;
}
.blog__list {
  list-style-type: none;
  padding: 0;
  width: 100%;
  max-width: 800px;
}
.blog__item {
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  border-radius: 0.3rem;
  padding: 1rem 0.8rem;
  margin-bottom: 2rem;
  transition: transform 0.4s ease-in-out, background 0.4s ease-in-out,
    color 0.4s ease-in-out;
}
.blog__item:hover {
  transform: translateY(-3px);
}
.theme-switch .wrapper {
  text-align: center;
}
.wrapper {
  width: 100%;
  position: relative;
  &-search--blogs {
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
    max-width: 360px;
  }
  &-search--blogs .search-icon {
    width: 20px;
    height: 20px;
    position: absolute;
    top: 30px;
    right: 80px;
  }
}
.search-input {
  width: 100%;
  max-width: 200px;
  background: none;
  border: none;
  border-bottom: 1px solid #000;
  color: #000;
  margin: 1.5rem 0 0.5rem 0;
  padding: 0.3rem 1.5rem 0.3rem 0;
  font-size: 1rem;
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
