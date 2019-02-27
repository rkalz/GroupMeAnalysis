SELECT *
FROM public.messages
WHERE public.messages.group_id = '19224977' AND array_length(messages.favorited_by, 1) > 5
GROUP BY array_length(messages.favorited_by, 1);