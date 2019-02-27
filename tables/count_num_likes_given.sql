SELECT uid, count(*) as ct
FROM messages, unnest(messages.favorited_by) AS uid
WHERE messages.group_id = '19224977'
GROUP BY uid;