namespace blog.Models;

public class Article
{
    public Article() : this("default-title", "empty")
    {
    }

    public void UpdateSlug()
    {
        Slug = SlugForTitle(Created, Title);
    }

    public void UpdateUpdated()
    {
        Updated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
        Created = DateTime.SpecifyKind(Created, DateTimeKind.Utc);
    }

    public Article(string title, string content)
    {
        Id = Guid.NewGuid();
        var now = DateTime.UtcNow;
        Created = now;
        Updated = now;
        Title = title;
        Content = content;
        Slug = SlugForTitle(now, title);
    }

    private string SlugForTitle(DateTime created, string title)
    {
        return created.ToString("yyyyMMddHHmmss-") + title;
    }

    public Article(Guid id, DateTime created, DateTime updated, string slug, string title, string content)
    {
        Id = id;
        Created = created;
        Updated = updated;
        Slug = slug;
        Title = title;
        Content = content;
    }

    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string Slug { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}