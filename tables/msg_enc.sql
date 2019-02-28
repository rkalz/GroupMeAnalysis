CREATE TABLE msg_enc (
    id character varying PRIMARY KEY,
    user_id character varying,
    name_enc bytea,
    attach_url_enc bytea,
    msg_enc bytea,
    CONSTRAINT msg_u_gid_uid UNIQUE (id, user_id)
);