<template>
  <div id="singleBlog" class="main-container--blog single-blog">
    <button class="single-blog__button" @click="backToList"><font-awesome-icon class="arrow-left-icon icon-arrow" icon="arrow-left" />Powrót</button>
    <div class="single-blog--box">
      <h1 class="single-blog__title" >{{event.name}}</h1>
        <p class="single-blog__paragraph">{{event.description}}</p>
        <p class="single-blog__paragraph"><strong class="single-blog__strong">Uczestnicy: </strong>
          <ul class="single-blog__categories-list">
              <li class="single-blog__categories-item" v-for="user in event.userList" :key="user">
                  {{user.userName}} - {{user.firstName}} {{user.lastName}} 
              </li>
          </ul>
        </p>
    </div>
    <div class="buttons-wrapper">
      <button title="Dołącz" class="my-button join">Dołącz
        <font-awesome-icon class="icon" icon="check"/>
      </button>
      <button title="Edytuj" class="my-button edit">Edytuj
        <font-awesome-icon class="icon" icon="eraser"/>
      </button>
      <button title="Usuń" class="my-button delete">Usuń
        <font-awesome-icon class="icon" icon="trash-alt"/>
      </button>
    </div>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";

export default {
   name: "singleEvent",
  data() {
    return {
        id: this.$route.params.id,
        event: {}
      }
    },
    methods: {
        backToList() {
            this.$router.push( '/' );
        },
    },
    created() {
        this.$http.get(`${eventUrl}/${this.id}`)
        .then(response => response.json())
        .then(object => {
             this.event = object;
        })
        .catch(err => {
          console.log(err)
          })
    }
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

@keyframes moveYe {
    0% {
        transform: translateX(0px);
    }
    50% {
        transform: translateX(-3px);
    }
    100% {
        transform: translateX(0px);
    }
    
}
.single-blog {
    width: 100%;
    max-width: 900px;
    margin: 2rem auto;
    text-align: left;
    &--box {
      padding: 1rem;
      box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
    }
    &__button {
      margin: 1rem 0 3rem;
      background: #2c3e50;
      border: 1px solid #2c3e50;
      cursor: pointer;
      color: #ffffff;
      font-weight: bold;
      padding: 0.7rem 1.3rem;
      transition: background .4s ease-in-out, color .4s ease-in-out;
      &:hover {
        background: #ffffff;
        color: #2c3e50;
      }
      &:hover > svg {
          animation: moveYe .8s infinite;
      }
    }
}
.icon {
    width: 15px;
    height: 15px;
    margin-left: 0.5rem;
}

.icon-arrow {
    width: 15px;
    height: 15px;
    margin-right: 0.5rem;
    animation: moveYe .8s;
}

.my-button + .my-button {
  margin-top: 0.5rem;
  margin-left: 0.5rem;
}

.join {
 @include default-button($dark-green);
}

.edit {
  @include default-button($dark-blue);
}

.delete {
  @include default-button($dark-red);
}
</style>