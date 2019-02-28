CREATE TABLE msg_enc (
    group_id character varying PRIMARY KEY,
    name_enc bytea,
    attach_url_enc bytea,
    msg_enc bytea,
    user_id character varying,
    CONSTRAINT msg_u_gid_uid UNIQUE (group_id, user_id)
);