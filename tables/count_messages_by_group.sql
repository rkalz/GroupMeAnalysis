SELECT
	public.groups.id,
	COUNT(public.messages.group_id)
FROM public.messages LEFT JOIN public.groups
ON public.messages.group_id = public.groups.id
GROUP BY public.groups.id;