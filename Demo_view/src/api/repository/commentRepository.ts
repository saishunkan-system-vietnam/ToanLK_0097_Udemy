import BaseRepository from "../base/baseRepository";

class CommentRepository extends BaseRepository {
  constructor() {
    super('comment-sender');
  }

  SendMessage(){
    return this.client.get('/');
  }

}

export default new CommentRepository();