namespace IntegrationTests.ApiClients;

public static class DeleteAll
{
    /// <summary>
    /// Deletes every id <paramref name="list"/> returns, page by page, until it comes back empty.
    /// Throws when a deleted id is listed again, so a delete that doesn't delete fails instead of looping forever.
    /// </summary>
    public static async Task Run(string resource, Func<Task<IEnumerable<Guid>>> list, Func<Guid, Task> delete)
    {
        HashSet<Guid> deleted = [];
        List<Guid> ids = (await list()).Distinct().ToList();

        while (ids.Count > 0)
        {
            foreach (Guid id in ids)
            {
                if (!deleted.Add(id))
                    throw new Exception($"{resource}:{id} is still listed after it was deleted");

                await delete(id);
            }

            ids = (await list()).Distinct().ToList();
        }
    }
}
