namespace CustomerManagement.Application.Dtos.CommunicationTemplates
{
    public class TemplateDetailsDto
    {
        public int CommunicationTemplateId { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}