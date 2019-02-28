CREATE TABLE groups_enc (
    group_id character varying PRIMARY KEY,
    user_id character varying,
    name_enc bytea,
    desc_enc bytea,
    img_url_enc bytea,
    share_url_enc bytea
);