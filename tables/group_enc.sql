CREATE TABLE group_enc (
    group_id character varying PRIMARY KEY,
    user_id character varying,
    name_enc bytea,
    desc_enc bytea,
    img_url_enc bytea,
    share_url_enc bytea,
    CONSTRAINT grp_u_gid_uid UNIQUE (group_id, user_id)
);