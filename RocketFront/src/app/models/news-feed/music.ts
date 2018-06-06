import { Musician } from './musician';

export class Music {
  Id: number;
  Title: string;
  ReleaseDate: Date;
  PosterImagePath: string;
  Musicians: Musician[];
}
