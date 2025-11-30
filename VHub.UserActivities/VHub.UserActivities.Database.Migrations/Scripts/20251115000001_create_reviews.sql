CREATE TABLE IF NOT EXISTS user_activities.reviews
(
    id         BIGINT GENERATED ALWAYS AS IDENTITY,
    author_id  UUID     NOT NULL,
    movie_id   CHAR(24) NOT NULL,
    content    TEXT     NOT NULL,
    type       SMALLINT NOT NULL,
    created_at TIMESTAMP DEFAULT pg_catalog.now(),
    CONSTRAINT pk_reviews_id PRIMARY KEY (id)
);

COMMENT ON TABLE user_activities.reviews IS 'Рецензии';
COMMENT ON COLUMN user_activities.reviews.id IS 'ID рецензии';
COMMENT ON COLUMN user_activities.reviews.author_id IS 'ID рецензента';
COMMENT ON COLUMN user_activities.reviews.movie_id IS 'ID фильма, на который написана рецензия';
COMMENT ON COLUMN user_activities.reviews.content IS 'Содержимое';
COMMENT ON COLUMN user_activities.reviews.type IS 'Тип рецензии';
COMMENT ON COLUMN user_activities.reviews.created_at IS 'Дата создания';