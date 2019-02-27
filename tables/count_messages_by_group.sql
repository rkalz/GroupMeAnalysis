SELECT
	public.groups.name,
	COUNT(public.messages.group_id)
FROM public.messages LEFT JOIN public.groups
ON public.messages.group_id = public.groups.id
GROUP BY public.groups.name;