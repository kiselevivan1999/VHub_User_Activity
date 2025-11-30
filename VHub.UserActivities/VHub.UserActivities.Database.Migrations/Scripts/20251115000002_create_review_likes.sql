CREATE TABLE IF NOT EXISTS user_activities.review_likes
(
    appraiser_id UUID,
    review_id    BIGINT,
    type         SMALLINT NOT NULL,
    CONSTRAINT pk_review_likes_appraiser_id_review_id PRIMARY KEY (appraiser_id, review_id),
    CONSTRAINT fk_review_likes_review_id_reviews_id FOREIGN KEY (review_id) REFERENCES user_activities.reviews (id)
);

COMMENT ON TABLE user_activities.review_likes IS 'Лайки';
COMMENT ON COLUMN user_activities.review_likes.appraiser_id IS 'ID оценщика';
COMMENT ON COLUMN user_activities.review_likes.review_id IS 'ID рецензии';
COMMENT ON COLUMN user_activities.review_likes.type IS 'Тип лайка';