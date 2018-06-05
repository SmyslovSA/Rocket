import { Genre } from './genre';
import { MusicTrack } from './music-track';
import { Musician } from './musician';
import { Music } from './music';

export class MusicDetails extends Music {
  Title: string;
  PosterImagePath: string;
  Duration: string;
  Genres: Genre[];
  MusicTracks: MusicTrack[];
  Musicians: Musician[];
  Id : number;
}
