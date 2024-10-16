namespace SNET.Framework.Domain.Notifications.Email;

public class MailModel
{
    public EmailAddress From { get; set; }
    public List<EmailAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public bool IsBodyHtml { get; set; }
    public List<string> Files { get; set; }
    public List<StreamAttachment> Streams { get; set; }
}

public class EmailAddress
{
    public string DisplayName { get; set; }
    public string Addres { get; set; }
}

public class StreamAttachment
{
    public Stream Stream { get; set; }
    public string Name { get; set; }
    public string MediaType { get; set; }
}
