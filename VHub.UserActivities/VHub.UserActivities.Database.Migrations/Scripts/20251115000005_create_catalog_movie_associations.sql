CREATE TABLE IF NOT EXISTS user_activities.catalog_movie_associations
(
    catalog_id BIGINT,
    movie_id   CHAR(24),
    CONSTRAINT pk_catalog_movie_associations_catalog_id_movie_id PRIMARY KEY (catalog_id, movie_id),
    CONSTRAINT fk_catalog_movie_associations_catalog_id_catalogs_id FOREIGN KEY (catalog_id) REFERENCES user_activities.catalogs (id)
);

COMMENT ON TABLE user_activities.catalog_movie_associations IS 'Ассоциации каталогов с фильмами';
COMMENT ON COLUMN user_activities.catalog_movie_associations.catalog_id IS 'ID каталога';
COMMENT ON COLUMN user_activities.catalog_movie_associations.movie_id IS 'ID фильма';