import { Email } from './email';

export class Profile {
    Id: number;
    FirstName: string;
    LastName: string;
    Avatar: string;
    Emails: Email[];
}
