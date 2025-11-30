CREATE TABLE IF NOT EXISTS user_activities.movie_rates
(
    appraiser_id UUID,
    movie_id     CHAR(24),
    value        SMALLINT NOT NULL,
    CONSTRAINT pk_movie_rates_appraiser_id_movie_id PRIMARY KEY (appraiser_id, movie_id)
);

COMMENT ON TABLE user_activities.movie_rates IS 'Оценки фильмов';
COMMENT ON COLUMN user_activities.movie_rates.appraiser_id IS 'ID оценщика';
COMMENT ON COLUMN user_activities.movie_rates.movie_id IS 'ID фильма';
COMMENT ON COLUMN user_activities.movie_rates.value IS 'Значение оценки (от 1 до 10)';