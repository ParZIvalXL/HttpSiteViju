namespace Templator.TemplateClasses;

public class FilmInfoTemplate
{
    public string film_name { get;  }
    public string full_film_name { get;  }
    public string film_poster { get;  }
    public string film_name_poster { get;  }
    public string film_year { get;  }
    public string film_countries { get;  }
    public string film_duration { get;  }
    public string film_age_limit { get;  }
    public string film_genres { get;  }
    public string film_tagline { get;  }
    public string film_starring { get;  }
    public string film_description { get;  }
    public string film_director { get;  }
    public string film_actors { get;  }
    public string film_writers { get;  }
    public string film_producers { get;  }
    public string film_audio_channels { get;  }
    public string film_quality { get;  }
    public string film_plot_sumary { get;  }
    public string vklink { get;  }
    public string film_id { get;  }

    public FilmInfoTemplate( string filmName,  string fullFilmName,  string filmPoster
        ,  string filmNamePoster,  string filmYear,  string filmCountries,  string filmDuration
        ,  string filmAgeLimit,  string filmGenres,  string filmTagline,  string filmStarring,  string filmDescription
        ,  string filmDirector,  string filmActors,  string filmWriters,  string filmProducers
        ,  string filmAudioChannels,  string filmQuality,  string filmPlotSumary,  string vklink, int filmFilmId)
    {
        film_name = filmName;
        full_film_name = fullFilmName;
        film_poster = filmPoster;
        film_name_poster = filmNamePoster;
        film_year = filmYear;
        film_countries = filmCountries;
        film_duration = filmDuration;
        film_age_limit = filmAgeLimit;
        film_genres = filmGenres;
        film_tagline = filmTagline;
        film_description = filmDescription;
        film_director = filmDirector;
        film_actors = filmActors;
        film_writers = filmWriters;
        film_producers = filmProducers;
        film_audio_channels = filmAudioChannels;
        film_quality = filmQuality;
        film_plot_sumary = filmPlotSumary;
        vklink = vklink;
        film_id = filmFilmId.ToString();
        film_starring = filmStarring;

    }
}