using MediatR;
using NCore.Systems;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace ITC.Domain.Commands.GoogleAnalytics.Models
{
    public class CreateMailTMCommand : IRequest<JsonResponse<bool>>
    {
        public string Address { get; set; }
        public string Password { get; set; }
    }

    public class GetCodeMailTMQuery : IRequest<JsonResponse<string>>
    {
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
    }

    public class MailTMTokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class MessageCollection
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("hydra:totalItems")]
        public int TotalItems { get; set; }

        [JsonProperty("hydra:member")]
        public List<Message> Members { get; set; }
    }

    public class Message
    {
        [JsonProperty("@id")]
        public string MessageUri { get; set; }

        [JsonProperty("@type")]
        public string MessageType { get; set; }

        public string Id { get; set; }

        public string Msgid { get; set; }

        public EmailAddress From { get; set; }

        public List<EmailAddress> To { get; set; }

        public string Subject { get; set; }

        public string Intro { get; set; }

        public bool Seen { get; set; }

        public bool IsDeleted { get; set; }

        public bool HasAttachments { get; set; }

        public int Size { get; set; }

        public string DownloadUrl { get; set; }

        public string SourceUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string AccountId { get; set; }
    }

    public class EmailAddress
    {
        public string Address { get; set; }

        public string Name { get; set; }
    }

}
