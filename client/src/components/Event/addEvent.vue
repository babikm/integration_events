<template>
  <div id="addBlog" class="main-container--add">
     <h1 class="main-container--add__title">Dodaj nowe wydarzenie</h1>

    <form v-if="!submitted" class="form">
      <p class="form__wrapper">
        <label class="form__label" for="cratedDate">Nazwa:</label>
        <input class="form__input date-input" type="text" name="createdDate" id="createdDate" required v-model.lazy="event.Name" />
      </p>
      <p class="form__wrapper">
        <label class="form__label" for="title">Opis:</label>
        <textarea class="form__input form__input--text-area" name="title" id="title" required v-model.lazy="event.Description" />
      </p>
      <p>
        <label class="form__label" for="content">Data:</label>
        <input class="form__input" type="datetime-local" name="content" id="content" required v-model.lazy="event.Date">
      </p>
      <p>
        <label class="form__label" for="content">Miejsce:</label>
        <input class="form__input" type="text" name="content" id="content" required v-model.lazy="event.Spot">
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
        <span class="preview__value">{{event.Date}}</span>
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


const today = new Date().toISOString().slice(0,10);


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
          console.log(data);
          this.submitted = true;
          setTimeout(() => {
            this.$router.push('/');
          }, 1500);
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
@keyframes rotateYe {
    0% {
        transform: rotate(0);
    }
    100% {
        transform: rotate(180deg);
    }
}
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s, transform 0.5s;
}
.fade-enter, .fade-leave-to /* .fade-leave-active below version 2.1.8 */ {
  opacity: 0;
  transform: translateY(-10px);
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
  &__header {
    text-align: center;
  }
  &__content {
    display: block;
  }
  &__value {
    color: #2c3e508e;
  }
}
</style>