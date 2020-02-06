<template>
  <div id="editEvent" class="main-container--edit">
     <h1 class="main-container--edit__title">Modyfikuj wydarzenie: {{event.Name}}</h1>
     
     <button class="single-event__button" @click="back"><font-awesome-icon class="plus-icon icon" icon="arrow-left" />Powrót</button>
    <form v-if="!submitted" class="form">
      <p class="form__wrapper">
        <label class="form__label" for="name">Nazwa:</label>
        <input class="form__input date-input" type="text" name="name" id="name" required v-model.lazy="event.Name" />
      </p>
      <p class="form__wrapper">
        <label class="form__label" for="desc">Opis:</label>
        <textarea class="form__input form__input--text-area" name="desc" id="desc" required v-model.lazy="event.Description" />
      </p>
      <p>
        <label class="form__label" for="date">Data:</label>
        <input class="form__input" type="datetime-local" name="date" id="date" required v-model.lazy="event.Date">
      </p>
      <p>
        <label class="form__label" for="spot">Miejsce:</label>
        <input class="form__input" type="text" name="spot" id="spot" required v-model.lazy="event.Spot">
      </p>
      <p class="form__errors" v-if="errors.length">
        <span>Popraw następujące błędy:</span>
        <ul>
          <li v-for="error in errors" :key="error">
            {{error}}
          </li>
        </ul>
      </p>
      <button class="form__send-button join" @click.prevent="put"><font-awesome-icon class="plus-icon icon" icon="check" />Zatwierdź</button>
    </form>

    <div class="after-post" v-if="submitted">
      <h2 class="after-post__title">Wydarzenie zostało zmodyfikowane!</h2>
    </div>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";
import * as moment from "moment/moment";


export default {
  name: "editEvent",
  data() {
    return {
      event: {
        Name: "",
        Description: "",
        Date: "",
        Spot: "",
      },
      eventId: this.$route.params.id,
      submitted: false,
      errors: [],
      visible: false,
      hideErrors: false,
    };
  },
  methods: {
    back(){
        this.$router.replace(`/event/${this.eventId}`);
    },
    show() {
      this.visible = true;
    },
    showErrors() {
      this.visible = false;
      this.hideErrors = true;
    },
    checkForm() {
      this.errors = [];
      if(this.event.Name && this.event.Description && this.event.Date && this.event.Spot) {
        return true;
      }
      if(!this.event.Name) {
        this.errors.push('Nazwa jest wymagana!');
      }
      if(!this.event.Description) {
        this.errors.push('Opis jest wymagany!');
      }
      if(!this.event.Date) {
        this.errors.push('Data jest wymagana!');
      }
      if(!this.event.Spot) {
        this.errors.push('Miejsce jest wymagane!');
      }
    },
    put() {
      const isValid = this.checkForm();
      if(isValid) {
        this.$http.put(`${eventUrl}/${this.eventId}`, this.event)
        .then(data => {
          this.submitted = true;
          setTimeout(() => {
            this.$router.push('/');
          }, 1000);
        })
        .catch(err => {
          console.log(err)
          })
      }
    },
    getEvent() {
           this.$http.get(`${eventUrl}/${this.eventId}`)
          .then(response => response.json())
          .then(object => {
              const {name, description, date, spot} = object;
              this.event.Name = name;
              this.event.Description = description;
              this.event.Date = moment(date).format("YYYY-MM-DDThh:mm");
              this.event.Spot = spot;
          })
          .catch(err => {
            console.log(err)
            })
        }
  },
  created() {
      this.getEvent();
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


.single-event__button {
  @include default-button($navy-blue);
  margin: 2rem 18rem 2rem 0;
  &:hover > svg {
          animation: moveYe .8s infinite;
      }
}

.main-container--edit {
  width: 100%;
  max-width: 950px;
  padding: 1rem;
  margin: 0 auto;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  &__title {
    margin: 3rem 0 4rem 0;
    flex-basis: 100%;
    text-align: center;
  }
}
.form {
  width: 100%;
  max-width: 400px;
  text-align: left;
  &__wrapper {
    margin: 1rem 0;
    &--checkbox {
      display: inline-block;
    }
    &--checkbox + &--checkbox {
      margin-left: 1rem;
    }
  }
  &__label {
    display: block;
    margin-bottom: 0.4rem;
  }
  &__input {
    display: block;
    width: 100%;
    padding: 0.6rem;
    border: none;
    border-radius: 0.2rem;
    background: #fff;
    box-shadow: $default-shadow;
    &--text-area {
      min-height: 80px;
    }
  }

  &__errors {
    margin-top: 2rem;
    color: #dc6868;
  }
}

.join {
 @include default-button($dark-green);
}

svg {
    margin-right: 0.5rem;
    animation: rotateYe 1s;
}
.preview {
  width: 100%;
  max-width: 400px;
  margin-top: 2.5rem;
  padding: 0.4rem 1rem;
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  text-align: left;
  overflow-wrap: break-word;
  &__content {
    display: block;
  }
  &__value {
    color: #2c3e508e;
  }
}
</style>