import BaseRepository from "../base/baseRepository";

class AccountRepository extends BaseRepository {
  constructor() {
    super('account');
  }

  signUp(user){
    return this.client.post('/sign-up',user);
  }

  verifyAccount(token){
    return this.client.get(`/verify-account/${token}`);
  }

  login(user){
    return this.client.post("/login",user);
  }
  
  getMe(){
    return this.client.get("/get-me");
  }

  update(user){
    return this.client.put("/",user);
  }
  
  changePassword(Password){
    return this.client.put('/password',Password);
  }

}

export default new AccountRepository();