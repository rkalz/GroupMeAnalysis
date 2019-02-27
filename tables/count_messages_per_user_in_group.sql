SELECT 
	public.messages.sender_id,
	COUNT(public.messages.sender_id)
FROM public.messages
WHERE public.messages.group_id = '19224977'
GROUP BY public.messages.sender_id;