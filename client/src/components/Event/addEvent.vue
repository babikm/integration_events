<template>
  <div id="addEvent" class="main-container--add">
     <h1 class="main-container--add__title">Dodaj nowe wydarzenie</h1>

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
      <button class="form__send-button" @click.prevent="post"><font-awesome-icon class="plus-icon icon" icon="plus" />Dodaj wydarzenie</button>
    </form>

    <div class="after-post" v-if="submitted">
      <h2 class="after-post__title">Wydarzenie zostało dodane!</h2>
    </div>

    <div class="preview">
      <h3 class="preview__header">Podgląd</h3>
      <p class="preview__date">
        Nazwa:
        <span class="preview__value">{{event.Name}}</span>
      </p>
      <p class="preview__title">
        Opis:
        <span class="preview__value">{{event.Description}}</span>
      </p>
      <p class="preview__content">
        Data:
        <span class="preview__value">{{event.Date | format-date}}</span>
      </p>
      <p class="preview__author">
        Miejsce:
        <span class="preview__value">{{event.Spot}}</span>
      </p>
    </div>
  </div>
</template>

<script>
import { eventUrl } from "@/variables";
import * as moment from "moment/moment";

export default {
  name: "addEvent",
  data() {
    return {
      event: {
        Name: "",
        Description: "",
        Date: "",
        Spot: "",
      },
      currentUser: this.$store.getters.getCurrentUser.id,
      submitted: false,
      errors: [],
      visible: false,
      hideErrors: false,
    };
  },
  filters: {
    formatDate(value) {
      if(value)
      return moment(String(value)).format('YYYY-MM-DD hh:mm')
    }
  },
  methods: {
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
    post() {
      const isValid = this.checkForm();
      if(isValid) {
        this.$http.post(`${eventUrl}/${this.currentUser}`, this.event)
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
    }
  }
};
</script>

<style lang="scss" scoped>
@import "@/assets/styles/variables.scss";

@keyframes rotateYe {
    0% {
        transform: rotate(0);
    }
    100% {
        transform: rotate(180deg);
    }
}

.main-container--add {
  width: 100%;
  max-width: 950px;
  padding: 1rem;
  margin: 0 auto;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
  align-items: flex-start;
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
    padding: 0.4rem;
    &--text-area {
      min-height: 80px;
    }
  }
  &__send-button {
    margin: 1rem 0;
   @include default-button($navy-blue);
    &:hover > svg {
      animation: rotateYe 1s infinite;
    }
  }
  &__errors {
    margin-top: 2rem;
    color: #dc6868;
  }
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