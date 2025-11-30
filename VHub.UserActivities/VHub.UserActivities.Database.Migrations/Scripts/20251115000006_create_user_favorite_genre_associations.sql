CREATE TABLE IF NOT EXISTS user_activities.user_favorite_genre_associations
(
    user_id UUID,
    genre   SMALLINT,
    CONSTRAINT pk_user_favorite_genre_associations_user_id_genre PRIMARY KEY (user_id, genre)
);

COMMENT ON TABLE user_activities.user_favorite_genre_associations IS 'Ассоциации пользователей с любимыми жанрами';
COMMENT ON COLUMN user_activities.user_favorite_genre_associations.user_id IS 'ID пользователя';
COMMENT ON COLUMN user_activities.user_favorite_genre_associations.genre IS 'Жанр фильма';