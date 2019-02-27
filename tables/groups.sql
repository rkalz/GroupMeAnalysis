CREATE TABLE groups (
    id character varying PRIMARY KEY,
    name text,
    type character varying,
    description text,
    image_url character varying,
    creator_user_id character varying,
    created_at timestamp without time zone,
    updated_at timestamp without time zone,
    members character varying[],
    share_url character varying
);