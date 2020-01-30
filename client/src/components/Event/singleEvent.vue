<template>
  <div id="singleBlog" class="main-container--blog single-blog">
    <button class="single-blog__button" @click="backToList"><font-awesome-icon class="arrow-left-icon icon-arrow" icon="arrow-left" />Powrót</button>
    <div class="single-blog--box">
      <h1 class="single-blog__title" >{{event.name}}</h1>
        <p class="single-blog__paragraph">{{event.description}}</p>
        <p class="single-blog__paragraph"><strong class="single-blog__strong">Uczestnicy: </strong>
          <ul class="single-blog__categories-list">
              <li class="single-blog__categories-item" v-for="user in event.userList" :key="user.userName">
                  {{user.userName}} - {{user.firstName}} {{user.lastName}} 
              </li>
          </ul>
        </p>
    </div>
    <div class="buttons-wrapper">
      <button title="Dołącz" v-if="canHeJoin" class="my-button join" @click="joinTheEvent">Dołącz
        <font-awesome-icon class="icon" icon="check"/>
      </button>
      <button title="Edytuj" v-if="canHeDoAnyAction" class="my-button edit" @click="edit">Edytuj
        <font-awesome-icon class="icon" icon="eraser"/>
      </button>
      <button title="Usuń" v-if="canHeDoAnyAction" class="my-button delete" @click="deleteEvent">Usuń
        <font-awesome-icon class="icon" icon="trash-alt"/>
      </button>
    </div>
  </div>
</template>

<script>
import { eventUrl, getUserDataUrl } from "@/variables";

export default {
   name: "singleEvent",
  data() {
    return {
        eventId: this.$route.params.id,
        userId: this.$store.getters.getCurrentUser.id,
        userName: this.$store.getters.getCurrentUser.userName,
        event: {},
        user: {},
        canHeJoin: true,
        canHeDoAnyAction: false,
      }
    },
    methods: {
        edit() {
          this.$router.replace(`/event/${this.event.id}/edit`);
        },
        deleteEvent() {
          if(confirm('Czy na pewno chcesz usunąć wydarzenie?'))
            this.$http.delete(`${eventUrl}/${this.eventId}`)
            .then(response => {
              alert('Wydarzenie zostało usunięte')
              this.$router.push( '/' );
              })
            .catch(err => {
              alert(err.body);
            })
        },
        joinTheEvent() {
          this.$http.post(`${eventUrl}/Join/${this.eventId}/${this.userId}`)
          .then(response => {
            alert(response.body)
            this.$router.push( '/' );
            })
          .catch(err => {
            alert(err.body);
            console.log(err.status);
           })
        },
        backToList() {
            this.$router.push( '/' );
        },
        getEvent() {
           this.$http.get(`${eventUrl}/${this.eventId}`)
          .then(response => response.json())
          .then(object => {
              this.event = object;
          })
          .catch(err => {
            console.log(err)
            })
        },
        getUserData() {
          this.$http.get(`${getUserDataUrl}/${this.userName}`)
          .then(response => response.json())
          .then(object => {
              this.user = object;
          })
          .then(() => {
            for(let idJ of this.user.eventJoined) {
                if(idJ === this.eventId) {
                  this.canHeJoin = false;
                  break;
                }
            }
            for(let idC of this.user.eventCreated) {
                if(idC === this.eventId) {
                  this.canHeDoAnyAction = true;
                  break;
                }
            }
          })
          .catch(err => {
            console.log(err)
            })
        },
    },
    created() {
      this.getEvent();
      this.getUserData();
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
      @include default-button($navy-blue);
      margin: 2rem 0 1.5rem 0;
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

.my-button {
  margin-top: 0.5rem;
}

.my-button + .my-button {
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