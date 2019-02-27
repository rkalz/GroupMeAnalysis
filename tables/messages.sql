CREATE TABLE messages (
    id character varying DEFAULT nextval('messages_id_seq'::regclass) PRIMARY KEY,
    source_guid character varying,
    created_at timestamp without time zone,
    user_id character varying,
    group_id character varying REFERENCES groups(id),
    sender_id character varying,
    sender_type character varying,
    name text,
    system boolean,
    favorited_by character varying[],
    attachment_mentions character varying[],
    attachment_types character varying[],
    attachment_urls text[],
    attachment_locis text[],
    message text
);