CREATE TABLE IF NOT EXISTS user_activities.user_favorite_person_associations
(
    user_id   UUID,
    person_id CHAR(24),
    CONSTRAINT pk_user_favorite_person_associations_user_id_person_id PRIMARY KEY (user_id, person_id)
);

COMMENT ON TABLE user_activities.user_favorite_person_associations IS 'Ассоциации пользователей с любимыми персонами';
COMMENT ON COLUMN user_activities.user_favorite_person_associations.user_id IS 'ID пользователя';
COMMENT ON COLUMN user_activities.user_favorite_person_associations.person_id IS 'ID персоны';