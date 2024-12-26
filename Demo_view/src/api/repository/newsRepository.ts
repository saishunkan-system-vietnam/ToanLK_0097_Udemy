import BaseRepository from "../base/baseRepository";

class NewsRepository extends BaseRepository {
  constructor() {
    super('news');
  }

  add(newObj) {
    return this.client.post('/', newObj);
  }

  get() {
    return this.client.get("/");
  }

  delete(newsId) {
    return this.client.delete(`/${newsId}`);
  }

  deleteRange(ids){
    return this.client.post("/delete",ids);
  }

  update(news){
    return this.client.put("/",news);
  }

  getById(newsId){
    return this.client.get(`/${newsId}`);
  }

  getByQuantity(quantity){
    return this.client.get(`/newest/${quantity}`);
  }

}

export default new NewsRepository();