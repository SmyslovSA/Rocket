import { Email } from './email';
import { GenreMusic } from './genreMusic';
import { GenreTv } from './genreTv';


export class Profile {
    Id: number;
    FirstName: string;
    LastName: string;
    Avatar: string;
    Emails: Email[];
    GenreMusic:GenreMusic[];
    GenreTv:GenreTv[];
}
