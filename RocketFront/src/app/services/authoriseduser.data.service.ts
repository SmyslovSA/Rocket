import {SimpleUser} from '../models/personal-area/authorised-user';
export class DataService {
    private data: SimpleUser = new SimpleUser();
    getData(): SimpleUser {
        this.data.FirstName = 'Tom';
        this.data.LastName = 'Hanks';
        this.data.Avatar = 'new';
        return this.data;
    }
}
