<template>
  <div id="events" class="main-container--show">
    <h1 class="event__title">Wydarzenia do których dołączyłeś</h1>
    <div class="lds-ring" v-if="loading">
      <div></div>
      <div></div>
      <div></div>
      <div></div>
    </div>
     <ul class="events__list">
      <li v-for="event in userEvents" :key="event.id" class="events__item">
        <div class="events__wrapper">
          <h2 v-rainbow class="events__single-title">
            {{ event.name }}
          </h2>
          <p class="events__paragraph">{{ event.description | snippet }}</p>
        </div>
        <div class="events__more-wrapper">
          <router-link class="events__single-link" :to="`/event/${event.id}`"
            >Dowiedz się więcej</router-link
          >
          <div v-if="event.userList.length" class="events__counter">
            <font-awesome-icon
              class="search-icon icon__users events__counter--icon"
              icon="users"
            />
            <span class="events__counter--count">{{
              event.userList.length
            }}</span>
          </div>
          <span class="events__created-date">
            <p class="events__created-date--spot">{{ event.spot }}</p>
            {{ event.date | format-date}}
          </span>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";
import * as moment from "moment/moment";

export default {
  name: "userEvents",
  data() {
    return {
      userEvents: [],
      userId: this.$store.getters.getCurrentUser.id,
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
      return moment(String(value)).format("YYYY-MM-DD");
    }
  },
  directives: {
    rainbow: {
      bind(el) {
       el.style.background = `linear-gradient(to right,
        #${Math.random()
          .toString()
          .slice(3, 9)},
        #${Math.random()
          .toString()
          .slice(6, 9)}`;
      }
    }
  },
  methods: {
    getUserEvents() {
      this.loading = true;
      this.$http
        .get(`${eventUrl}/GetUserEvents/${this.userId}`)
        .then(resp => resp.json())
        .then(data => {
          this.loading = false;
          this.userEvents = data;
        })
        .catch(err => {
          console.log(err);
        });
    }
  },

  created() {
    this.getUserEvents();
  },
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
  margin: 3rem 0;
}
.events__single-title {
  color: $white;
  margin: 0;
  padding: 3rem 0 4rem;
  //background: linear-gradient(to right, #4ecdc4, #556270);
}
.events__paragraph {
  margin: 0;
  line-height: 1.7rem;
  text-align: left;
  padding: 4rem 1.4rem 3rem;
}
.events__single-link {
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

.events__wrapper {
  background: #fff;
}
.events__more-wrapper {
  display: flex;
  justify-content: space-between;
  align-items: flex-end;
  background: #fff;

  &::before {
    content: "";
    display: block;
    width: 90%;
    height: 0.05rem;
    margin: 0 auto;
    background: #00000020;
    position: absolute;
    bottom: 43px;
    left: 0;
    right: 0;
  }
}
.events__created-date {
  opacity: 1;
  width: 100%;
  text-align: right;
  padding: 0.8rem 1.2rem;
  color: #00000060;
  &--spot {
    color: #00000060;
    display: inline-block;
    border-right: 0.05rem solid #00000020;
    padding: 0 0.6rem 0 0;
    margin: 0 0.4rem 0 0;
  }
}
.events__list {
  list-style-type: none;
  padding: 0;
  width: 100%;
  max-width: 975px;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}
.events__item {
  position: relative;
  width: 100%;
  max-width: 300px;
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  border-radius: 0.2rem;
  overflow: hidden;
  margin: 0.7rem;
  transition: transform 0.4s ease-in-out, background 0.4s ease-in-out,
    color 0.4s ease-in-out;
}
.events__item:hover {
  transform: translateY(-3px);
}
.events__counter {
  position: absolute;
  bottom: 12px;
  left: 30px;

  &--icon {
    font-size: 20px;
    color: #00000060;
  }

  &--count {
    position: absolute;
    z-index: 5;
    top: -23px;
    left: -19px;
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
}
.theme-switch .wrapper {
  text-align: center;
}
.wrapper {
  width: 100%;
  position: relative;
  &-search--box {
    width: 100%;
    max-width: 975px;
    padding: 0 0.5rem;
    display: flex;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-items: center;
    margin-bottom: 2rem;
    @media only screen and(max-width: 1049px){
      max-width: 655px;
    }
    @media only screen and(max-width: 725px){
      justify-content: center;
      margin-bottom: 3rem;
    }
  }
  &-search--events {
    display: flex;
    justify-content: center;
    align-items: center;
    position: relative;
    max-width: 300px;
  }
  &-search--events .search-icon {
    width: 20px;
    height: 20px;
    position: absolute;
    top: 36px;
    right: 18px;
    @media only screen and(max-width: 695px){
      top: 21px;
    }
  }
}
.search-input {
  width: 100%;
  max-width: 280px;
  background: $white;
  border-radius: 0.2rem;
  border: none;
  color: #00000085;
  margin: 1.5rem 0 2.5rem 0;
  padding: 0.7rem 2.5rem 0.7rem 0.5rem;
  font-size: 1rem;
  box-shadow: 0px 6px 12px rgba(24, 41, 67, 0.2);
  @media only screen and(max-width: 695px){
      margin: 0.5rem 0;
    }

  &:focus {
    color: $navy-blue;
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
