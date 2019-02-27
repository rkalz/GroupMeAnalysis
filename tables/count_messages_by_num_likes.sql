SELECT
	DISTINCT array_length(messages.favorited_by, 1),
	COUNT(array_length(messages.favorited_by,1))
FROM public.messages
WHERE public.messages.group_id = '19224977'
GROUP BY array_length(messages.favorited_by, 1);