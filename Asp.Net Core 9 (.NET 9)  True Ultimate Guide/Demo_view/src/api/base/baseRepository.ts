import RestClient from "./restClient";

export default class BaseRepository {
  client;

  constructor(servicePath) {
    this.client = new RestClient(servicePath);
  }
}
