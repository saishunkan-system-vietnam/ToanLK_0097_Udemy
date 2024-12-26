import BaseRepository from "../base/baseRepository";

class ProductRepository extends BaseRepository {
  constructor() {
    super('product');
  }

  add(product) {
    return this.client.post('/add', product);
  }

  get() {
    return this.client.get("/");
  }

  getNewProduct() {
    return this.client.get("/get-new");
  }

  getByType(typeId,quantity,page) {
    return this.client.get(`/get-by-type/${typeId}/${quantity}/${page}`);
  }

  getById(id) {
    return this.client.get(`/${id}`)
  }

  getRelated(keyword) {
    return this.client.get(`/get-related/${keyword}`)
  }

  update(product) {
    return this.client.put("/", product)
  }

  delete(id) {
    return this.client.delete(`/${id}`);
  }

  deleteRange(guids) {
    return this.client.post("/delete", guids);
  }

  getByQuantity(quantity){
    return this.client.post(`/newest/${quantity}`);
  }

}

export default new ProductRepository();