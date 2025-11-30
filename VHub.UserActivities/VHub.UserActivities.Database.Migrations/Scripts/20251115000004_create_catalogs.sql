CREATE TABLE IF NOT EXISTS user_activities.catalogs
(
    id      BIGINT GENERATED ALWAYS AS IDENTITY,
    title   VARCHAR(30) NOT NULL,
    user_id UUID        NOT NULL,
    CONSTRAINT pk_catalogs_id PRIMARY KEY (id)
);

COMMENT ON TABLE user_activities.catalogs IS 'Каталоги';
COMMENT ON COLUMN user_activities.catalogs.id IS 'ID каталога';
COMMENT ON COLUMN user_activities.catalogs.title IS 'Название';
COMMENT ON COLUMN user_activities.catalogs.user_id IS 'ID пользователя, создавшего каталог';