CREATE TABLE groups (
    id character varying PRIMARY KEY,
    type character varying,
    creator_user_id character varying,
    created_at timestamp without time zone,
    updated_at timestamp without time zone,
    members character varying[]
);