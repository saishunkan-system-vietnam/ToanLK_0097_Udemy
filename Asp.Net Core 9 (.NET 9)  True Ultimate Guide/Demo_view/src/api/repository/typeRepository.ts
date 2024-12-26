import BaseRepository from "../base/baseRepository";

class TypeRepository extends BaseRepository {
  constructor() {
    super('type');
  }

  add(type) {
    return this.client.post('/',type);
  }

  get() {
    return this.client.get("/");
  }

  getByShowLandingPage(){
    return this.client.get("/get-by-landing-page");
  }

  delete(guid){
    return this.client.delete(`/${guid}`);
  }

  updateFlagShow(guid,isShow){
    return this.client.put(`/`,{
      id: guid,
      isShow:isShow
    });
  }


  update(type){
    return this.client.put("/update",type);
  }
}

export default new TypeRepository();