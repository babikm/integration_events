<template>
  <div class="login">
    <div class="loginPanel">
      <h2 class="form-title">Logowanie</h2>
      <form method="POST" class="form" id="login-form" @submit.prevent="signin">
        <div class="form-group">
          <label for="username">
            <font-awesome-icon class="user-icon icon" icon="user"/>
          </label>
          <input
            v-model="credential.username"
            type="text"
            class="form-input"
            id="username"
            placeholder="Nazwa użytkownika"
            autofocus
          >
        </div>
        <div class="form-group">
          <label for="password">
            <font-awesome-icon class="lock-icon icon" icon="lock"/>
          </label>
          <input
            v-model="credential.password"
            type="password"
            class="form-input"
            id="password"
            placeholder="Hasło"
          >
        </div>
        <div class="form-group form-button">
          <span class="form__errors" v-if="errors.length && !isVaild">
            <span class="form__errors--info">Popraw następujące błędy:</span>
            <ul class="form__errors-list">
              <li class="form__errors-item" v-for="error in errors" :key="error">{{error}}</li>
            </ul>
          </span>
          <button class="btn btn-dark loginBtn" @click.prevent="signin">Zaloguj</button>
          <span class="form-register">Nie masz jeszcze konta?
            <router-link class="form-register--link" to="/register">Zarejstruj się</router-link>
          </span>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "login",
  data() {
    return {
      credential: {
        username: "",
        password: ""
      },
      errors: [],
      isVaild: false
    };
  },
  methods: {
    login() {
      this.$store.dispatch("login", this.credential);
    },
    signin() {
      this.isValid = this.checkForm();
      if (this.isValid) {
        this.login();
      }
    },
    checkForm() {
      this.errors = [];
      if (this.credential.username && this.credential.password) {
        return true;
      }
      if (!this.credential.username) {
        this.errors.push("Nazwa użytkownika jest wymagana!");
      }
      if (!this.credential.password) {
        this.errors.push("Hasło jest wymagane!");
      }
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
.login {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.loginPanel {
  display: flex;
  flex-direction: column;
  justify-content: center;
  border-radius: 0.2rem;
  text-align: center;
  background-color: rgba(255, 255, 255, 0.9);
  box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  margin: 2rem auto 0;
  width: 350px;
  padding: 1rem 3.5rem 2rem;
}
.header {
  background-color: rgba(105, 105, 105, 0.8);
  height: 100px;
  width: 100%;
}

.formLayout {
  height: 450px;
  width: 100%;
  background-color: rgba(220, 220, 220, 0.9);
  display: flex;
  flex-direction: column;
  border: 2px solid lightslategray;
}
.form {
  margin: 1rem 0;
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  &__errors {
    font-size: 0.9rem;
    text-align: left;
    margin-top: 2rem;
    color: #dc6868;
    &-list {
      margin: 0;
      padding: 0.5rem 0 0 1.3rem;
    }
  }
}

.form-group {
  width: 100%;
  position: relative;
}

.form-button {
  display: flex;
  flex-direction: column;
}

.form-register {
  margin-top: 1rem;
}

label {
  position: absolute;
  top: 36px;
  left: 0;
}
.icon {
  font-size: 13px;
}
.form-title {
  margin: 2rem 0 1rem 0;
  font-size: 2rem;
  width: 100%;
}
.form-input {
  margin-top: 2rem;
  width: 100%;
  padding: 0.5rem 1.5rem;
  border: none;
  border-bottom: 1px solid rgba(0, 0, 0, 0.38);
  background: none;
  outline: none;
  transition: border-color 0.3s ease-in-out;

  &:focus {
    border-bottom-color: rgba(0, 0, 0, 0.95);
  }
}
.loginBtn {
  font-weight: bold;
  margin-top: 3rem;
  padding: 0.7rem 2.5rem;
  border: none;
  border-radius: 0px 8px;
  background-color: #4fc08d;
  color: #fff;
  cursor: pointer;
  transition: box-shadow 0.5s ease-in-out;
  &:hover {
    box-shadow: 0px 6px 16px rgba(24, 41, 67, 0.2);
  }
  &:active {
    background-color: #44ae7e;
  }
}

</style>
